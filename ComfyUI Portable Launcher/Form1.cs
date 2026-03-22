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
            string appDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string batPath = Path.Combine(appDirectory, "run_nvidia_gpu.bat");

            if (!File.Exists(batPath))
            {
                MessageBox.Show(
                    "ERROR: The file 'run_nvidia_gpu.bat' not found in this location!\n\n" +
                    "Please copy this EXE into your ComfyUI portable folder and try again.\n"+
                    "This EXE should be placed next to 'run_nvidia_gpu.bat'.",
                    "Wrong launch directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Application.Exit();
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = batPath,
                WorkingDirectory = appDirectory,
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
