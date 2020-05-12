using BTDToolbox.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.CreditViewer;

namespace BTDToolbox
{
    public delegate void CreditsEventHandler(object source, CreditsEventArgs e);
    public partial class CreditViewer : ThemedForm
    {
        public event CreditsEventHandler GotCredits;


        public CreditViewer()
        {
            InitializeComponent();
            this.canResize = false;
            this.moveCenterScreen = true;

            //this.TitleBar_LeftCorner.SendToBack();
            Sizer.Hide();
        }
        public CreditViewer(string creditText):this()
        {
            this.moveCenterScreen = true;
            InitCreditView(creditText);
            Show();
            this.Location = new Point(this.Location.X, this.Location.Y - 50);
        }

        public void InitCreditView(string creditText)
        {
            string[] split = creditText.Split('\n');

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
                if (line.StartsWith("#0"))
                {
                    text = line.Substring(2);
                    size = 50;
                    height = 15;
                    x = 0;
                    lbl.Location = new Point(x, y + 10);
                }
                if (line.StartsWith("#1"))
                {
                    text = line.Substring(2);
                    size = 40;
                    height = 10;
                    x = 0;
                    lbl.Location = new Point(x, y + 10);
                }
                if (line.StartsWith("#2"))
                {
                    text = line.Substring(2);
                    size = 20;
                    height = 10;
                    x = 0;
                    lbl.Location = new Point(x, y + 5);
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
            this.Size = new Size(this.Size.Width, y + 25);
        }


        public void StartCreditsView()
        {
            new Thread(() =>
            {
                WebHandler web = new WebHandler();
                string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/credits.tdc";
                string answer = web.WaitOn_URL(url);

                if (!Guard.IsStringValid(answer))
                {
                    ConsoleHandler.append("Failed to read update Changelog");
                    Serializer.cfg.recentUpdate = true;
                    Serializer.SaveSettings();
                    return;
                }

                if (GotCredits != null)
                {
                    GotCredits(this, new CreditsEventArgs(answer));
                }
            }).Start();
        }

        public class CreditsEventArgs : EventArgs
        {
            private string EventInfo;
            public CreditsEventArgs(string Text)
            {
                EventInfo = Text;
            }
            public string GetInfo()
            {
                return EventInfo;
            }
        }
    }
}
