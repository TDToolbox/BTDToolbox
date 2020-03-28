using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTDToolbox.Classes;
using System.IO;
using BTDToolbox.Extra_Forms;
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    public partial class JsonEditor_UserControl : UserControl
    {
        Font font;
        ConfigFile programData;
        public bool jsonError;
        public string path = "";

        //Tab new line vars
        string tab;
        bool tabLine = false;


        //Find vars
        int endEditor = 0;
        int endPosition = 0;
        int startPosition = 0;
        int numPhrasesFound = 0;
        bool searchPhrase_selected = false;
        string previousSearchPhrase = "";

        public JsonEditor_UserControl()
        {
            InitializeComponent();
            SetHideEvents();

            Editor_TextBox.Select();
            AddLineNumbers();

            Find_Button.KeyDown += Find_TB_KeyDown;
            Replace_TB.KeyDown += Find_TB_KeyDown;
            SearchOptions_Panel.KeyDown += Find_TB_KeyDown;
            SearchOptions_Button.KeyDown += Find_TB_KeyDown;
        }
        public void FinishedLoading()
        {
            HandleTools();
            if (Serializer.Deserialize_Config().JSON_Editor_FontSize > 0)
                Editor_TextBox.Font = new Font("Consolas", Serializer.Deserialize_Config().JSON_Editor_FontSize);
            else
                Editor_TextBox.Font = new Font("Consolas", 14);
            tB_line.Font = Editor_TextBox.Font;
            FontSize_TextBox.Text = Editor_TextBox.Font.Size.ToString();
        }
        //
        //JSON
        //
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
            CheckJSON(Editor_TextBox.Text);
            File.WriteAllText(path, Editor_TextBox.Text);

            if (tabLine)
            {
                Editor_TextBox.SelectedText = tab;
                tabLine = false;
            }
        }
        private void CheckJSON(string text)
        {
            if (JSON_Reader.IsValidJson(text) == true)
            {
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_valid;
                JsonError_Label.Visible = false;

                jsonError = false;
                New_JsonEditor.isJsonError = false;
            }
            else
            {
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_Invalid;
                JsonError_Label.Visible = true;

                jsonError = true;
                New_JsonEditor.isJsonError = true;
            }
        }
        private void LintPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (jsonError)
            {
                string error = JSON_Reader.GetJSON_Error(Editor_TextBox.Text);
                ConsoleHandler.force_appendNotice(error);
                //Line number
                string[] split = error.Split(',');
                string[] line = split[split.Length - 2].Split(' ');
                int lineNumber = Int32.Parse(line[line.Length - 1].Replace(".", "").Replace(",", ""));

                //Position in line
                string[] pos = split[split.Length - 1].Split(' ');
                int linePos = Int32.Parse(pos[pos.Length - 1].Replace(".", "").Replace(",", ""));

                //Scroll to the line above error
                int index = Editor_TextBox.GetFirstCharIndexFromLine(lineNumber - 3) + linePos;
                Editor_TextBox.Select(index, 1);
                Editor_TextBox.ScrollToCaret();

                int numChars = (Editor_TextBox.GetFirstCharIndexFromLine(lineNumber - 1)) - (Editor_TextBox.GetFirstCharIndexFromLine(lineNumber - 2));

                //highlight line with error
                index = Editor_TextBox.GetFirstCharIndexFromLine(lineNumber - 2) + linePos;
                Editor_TextBox.Focus();
                Editor_TextBox.Select(index, numChars-2);                
            }
        }
        private void CloseFile_Button_Click(object sender, EventArgs e)
        {
            if(!jsonError)
            {
                JsonEditorHandler.CloseFile(path);
                Serializer.SaveJSONEditor_Instance(this, programData);
            }
            else
            {
                
                DialogResult dialogResult = MessageBox.Show("This file has a JSON error! Are you sure you want to close and save it?", "ARE YOU SURE!!!!!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    JsonEditorHandler.CloseFile(path);
                    Serializer.SaveJSONEditor_Instance(this, programData);
                }
            }
        }


        //
        //EZ Tools and text formatting
        //
        private int IndentNewLines()
        {
            int index = Editor_TextBox.GetFirstCharIndexOfCurrentLine();
            string text = Editor_TextBox.Text.Remove(0, index);

            int numSpace = 0;
            foreach (char c in text)
            {
                if (c != ' ')
                    break;
                else
                    numSpace++;
            }
            return numSpace;
        }
        private void HandleTools()
        {
            if (path.EndsWith("tower"))
                EZTowerEditor_Button.Visible = true;
            else
                EZTowerEditor_Button.Visible = false;

            if (path.EndsWith("bloon"))
                EZBoon_Button.Visible = true;
            else
                EZBoon_Button.Visible = false;
        }

        //
        //Add line numbers
        //
        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1
            int line = Editor_TextBox.Lines.Length;
            if (line <= 99)
            {
                w = 20 + (int)Editor_TextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)Editor_TextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)Editor_TextBox.Font.Size;
            }

            return w;
        }
        public void AddLineNumbers()
        {
            Point pt = new Point(0, 0);
            int First_Index = Editor_TextBox.GetCharIndexFromPosition(pt);
            int First_Line = Editor_TextBox.GetLineFromCharIndex(First_Index);
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            int Last_Index = Editor_TextBox.GetCharIndexFromPosition(pt);
            int Last_Line = Editor_TextBox.GetLineFromCharIndex(Last_Index);
            tB_line.SelectionAlignment = HorizontalAlignment.Center;
            tB_line.Text = "";
            tB_line.Width = getWidth();
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                tB_line.Text += i + 1 + "\n";
            }
        }
        private void Editor_TextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = Editor_TextBox.GetPositionFromCharIndex(Editor_TextBox.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }
        private void Editor_TextBox_FontChanged(object sender, EventArgs e)
        {
            tB_line.Font = Editor_TextBox.Font;
            Editor_TextBox.Select();
            AddLineNumbers();
        }
        private void Editor_TextBox_VScroll(object sender, EventArgs e)
        {
            tB_line.Text = "";
            AddLineNumbers();
            tB_line.Invalidate();
        }
        private void TB_line_MouseDown(object sender, MouseEventArgs e)
        {
            Editor_TextBox.Select();
            tB_line.DeselectAll();
        }
        private void JsonEditor_UserControl_Resize(object sender, EventArgs e)
        {
            Editor_TextBox.Size = new Size(this.Width - 43, this.Height-38);
            tB_line.Size = new Size(tB_line.Width, this.Height - 38);

            Point pt = Editor_TextBox.GetPositionFromCharIndex(Editor_TextBox.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        //
        //Find and replace stuff
        //
        private void ShowSearchMenu(string op)
        {
            Editor_TextBox.Size = new Size(Editor_TextBox.Size.Width, Editor_TextBox.Size.Height - 40);
            tB_line.Size = new Size(tB_line.Size.Width, tB_line.Size.Height - 40);

            if(op == "find")
            {
                Find_TB.Location = new Point(Editor_TextBox.Width- 241, 5);
                Find_Button.Location = new Point(Editor_TextBox.Width - 321, 4);
                SearchOptions_Button.Location = new Point(Editor_TextBox.Width, 4);
                Find_TB.Visible = !Find_TB.Visible;
                Find_Button.Visible = !Find_Button.Visible;
                SearchOptions_Button.Visible = !SearchOptions_Button.Visible;

                Option1_CB.Text = "Subtask";
                Option1_CB.Location = new Point(15, Option1_CB.Location.Y);
                Find_Panel.Visible = !Find_Panel.Visible;
            }
            if (op == "replace")
            {
                Find_TB.Visible = !Find_TB.Visible;
                Find_Button.Visible = !Find_Button.Visible;
                SearchOptions_Button.Visible = !SearchOptions_Button.Visible;
                Replace_TB.Visible = !Replace_TB.Visible;
                Replace_Button.Visible = !ShowReplaceMenu_Button.Visible;

                Replace_TB.Location = new Point(Editor_TextBox.Width - 241, 5);
                Replace_Button.Location = new Point(Editor_TextBox.Width - 321, 4);
                Find_TB.Location = new Point(Editor_TextBox.Width - 591, 5);
                Find_Button.Location = new Point(Editor_TextBox.Width - 671, 4);
                SearchOptions_Button.Location = new Point(Editor_TextBox.Width, 4);

                Option1_CB.Text = "Replace all";
                Option1_CB.Location = new Point(10, Option1_CB.Location.Y);
                Find_Panel.Visible = !Find_Panel.Visible;
            }
            if (Find_TB.Visible)
                Find_TB.Focus();
            else
                Editor_TextBox.Focus();

            if (!Find_Panel.Visible)
            {
                Find_TB.Text = "";
                Replace_TB.Text = "";
                SearchOptions_Panel.Visible = false;
                Editor_TextBox.Size = new Size(Editor_TextBox.Size.Width, Editor_TextBox.Size.Height + 80);
                tB_line.Size = new Size(tB_line.Size.Width, tB_line.Size.Height + 80);
            }
        }
        private void Find_Button_Click(object sender, EventArgs e)
        {
            if (Find_TB.Text.Length > 0)
            {
                Editor_TextBox.Focus();
                FindText();
            }
            else
            {
                ConsoleHandler.appendLog("You didn't enter anything to search");
            }
        }
        private void FindOptions_Button_Click(object sender, EventArgs e)
        {
            SearchOptions_Panel.Visible = !SearchOptions_Panel.Visible;
        }
        private void FindText()
        {
            endEditor =  Editor_TextBox.Text.Length;
            startPosition = Editor_TextBox.SelectionStart + 1;

            if (previousSearchPhrase != Find_TB.Text)
            {
                startPosition = 0;
                endPosition = Find_TB.Text.Length;
                numPhrasesFound = 0;
            }
            for (int i = 0; i < endEditor; i = startPosition)
            {
                searchPhrase_selected = false;
                if (i == -1)
                {
                    MessageBox.Show("Reached the end of the file");
                    break;
                }
                if (startPosition >= endEditor)
                {
                    startPosition = 0;
                    break;
                }
                startPosition = Editor_TextBox.Find(Find_TB.Text, startPosition, endEditor, RichTextBoxFinds.None);
                if (startPosition >= 0)
                {
                    numPhrasesFound++;
                    searchPhrase_selected = true;

                    endPosition = this.Find_TB.Text.Length;
                    startPosition = startPosition + endPosition;
                    previousSearchPhrase = this.Find_TB.Text;
                    break;
                }
                if (numPhrasesFound == 0)
                {
                    MessageBox.Show("No Match Found!!!");
                    break;
                }
            }
        }
        private void Replace_Button_Click(object sender, EventArgs e)
        {
            if (Find_TB.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to search for. Please Try Again");
            }
            if (Replace_TB.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to replace with. Please Try Again");
            }
            if (Option1_CB.Checked)
            {
                Editor_TextBox.Text = Editor_TextBox.Text.Replace(Find_TB.Text, Replace_TB.Text);
            }
            else
            {
                if (!searchPhrase_selected)
                {
                    MessageBox.Show("You need to find something before you can try replacing it...");
                }
                else
                {
                    Editor_TextBox.Focus();
                    ReplaceText();
                }
            }
            
        }
        private void ReplaceText()
        {
            Editor_TextBox.Text = Editor_TextBox.Text.Remove(startPosition - endPosition, Find_TB.Text.Length);
            Editor_TextBox.Text = Editor_TextBox.Text.Insert(startPosition - endPosition, Replace_TB.Text);
            endPosition = this.Replace_TB.Text.Length;
            startPosition = startPosition + endPosition + Editor_TextBox.SelectionStart + 1;

            //startPosition = Editor_TextBox.SelectionStart + 1;
            FindText();
        }

        //
        //UI Events
        //
        private void SetHideEvents()
        {
            MouseClick += HideExtraPanels;
            Editor_TextBox.Click += HideExtraPanels;
            this.Click += HideExtraPanels;
            Find_TB.Click += HideExtraPanels;
            Replace_TB.Click += HideExtraPanels;
            Replace_Button.Click += HideExtraPanels;
            Find_Button.Click += HideExtraPanels;
            ShowReplaceMenu_Button.Click += HideExtraPanels;
        }
        private void HideExtraPanels(object sender, EventArgs e)
        {
            if (SearchOptions_Panel.Visible)
            {
                SearchOptions_Panel.Visible = false;
            }
        }
        private void Find_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                ShowSearchMenu("find");
            }
            if (e.Control && e.KeyCode == Keys.H)
            {
                ShowSearchMenu("replace");
            }
        }
        private void Editor_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tab = string.Concat(Enumerable.Repeat(" ", IndentNewLines()));
                tabLine = true;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                ShowSearchMenu("find");
            }
            if (e.Control && e.KeyCode == Keys.H)
            {
                ShowSearchMenu("replace");
            }
        }
        private void FontSize_TextBox_TextChanged(object sender, EventArgs e)
        {
            float FontSize = 0;
            float.TryParse(FontSize_TextBox.Text, out FontSize);
            if (FontSize < 3)
                FontSize = 3;

            Editor_TextBox.Font = new Font("Consolas", FontSize);
            tB_line.Font = new Font("Consolas", FontSize);
            Serializer.SaveJSONEditor_Instance(this, programData);
        }
        private void EZTowerEditor_Button_Click(object sender, EventArgs e)
        {
            var easyTower = new EasyTowerEditor();
            easyTower.path = path;
            easyTower.Show();
        }
        private void EZBoon_Button_Click(object sender, EventArgs e)
        {
            var ezBloon = new EZBloon_Editor();
            ezBloon.path = path;
            ezBloon.Show();
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {

        }

        private void ReplaceAllButton_DropDown_Click_1(object sender, EventArgs e)
        {

        }

        
    }
}
