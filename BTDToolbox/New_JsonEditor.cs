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

        ConfigFile programData;
        public New_JsonEditor()
        {
            InitializeComponent();
            this.MdiParent = Main.getInstance();
            this.Font = new Font("Consolas", 11);
            JsonEditor_Width = tabControl1.Width;
            JsonEditor_Height = tabControl1.Height;
            
            programData = Serializer.Deserialize_Config();
            this.Size = new Size(programData.JSON_Editor_SizeX, programData.JSON_Editor_SizeY);
            this.Location = new Point(programData.JSON_Editor_PosX, programData.JSON_Editor_PosY);

            /*jsonEditorFont = programData.JSON_Editor_FontSize;
            Font newfont = new Font("Consolas", jsonEditorFont);
            tB_line.Font = newfont;
            Editor_TextBox.Font = newfont;
            FontSize_TextBox.Text = jsonEditorFont.ToString();*/

        }
        public void NewTab(string path)
        {
            //handle user control stuff
            Array.Resize(ref userControls, userControls.Length + 1);
            userControls[userControls.Length - 1] = new JsonEditor_UserControl();

            //handle tab pages
            Array.Resize(ref tabPages, tabPages.Length + 1);
            tabPages[tabPages.Length - 1] = new TabPage();

            //handle file path array
            Array.Resize(ref tabFilePaths, tabFilePaths.Length + 1);
            tabFilePaths[tabFilePaths.Length - 1] = path;

            //create the tab and do required processing
            string[] split = path.Split('\\');
            string filename = split[split.Length - 1];
            tabPages[tabPages.Length - 1].Text = filename;
            tabPages[tabPages.Length - 1].Controls.Add(userControls[userControls.Length - 1]);
            userControls[userControls.Length - 1].path = path;

            AddText(path);
            this.tabControl1.TabPages.Add(tabPages[tabPages.Length - 1]);

            OpenTab(path);
            ConsoleHandler.appendLog_CanRepeat("Opened " + filename);
            userControls[userControls.Length - 1].FinishedLoading();
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

        //
        //Closing stuff
        //
        public void CloseTab(string path)
        {
            int i = tabControl1.SelectedIndex;

            //Remove the closed filepath
            int j = 0;
            string[] tempFilePaths = new string[tabFilePaths.Length - 1];
            foreach (string tf in tabFilePaths)
            {
                if (j != i)
                {
                    if(i == 0)
                    {
                        if(j == 0)
                        {
                            tempFilePaths[j] = tf;
                        }
                        else
                        {
                            tempFilePaths[j - 1] = tf;
                        }
                    }
                    else
                        tempFilePaths[j] = tf;
                }
                j++;
            }
            Array.Resize(ref tabFilePaths, tabFilePaths.Length - 1);
            Array.Copy(tempFilePaths, 0, tabFilePaths, 0, tempFilePaths.Length);


            //Remove the closed usercontrol
            j = 0;
            JsonEditor_UserControl[] tempUserControl = new JsonEditor_UserControl[userControls.Length - 1];
            foreach (JsonEditor_UserControl tf in userControls)
            {
                if (j != i)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            tempUserControl[j] = tf;
                        }
                        else
                        {
                            tempUserControl[j - 1] = tf;
                        }
                    }
                    else
                        tempUserControl[j] = tf;
                }
                j++;
            }
            Array.Resize(ref userControls, userControls.Length - 1);
            Array.Copy(tempUserControl, 0, userControls, 0, tempUserControl.Length);


            //Remove the closed tab page
            j = 0;
            TabPage[] tempTabPages = new TabPage[tabPages.Length - 1];
            foreach (TabPage tf in tabPages)
            {
                if (j != i)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            tempTabPages[j] = tf;
                        }
                        else
                        {
                            tempTabPages[j - 1] = tf;
                        }
                    }
                    else
                        tempTabPages[j] = tf;
                }
                j++;
            }
            Array.Resize(ref tabPages, tabPages.Length - 1);
            Array.Copy(tempTabPages, 0, tabPages, 0, tempTabPages.Length);

            if (tabControl1.TabPages.Count - 1 <= 0)
                this.Close();

            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            if(i == 0)
                tabControl1.SelectedIndex = i;
            else
                tabControl1.SelectedIndex = i - 1;
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

            //GetTabFilePath();
            ConsoleHandler.appendLog_CanRepeat("Tab Pages: " + tabPages.Length.ToString());
            ConsoleHandler.appendLog_CanRepeat("User Controls: " + userControls.Length.ToString());
            ConsoleHandler.appendLog_CanRepeat("File paths: " + tabFilePaths.Length.ToString());
        }

        private void New_JsonEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serializer.SaveConfig(this, "json editor", programData);
            JsonEditorHandler.jeditor = null;
        }
    }
}
