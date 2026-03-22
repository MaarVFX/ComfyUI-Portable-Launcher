# ComfyUI Portable Launcher 🚀
A lightweight, open-source, single-window "Quality of Life" launcher for [ComfyUI Windows Portable](https://github.com/Comfy-Org/ComfyUI).

The Launcher makes your ComfyUI Windows Portable look and feel like the desktop version. 
No additional menus or toolbars, your ComfyUI Portable takes all the space it needs, and with no separate CMD window.

I'm not planning on developing it much further, I made it to help me organize my workspace, but if you have any suggestions how to streamline the worklow further, I might take a look.


## ✨ Features

* Single Window UI: The terminal and the the actual UI live in one window. 
* Smart Terminal: Displays the ComfyUI boot logs in a built-in terminal view. Once ready, it automatically switches to the viewer.
* Ultra Portable: A tiny, standalone app. Just drop it in and run.

## 🛠️ Installation & Usage

   1. Grab the latest **ComfyUI_Launcher.exe** from the [Releases page](https://github.com/MaarVFX/ComfyUI-Portable_Launcher/releases/tag/v1.0.0).
   2. Copy the .exe into your root _ComfyUI_windows_portable_ folder (the one containing _run_nvidia_gpu.bat_).
   3. Add the argument  `--disable-auto-launch` at the end of the line that launches ComfyUI in _run_nvidia_gpu.bat_ to prevent it from opening additional browser window. The line will look like this: `.\python_embeded\python.exe -s ComfyUI\main.py --windows-standalone-build --disable-auto-launch`
   4. Run
  
   
## 🖥️ Prerequisites

* Windows 10/11
* WebView2 Runtime (Pre-installed in Windows systems).
* .NET Core 3.1 Runtime (Should be already present too).

## ⚠️ Troubleshooting

* SmartScreen Warning: Since this is a specialized tool, Windows may flag it as "Unknown." Click More Info -> Run Anyway.
* Path Error: Ensure the launcher is in the same folder as _run_nvidia_gpu.bat_. It uses relative paths to keep your setup portable.


---
_Disclaimer: This is a 3rd-party launcher and is not affiliated with the official ComfyUI project._






