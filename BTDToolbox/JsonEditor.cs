using BTDToolbox.Classes;
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
    public partial class JsonEditor : ThemedForm
    {
        //Project variables
        string livePath = Environment.CurrentDirectory;
        public string Path;
        public string fileName;

        //Find and replace variables
        public int numPhraseFound;
        public int startPosition;
        public int endPosition;
        public int endEditor;
        public bool isCurrentlySearching;
        public string previousSearchPhrase;
        public bool isReplacing;
        public bool isFinding;
        public bool findNextPhrase;
        public static int maxLC = 1;

        //Congif variables
        //JsonEditor_Config jsonEditorConfig;
        ConfigFile programData;
        public static bool jsonError;
        
        public static float jsonEditorFont;
        public string lastJsonFile;

        public JsonEditor(string Path)
        {
            InitializeComponent();
            Deserialize_Config();
            StartUp();

            this.Path = Path;
            this.FormClosing += exitHandling;
            Editor_TextBox.MouseUp += Editor_TextBox_RightClicked;

            FileInfo info = new FileInfo(Path);
            this.Text = info.Name;            
            this.Find_TextBox.Visible = false;
            this.FindNext_Button.Visible = false;
            this.toolStripSeparator2.Visible = false;
            this.Replace_TextBox.Visible = false;
            this.ReplaceDropDown.Visible = false;

            //tabstops
            this.tB_line.TabStop = false;
            this.lintPanel.TabStop = false;
            this.Find_TextBox.AcceptsTab = false;
            this.Editor_TextBox.TabStop = true;
            string formattedText = "";

            string unformattedText = File.ReadAllText(Path);
            
            try
            {
                JToken jt = JToken.Parse(unformattedText);
                formattedText = jt.ToString(Formatting.Indented);
                Editor_TextBox.Text = formattedText;
            }
            catch (Exception)
            {
                Editor_TextBox.Text = unformattedText;
            }
            CheckJSON(this.Editor_TextBox.Text);

            this.FontSize_TextBox.TextChanged += new System.EventHandler(this.FontSize_TextBox_TextChanged);

            JsonProps.increment(this);
            this.Load += EditorLoading;
        }
        private void StartUp()
        {
            this.Size = new Size(programData.JSON_Editor_SizeX, programData.JSON_Editor_SizeY);
            this.Location = new Point(programData.JSON_Editor_PosX, programData.JSON_Editor_PosY);

            jsonEditorFont = programData.JSON_Editor_FontSize;
            Font newfont = new Font("Consolas", jsonEditorFont);
            tB_line.Font = newfont;
            Editor_TextBox.Font = newfont;
            FontSize_TextBox.Text = jsonEditorFont.ToString();
        }
        private void Deserialize_Config()
        {
            programData = Serializer.Deserialize_Config();
        }
        private void EditorLoading(object sender, EventArgs e)
        {
            tB_line.Font = Editor_TextBox.Font;
            Editor_TextBox.Select();
            AddLineNumbers();

            bool close = false;
            
            if(close)
            {
                this.Close();
            }
        }
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Path, Editor_TextBox.Text);
            CheckJSON(this.Editor_TextBox.Text);

            if (Editor_TextBox.Text == "")
            {
                AddLineNumbers();
            }
        }
        private void CheckJSON (string text)
        {
            if (ValidateJSON.IsValidJson(text) == true)
            {
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_valid;
                jsonError = false;
            }
            else
            {
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_Invalid;
                jsonError = true;
            }
        }
        private void FontSize_TextBox_TextChanged(object sender, EventArgs e)
        {
            float FontSize = 0;
            float.TryParse(FontSize_TextBox.Text, out FontSize);
            if (FontSize < 3)
                FontSize = 3;
            jsonEditorFont = FontSize;
            Font newfont = new Font("Consolas", jsonEditorFont);
            Editor_TextBox.Font = newfont;
            tB_line.Font = newfont;
        }
        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "json editor", programData);
            JsonProps.decrement(this);
        }
        private void Editor_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                ShowFindMenu();
            }
            if (e.Control && e.KeyCode == Keys.H)
            {
                ShowReplaceMenu();
            }
        }
        private void FindText()
        {
            if (Find_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to search for. Please Try Again");
            }
            else
            {
                findNextPhrase = true;
                endEditor = Editor_TextBox.Text.Length;

                startPosition = Editor_TextBox.SelectionStart + 1;

                if (previousSearchPhrase != Find_TextBox.Text)
                {
                    endPosition = 0;
                    numPhraseFound = 0;
                }
                for (int i = 0; i < endEditor; i = startPosition)
                {
                    previousSearchPhrase = this.Find_TextBox.Text;
                    isCurrentlySearching = true;
                    if (i == -1)
                    {
                        isCurrentlySearching = false;
                        break;
                    }
                    if (startPosition >= endEditor)
                    {
                        MessageBox.Show("Reached the end of the file");
                        break;
                    }
                    startPosition = Editor_TextBox.Find(Find_TextBox.Text, startPosition, endEditor, RichTextBoxFinds.None);
                    if (startPosition >= 0)
                    {
                        findNextPhrase = false;
                        numPhraseFound++;
                        //Editor_TextBox.SelectionColor = Color.Blue;       //saving this value for later use
                        endPosition = this.Find_TextBox.Text.Length;
                        startPosition = startPosition + endPosition;
                        break;
                    }

                    if (numPhraseFound == 0)
                    {
                        MessageBox.Show("No Match Found!!!");
                    }
                }
            }
            
        }
        private void ShowFindMenu()
        {
            isFinding = !isFinding;
            this.Find_TextBox.Visible = !this.Find_TextBox.Visible;
            this.FindNext_Button.Visible = !this.FindNext_Button.Visible;
            if (isReplacing)
            {
                isFinding = false;
                isReplacing = false;
                this.Find_TextBox.Visible = false;
                this.FindNext_Button.Visible = false;
                this.toolStripSeparator2.Visible = false;
                this.Replace_TextBox.Visible = false;
                this.ReplaceDropDown.Visible = false;
            }
        }
        private void ShowReplaceMenu()
        {
            isReplacing = !isReplacing;
            this.Find_TextBox.Visible = !this.Find_TextBox.Visible;
            this.FindNext_Button.Visible = !this.FindNext_Button.Visible;
            this.toolStripSeparator2.Visible = !this.toolStripSeparator2.Visible;
            this.Replace_TextBox.Visible = !this.Replace_TextBox.Visible;
            this.ReplaceDropDown.Visible = !this.ReplaceDropDown.Visible;
            if (isFinding)
            {
                isFinding = false;
                isReplacing = true;
                this.Find_TextBox.Visible = true;
                this.FindNext_Button.Visible = true;
                this.toolStripSeparator2.Visible = true;
                this.Replace_TextBox.Visible = true;
                this.ReplaceDropDown.Visible = true;
            }
        }
        private void ShowFindMenu_Button_Click_1(object sender, EventArgs e)
        {
            ShowFindMenu();
        }
        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            if (Find_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to search for. Please Try Again");
            }
            if (Replace_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to replace with. Please Try Again");
            }
            if (findNextPhrase)
            {
                MessageBox.Show("You need to find something before you can try replacing it...");
            }
            else if (findNextPhrase == false)
            {
                Editor_TextBox.Text = Editor_TextBox.Text.Remove(startPosition - endPosition, Find_TextBox.Text.Length);
                Editor_TextBox.Text = Editor_TextBox.Text.Insert(startPosition - endPosition, Replace_TextBox.Text);
                endPosition = this.Replace_TextBox.Text.Length;
                startPosition = startPosition + endPosition;

                endEditor = Editor_TextBox.Text.Length;

                startPosition = Editor_TextBox.SelectionStart + 1;

                if (previousSearchPhrase != Find_TextBox.Text)
                {
                    endPosition = 0;
                    numPhraseFound = 0;
                }

                for (int i = 0; i < endEditor; i = startPosition)
                {
                    previousSearchPhrase = this.Find_TextBox.Text;
                    isCurrentlySearching = true;
                    if (i == -1)
                    {
                        isCurrentlySearching = false;
                        break;
                    }
                    startPosition = Editor_TextBox.Find(Find_TextBox.Text, startPosition, endEditor, RichTextBoxFinds.None);
                    if (startPosition >= 0)
                    {
                        numPhraseFound++;
                        //Editor_TextBox.SelectionColor = Color.Blue;       //saving this value for later use
                        endPosition = this.Find_TextBox.Text.Length;
                        startPosition = startPosition + endPosition;
                        break;
                    }
                }
            }
        }
        private void ReplaceAllButton_DropDown_Click_1(object sender, EventArgs e)
        {
            if (Find_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to search for. Please Try Again");
            }
            else if (Replace_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to replace with. Please Try Again");
            }
            else
            {
                Editor_TextBox.Text = Editor_TextBox.Text.Replace(Find_TextBox.Text, Replace_TextBox.Text);
            }
        }
        private void FindNext_Button_Click(object sender, EventArgs e)
        {
            FindText();
        }
        private void JsonEditor_Load(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "json editor", programData);
        }

        private void ShowReplaceMenu_Button_Click(object sender, EventArgs e)
        {
            ShowReplaceMenu();
        }
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

        private void Editor_TextBox_VScroll(object sender, EventArgs e)
        {
            tB_line.Text = "";
            AddLineNumbers();
            tB_line.Invalidate();
        }

        private void Editor_TextBox_FontChanged(object sender, EventArgs e)
        {
            tB_line.Font = Editor_TextBox.Font;
            Editor_TextBox.Select();
            AddLineNumbers();
        }

        private void TB_line_MouseDown(object sender, MouseEventArgs e)
        {
            Editor_TextBox.Select();
            tB_line.DeselectAll();
        }

        private void Editor_TextBox_MouseClick(object sender, MouseEventArgs e)
        {
            //SearchForPairs();
        }
        private void SearchForPairs()
        {
            int duplicate = -1;
            string searchDirection = "";
            string searchText = "";

            int index = Editor_TextBox.SelectionStart;
            char[] ch = Editor_TextBox.Text.ToCharArray();
            char selectedText;
            int duplicatesFound = 0;

            endEditor = Editor_TextBox.Text.Length;
            startPosition = Editor_TextBox.SelectionStart;

            if (index - 1 < 0)
                index = 0;
            else if (index - 1 > Editor_TextBox.Text.Length)
                index = Editor_TextBox.Text.Length;
            selectedText = ch[index - 1];

            switch (selectedText)
            {
                case '[':
                    searchDirection = "down";
                    searchText = "]";
                    break;
                case ']':
                    searchDirection = "up";
                    searchText = "[";
                    break;
                case '{':
                    searchDirection = "down";
                    searchText = "}";
                    break;
                case '}':
                    searchDirection = "up";
                    searchText = "{";
                    break;
                case '(':
                    searchDirection = "down";
                    searchText = ")";
                    break;
                case ')':
                    searchDirection = "up";
                    searchText = "(";
                    break;
            }

            for (int i = 0; i < endEditor + 1; i = startPosition)
            {
                duplicate = -1;
                if (startPosition >= endEditor + 1 || i == -1)
                {
                    break;
                }
                else
                {
                    if (searchDirection == "down")
                    {
                        startPosition = Editor_TextBox.Find(searchText, index, endEditor + 1, RichTextBoxFinds.None);
                        if (startPosition >= 0)
                        {
                            //we found the character, lets make sure its the correct match to our pair
                            duplicate = Editor_TextBox.Find(selectedText.ToString(), index, startPosition + 1, RichTextBoxFinds.NoHighlight);
                            if (duplicate != -1)
                            {
                                duplicatesFound++;
                                index = duplicate + 1;
                            }
                            else
                            {
                                if (duplicatesFound != 0)
                                {
                                    index = startPosition + 1;
                                    duplicatesFound--;
                                }
                                else
                                    break;
                            }
                        }
                        else
                            break;
                    }
                    else
                    {
                        startPosition = Editor_TextBox.Find(searchText, 0, index, RichTextBoxFinds.Reverse);
                        if (startPosition >= 0)
                        {
                            //we found the character, lets make sure its the match
                            duplicate = Editor_TextBox.Find(selectedText.ToString(), startPosition, index - 1, RichTextBoxFinds.Reverse | RichTextBoxFinds.NoHighlight);
                            if (duplicate != -1)
                            {
                                duplicatesFound++;
                                index = duplicate - 1;
                            }
                            else
                            {
                                if (duplicatesFound != 0)
                                {
                                    index = startPosition - 1;
                                    duplicatesFound--;
                                }
                                else
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            if (JsonEditor.jsonError == true)
            {
                DialogResult dialogResult = MessageBox.Show("ERROR!!! There is a JSON Error in this file!!!\n\nIf you leave the file now it will be corrupted and WILL break your mod. Do you still want to leave?", "ARE YOU SURE!!!!!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        private void initSelContextMenu()
        {
            selMenu = new ContextMenuStrip();
            selMenu.Items.Add("Find");
            selMenu.Items.Add("Replace");
            selMenu.Items.Add("Get subtask number");
            selMenu.ItemClicked += jsonContextClicked;
        }
        private void Editor_TextBox_RightClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Selected.Count == 1)
                {
                    selMenu.Show(listView1, e.Location);
                }
                else if (Selected.Count == 0 || Selected == null)
                {

                    empMenu.Show(listView1, e.Location);
                }
                else if (Selected.Count > 1)
                {
                    multiSelMenu.Show(listView1, e.Location);
                }
            }
        }
    }
}
