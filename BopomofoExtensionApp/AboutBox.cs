using System;
using System.Reflection;
using System.Windows.Forms;

namespace phding.BopomofoExtension.App
{
    public partial class AboutBox : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        public AboutBox()
        {
            InitializeComponent();
            var version = ((AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyFileVersionAttribute))).Version;
            this.textBox2.Text = version;
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
    }
}
