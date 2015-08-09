using System;
using System.Diagnostics;
using System.Windows.Forms;

using phding.BopomofoExtension.App;
using phding.BopomofoExtension.App.Properties;

namespace BopomofoExtensionApp
{
    internal class ProcessIcon : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        private NotifyIcon ni;

        private ContextMenus _contextMenu;

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
            this.ni.MouseClick += new MouseEventHandler(this.ni_MouseClick);
            this.ni.Icon = Resources.SystemTrayIcon;
            this.ni.Text = Resources.ProcessIcon_Display_Bopomofo_Extension;
            this.ni.Visible = true;

            // Attach a context menu.
            this._contextMenu = new ContextMenus();
            this.ni.ContextMenuStrip = this._contextMenu.Create();
        }


        public void SetTextModeText(bool isSimplifiedEnable)
        {
            MethodInvoker method = delegate
            {
                this._contextMenu.SetTextModeText(isSimplifiedEnable, true);
                this.ni.ContextMenuStrip.Invalidate();
                this.ni.ContextMenuStrip.Update();
                this.ni.ContextMenuStrip.Refresh();
            };
            try
            {
                this.ni.ContextMenuStrip.Invoke(method);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            ni.Dispose();
        }

        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            // Handle mouse button clicks.
            if (e.Button == MouseButtons.Left)
            {
                // Start Windows Explorer.
                Process.Start("explorer", null);
            }
        }
    }
}
