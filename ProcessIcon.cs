using System;
using System.Diagnostics;
using System.Windows.Forms;
using OBStray.Properties;

namespace OBStray
{
	/// <summary>
	/// 
	/// </summary>
	class ProcessIcon : IDisposable
	{
		/// <summary>
		/// The NotifyIcon object.
		/// </summary>
		NotifyIcon ni;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessIcon"/> class.
		/// </summary>
		public ProcessIcon()
		{
			// Instantiate the NotifyIcon object.
			ni = new NotifyIcon();
		}

		/// <summary>
		/// Displays the icon in the system tray.
		/// </summary>
		public void Display()
		{
			// Put the icon in the system tray and allow it react to mouse clicks.			
            ni.Icon = Resources.default_icon;
			ni.Text = "OBStray";
			ni.Visible = true;

			// Attach a context menu.
            TrayApp mainApp = new TrayApp("ws://127.0.0.1:4444");
			ni.ContextMenuStrip = mainApp.Create();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		public void Dispose()
		{
			// When the application closes, this will remove the icon from the system tray immediately.
			ni.Dispose();
		}

	}
}