using System;
using System.Diagnostics;
using System.Windows.Forms;

using phding.Bopomofo;

namespace phding.BopomofoExtension.App
{
    public class ContextMenus
    {

        public void SetTextModeText(bool isSimplifiedEnabled, bool invalidate)
        {
            if (this._textmodeItem != null)
            {

                if (isSimplifiedEnabled)
                {
                    this._textmodeItem.Text = "Simplified";
                }
                else
                {
                    this._textmodeItem.Text = "Traditional";
                }

                if (invalidate)
                {
                    this._textmodeItem.Invalidate();
                }
            }
        }

        private ToolStripMenuItem _textmodeItem;
        /// <summary>
        /// Is the About box displayed?
        /// </summary>
        bool isAboutLoaded = false;

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;


            this._textmodeItem = new ToolStripMenuItem();

            this._textmodeItem.Text = "Text mode";
            menu.Items.Add(this._textmodeItem);

            this.SetTextModeText(BopomofoRegistry.IsSimplifiedEnable(), false);

            // About.
            item = new ToolStripMenuItem();
            item.Text = "About";
            item.Click += new EventHandler(this.About_Click);
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(this.Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        /// <summary>
        /// Handles the Click event of the Explorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Explorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", null);
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void About_Click(object sender, EventArgs e)
        {
            if (!isAboutLoaded)
            {
                isAboutLoaded = true;
                new AboutBox().ShowDialog();
                isAboutLoaded = false;
            }
        }

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            Application.Exit();
        }
    }
}
