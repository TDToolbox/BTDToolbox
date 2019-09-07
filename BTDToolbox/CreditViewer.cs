using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class CreditViewer : ThemedForm
    {
        public CreditViewer() : base()
        {
            InitializeComponent();

            WebClient client = new WebClient();
            string credText = client.DownloadString("https://raw.githubusercontent.com/TDToolbox/Credits/master/credits.tdc");
            string[] split = credText.Split('\n');

            int y = -10;
            foreach(string line in split)
            {
                string text = line;
                int size = 20;
                int x = 30;
                int height = 0;
                if (line.StartsWith("#1"))
                {
                    text = line.Substring(2);
                    size = 40;
                    height = 10;
                    x = 0;
                }
                Label lbl = new Label();
                lbl.Text = text;
                lbl.Font = new Font(FontFamily.GenericSansSerif, size/2);
                if (line.StartsWith("#1"))
                {
                    lbl.Location = new Point(x, y + 10);
                }
                else
                {
                    lbl.Location = new Point(x, y);
                }
                lbl.ForeColor = Color.White;
                lbl.BringToFront();
                lbl.Width = contentPanel.Width;
                lbl.Height += height;
                base.contentPanel.Controls.Add(lbl);
                lbl.Show();
                y += size;
            }
        }
    }
}
