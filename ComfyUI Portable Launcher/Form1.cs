using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;


namespace ComfyUI_Portable_Launcher
{
    public partial class ComfyForm : Form
    {
        private Process comfyProcess;

        public ComfyForm()
        {
            InitializeComponent();
        }
        private bool HasNvidiaGpu()
        {
            try
            {
                using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}");
                if (key != null)
                {
                    foreach (var subkeyName in key.GetSubKeyNames())
                    {
                        using var subkey = key.OpenSubKey(subkeyName);
                        string provider = subkey?.GetValue("ProviderName")?.ToString() ?? "";
                        string desc = subkey?.GetValue("DriverDesc")?.ToString() ?? "";
                        if (provider.Contains("NVIDIA") || desc.Contains("NVIDIA"))
                            return true;
                    }
                }
            }
            catch { /* Fallback to assuming false if WMI fails */ }
            return false;
        }
        private void CleanupComfyProcess(int pid)
        {
            AppendToTerminal("[Shutting down Python env]");
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/F /T /PID {pid}",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi).WaitForExit();
            }
            catch { /* Ignore errors during shutdown */ }
        }
        private void RunComfyUI()
        {
            string comfyArgs = "";

            string appDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string pythonExe = Path.Combine(appDirectory, "python_embeded", "python.exe");
            string mainPy = Path.Combine(appDirectory, "ComfyUI", "main.py");

            if (!File.Exists(mainPy))
            {
                MessageBox.Show(
                    "ERROR: The ComfyUI embedded venv not found in this location!\n\n" +
                    "Please copy this EXE into your ComfyUI_windows_portable folder and try again.\n"+
                    "This EXE should be placed next to 'run_nvidia_gpu.bat'.",
                    "Wrong launch directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Application.Exit();
                return;
            }

            if (HasNvidiaGpu())
            {
                comfyArgs = $"-s \"{mainPy}\" --windows-standalone-build --disable-auto-launch";
            }
            else
            {
                comfyArgs = $"-s \"{mainPy}\" --cpu --windows-standalone-build --disable-auto-launch";
            }

            AppendToTerminal("Starting ComfyUI Portable");
            AppendToTerminal("python_embedded\\python.exe "+comfyArgs);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = comfyArgs,
                WorkingDirectory = Path.Combine(appDirectory, "ComfyUI"),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            comfyProcess = new Process { StartInfo = startInfo };
            comfyProcess.OutputDataReceived += (s, e) => AppendToTerminal(e.Data);
            comfyProcess.ErrorDataReceived += (s, e) => AppendToTerminal(e.Data);
            comfyProcess.Start();

            comfyProcess.BeginOutputReadLine();
            comfyProcess.BeginErrorReadLine();
        }
        private void AppendToTerminal(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            this.Invoke(new Action(() =>
            {
                ComfyTerminal.AppendText(text + Environment.NewLine);
                if (text.Contains("To see the GUI") || text.Contains("http://127.0.0.1:8188"))
                {
                    ShowBrowser();
                }
            }));
        }
        private void ShowBrowser()
        {
            ComfyViewer.Visible = true;
            ComfyViewer.CoreWebView2.Navigate("http://127.0.0.1:8188");
            ComfyTerminal.Visible = false;
        }
        private async void ComfyForm_Load(object sender, EventArgs e)
        {
            var env = await CoreWebView2Environment.CreateAsync(null, Path.Combine(Path.GetTempPath(), "ComfyBrowserCache"));
            await ComfyViewer.EnsureCoreWebView2Async(env);
            RunComfyUI();
        } 
        private void ComfyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComfyTerminal.Show();
            ComfyViewer.Hide();
            AppendToTerminal(">");
            AppendToTerminal("[Initializing shutdown]");
            if (comfyProcess != null && !comfyProcess.HasExited)
            {
                CleanupComfyProcess(comfyProcess.Id);
            }
        }
    }
}
