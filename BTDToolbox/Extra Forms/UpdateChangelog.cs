using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class UpdateChangelog : ThemedForm
    {
        public static bool recentUpdate;
        public UpdateChangelog()
        {
            InitializeComponent();
            
            WebClient client = new WebClient();
            string credText = client.DownloadString("https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20update%20changelog");
            string[] split = credText.Split('\n');

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(GeneralMethods.GetCenterScreen().X - (this.Width / 2), GeneralMethods.GetCenterScreen().Y - (this.Height / 2) - 145);


            int y = -10;
            foreach (string line in split)
            {
                Color color = Color.White;
                string text = line;
                int size = 20;
                int x = 30;
                int height = 0;
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                if (line.StartsWith("#1"))
                {
                    text = line.Substring(2);
                    size = 40;
                    height = 10;
                    x = 0;
                    lbl.Location = new Point(x, y + 10);
                }
                if (line.StartsWith("#L"))
                {
                    text = line.Substring(2);
                    color = Color.FromArgb(0, 100, 255);
                    lbl.MouseClick += (sender, eventArgs) =>
                    {
                        Process.Start(lbl.Text);
                    };
                }
                lbl.Font = new Font(FontFamily.GenericSansSerif, size / 2);
                lbl.Text = text;
                lbl.ForeColor = color;
                lbl.BringToFront();
                lbl.Width = contentPanel.Width;
                lbl.Height += height;
                base.contentPanel.Controls.Add(lbl);
                lbl.Show();
                y += size;
            }
            this.Size = new Size(this.Size.Width - 300, y + 25);
            
        }


    }
}
