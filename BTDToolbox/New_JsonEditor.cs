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
        public static bool isJsonError = false;
        public static string readOnlyName = "_original (READ-ONLY)";
        public Point mouseClickPos;

        public List<TabPage> tabPages;
        public List<string> tabFilePaths;
        public List<JsonEditor_Instance> userControls;

        ConfigFile programData;
        public New_JsonEditor()
        {
            InitializeComponent();
            //initSelContextMenu();
            tabControl1.MouseUp += Mouse_RightClick;

            this.MdiParent = Main.getInstance();
            this.Font = new Font("Consolas", 11);
            JsonEditor_Width = tabControl1.Width;
            JsonEditor_Height = tabControl1.Height;

            programData = Serializer.Deserialize_Config();
            this.Size = new Size(programData.JSON_Editor_SizeX, programData.JSON_Editor_SizeY);
            this.Location = new Point(programData.JSON_Editor_PosX, programData.JSON_Editor_PosY);
        }


        //
        //Open stuff
        //
        private void ContextClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = GetTabUnderMouse(mouseClickPos.X, mouseClickPos.Y);
            if (i != 9999)
            {
                if (e.ClickedItem.Text == "Close")
                {
                    CloseTab(tabFilePaths[i]);
                }
                else if (e.ClickedItem.Text == "View original")
                {
                    JsonEditorHandler.OpenOriginalFile(tabFilePaths[i]);
                }
                else if (e.ClickedItem.Text == "Restore to original")
                {
                    userControls[i].RestoreToOriginal();
                }
                else if (e.ClickedItem.Text == "Open in File Explorer")
                {
                    userControls[i].OpenInFileExplorer();
                }
            }
            
        }
        private void Mouse_RightClick(object sender, MouseEventArgs e)
        {
            mouseClickPos = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu.Show(tabControl1, e.Location);
            }
            else if (e.Button == MouseButtons.Middle)
            {
                int i = GetTabUnderMouse(mouseClickPos.X, mouseClickPos.Y);
                if (i != 9999)
                    CloseTab(tabFilePaths[i]);
            }
        }
        public void NewTab(string path)
        {
            if (path != "" && path != null)
            {
                tabFilePaths.Add(path);
                tabPages.Add(new TabPage());
                userControls.Add(new JsonEditor_Instance());

                //create the tab and do required processing

                string[] split = path.Split('\\');
                string filename = split[split.Length - 1];
                if (path.Contains("BackupProject"))
                {
                    filename = filename + readOnlyName;
                    userControls[userControls.Count - 1].Editor_TextBox.ReadOnly = true;
                }

                tabPages[tabPages.Count - 1].Text = filename;
                tabPages[tabPages.Count - 1].Controls.Add(userControls[userControls.Count - 1]);
                userControls[userControls.Count - 1].path = path;
                userControls[userControls.Count - 1].filename = filename;

                AddText(path);

                tabControl1.TabPages.Add(tabPages[tabPages.Count - 1]);
                
                OpenTab(path);
                ConsoleHandler.appendLog_CanRepeat("Opened " + filename);
                userControls[userControls.Count - 1].FinishedLoading();

            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("Something went wrong when trying to read the files path...");
            }
        }
        private void AddText(string path)
        {
            string unformattedText = File.ReadAllText(path);
            try
            {
                JToken jt = JToken.Parse(unformattedText);
                string formattedText = jt.ToString(Formatting.Indented);
                userControls[userControls.Count - 1].Editor_TextBox.Text = formattedText;
            }
            catch (Exception)
            {
                userControls[userControls.Count - 1].Editor_TextBox.Text = unformattedText;
            }
        }
        public void OpenTab(string path)
        {
            int i = 0;
            foreach (string t in tabFilePaths)
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
        //Methods
        //
        public int GetTabUnderMouse(int mouseX, int mouseY)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.GetTabRect(i).Contains(mouseX, mouseY))
                {
                    return i;
                }
            }
            return 9999;
        }
        //
        //Closing stuff
        //
        public void CloseTab(string path)
        {
            int i = tabFilePaths.IndexOf(path);
            int indexBeforeDelete = tabControl1.SelectedIndex;

            tabControl1.TabPages.Remove(tabPages[i]);
            if (indexBeforeDelete + 1 <= tabControl1.TabPages.Count)
                tabControl1.SelectedIndex = indexBeforeDelete;   
            else
                tabControl1.SelectedIndex = indexBeforeDelete - 1;

            tabFilePaths.RemoveAt(i);
            tabPages.RemoveAt(i);
            userControls.RemoveAt(i);

            if (tabControl1.TabPages.Count <= 0)
                this.Close();
        }
        private void New_JsonEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serializer.SaveConfig(this, "json editor", programData);
            Serializer.SaveJSONEditor_Tabs(programData);
            if(userControls.Count >0)
                Serializer.SaveJSONEditor_Instance(userControls[tabControl1.SelectedIndex], programData);
            JsonEditorHandler.jeditor = null;
        }
        private void Close_button_Click(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "json editor", programData);
            if (JsonEditorHandler.AreJsonErrors())
            {
                DialogResult diag = MessageBox.Show(tabControl1.SelectedTab.Text + " has a Json Error! Your mod will break if you don't fix it.\nClose anyways?", "WARNING!!", MessageBoxButtons.YesNo);
                if (diag == DialogResult.Yes)
                    this.Close();
            }
        }

        //
        //Other stuff
        //
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
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(40, 40, 40)), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            //TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, page.ForeColor);
            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, Color.White);
        }
    }
}
