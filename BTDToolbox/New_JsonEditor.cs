using BTDToolbox.Classes;
using BTDToolbox.Extra_Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    public partial class New_JsonEditor : ThemedForm
    {
        public static int JsonEditor_Width = 0;
        public static int JsonEditor_Height = 0;
        public static string selectedPath = "";
        public JsonEditor_UserControl[] userControls;
        public TabPage[] tabPages;
        public string[] tabFilePaths;
        public New_JsonEditor()
        {
            InitializeComponent();
            this.MdiParent = Main.getInstance();
            this.Font = new Font("Consolas", 11);
            JsonEditor_Width = tabControl1.Width;
            JsonEditor_Height = tabControl1.Height;

        }
        public void NewTab(string path)
        {
            Array.Resize(ref userControls, userControls.Length + 1);
            userControls[userControls.Length - 1] = new JsonEditor_UserControl();

            Array.Resize(ref tabPages, tabPages.Length + 1);
            tabPages[tabPages.Length - 1] = new TabPage();
            string[] split = path.Split('\\');
            string filename = split[split.Length - 1];
            tabPages[tabPages.Length - 1].Text = filename;

            Array.Resize(ref tabFilePaths, tabFilePaths.Length + 1);
            tabFilePaths[tabFilePaths.Length - 1] = path;

            tabPages[tabPages.Length - 1].Controls.Add(userControls[userControls.Length - 1]);
            AddText(path);
            
            this.tabControl1.TabPages.Add(tabPages[tabPages.Length - 1]);

            OpenTab(path);
        }
        private void AddText(string path)
        {
            string unformattedText = File.ReadAllText(path);

            try
            {
                JToken jt = JToken.Parse(unformattedText);
                string formattedText = jt.ToString(Formatting.Indented);
                userControls[userControls.Length - 1].Editor_TextBox.Text = formattedText;
            }
            catch (Exception)
            {
                userControls[userControls.Length - 1].Editor_TextBox.Text = unformattedText;
            }
            /*CheckJSON(userControls[userControls.Length - 1].Editor_TextBox.Text);
            s*/
        }
        public void OpenTab(string path)
        {
            int i = 0;
            foreach(string t in tabFilePaths)
            {
                if (t == path)
                {
                    tabControl1.SelectedTab = tabPages[i];
                    userControls[i].Size = new Size(tabControl1.SelectedTab.Width, tabControl1.SelectedTab.Height);
                }
                i++;
            }
        }
        private void New_JsonEditor_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Control && e.KeyCode == Keys.F)
            {
                FindReplace_Panel.Visible = !FindReplace_Panel.Visible;
                FindReplace_Panel.BringToFront();
            }*/
        }
        private void TabControl1_SizeChanged(object sender, EventArgs e)
        {
            if (userControls != null)
            {
                foreach (var x in userControls)
                {
                    x.Size = new Size(tabControl1.SelectedTab.Width, tabControl1.SelectedTab.Height);
                }
            }
        }

        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            //e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(40,40,40)), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            //TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, page.ForeColor);
            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, Color.White);
        }
        private string GetTabFilePath()
        {
            int i = 0;
            foreach (TabPage x in tabPages)
            {
                if (x.Text == tabControl1.SelectedTab.Text)
                {
                    selectedPath = tabFilePaths[i];
                }
                i++;
            }
            return selectedPath;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            GetTabFilePath();
            ConsoleHandler.appendLog_CanRepeat(selectedPath);
        }
    }
}
