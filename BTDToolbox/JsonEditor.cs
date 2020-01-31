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
        
        public static float jsonEditorFont;
        public string lastJsonFile;

        public JsonEditor(string Path)
        {
            InitializeComponent();
            Deserialize_Config();
            StartUp();
            this.Path = Path;
            this.FormClosed += exitHandling;

            FileInfo info = new FileInfo(Path);
            this.Text = info.Name;            
            this.Find_TextBox.Visible = false;
            this.FindNext_Button.Visible = false;
            this.toolStripSeparator2.Visible = false;
            this.Replace_TextBox.Visible = false;
            this.ReplaceDropDown.Visible = false;
            string formattedText = "";
            string unformattedText = File.ReadAllText(Path);

            try
            {
                JToken jt = JToken.Parse(unformattedText);
                formattedText = jt.ToString(Formatting.Indented);
                Editor_TextBox.Text = formattedText;
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_valid;
            }
            catch (Exception)
            {
                Editor_TextBox.Text = unformattedText;
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_Invalid;
            }
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
            try
            {
                JObject.Parse(this.Editor_TextBox.Text);
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_valid;
            }
            catch (Exception)
            {
                this.lintPanel.BackgroundImage = Properties.Resources.JSON_Invalid;
            }
            if (Editor_TextBox.Text == "")
            {
                AddLineNumbers();
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
            // create & set Point pt to (0,0)
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1
            int First_Index = Editor_TextBox.GetCharIndexFromPosition(pt);
            int First_Line = Editor_TextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1
            int Last_Index = Editor_TextBox.GetCharIndexFromPosition(pt);
            int Last_Line = Editor_TextBox.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox
            tB_line.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value
            tB_line.Text = "";
            tB_line.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line
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
            int index = Editor_TextBox.SelectionStart - 1;
            char[] ch = Editor_TextBox.Text.ToCharArray();//.IndexOf(index);
            char selectedText;
            findNextPhrase = true;
            endEditor = Editor_TextBox.Text.Length;
            startPosition = Editor_TextBox.SelectionStart;//+ 1;

            if (index < 0)
                index = 0;
            else if (index > Editor_TextBox.Text.Length)
                index = Editor_TextBox.Text.Length;
            selectedText = ch[index];
            string searchText = "";

            switch (selectedText)
            {
                case '[':
                    searchText = "]";
                    break;
                case ']':
                    searchText = "[";
                    break;
                case '{':
                    searchText = "}";
                    break;
                case '}':
                    searchText = "{";
                    break;
                case '(':
                    searchText = ")";
                    break;
                case ')':
                    searchText = "(";
                    break;
            }            

            if (previousSearchPhrase != searchText)
            {
                endPosition = 0;
                numPhraseFound = 0;
            }
            for (int i = 0; i < endEditor; i = startPosition)
            {
                previousSearchPhrase = selectedText.ToString();
                isCurrentlySearching = true;
                if (i == -1 || startPosition >= endEditor || startPosition <= 0)
                {
                    isCurrentlySearching = false;
                    MessageBox.Show("i = -1");
                    break;
                }
                startPosition = Editor_TextBox.Find(searchText, startPosition, endEditor, RichTextBoxFinds.None);
                if (startPosition >= 0)
                {
                    //we found the character, lets make sure its the match

                    int duplicate = Editor_TextBox.Find(selectedText.ToString(), index, startPosition, RichTextBoxFinds.None);
                    if (duplicate < endPosition)
                    {
                        index = startPosition + 1;
                        startPosition = startPosition + 1;
                    }
                    else
                    {
                        findNextPhrase = false;
                        numPhraseFound++;
                        Editor_TextBox.SelectionColor = Color.White;       //saving this value for later use
                        endPosition = searchText.Length;
                        startPosition = startPosition + endPosition;
                        break;
                    }

                    
                }
            }

        }
        private int CustomIndexOf(string source, char toFind, int min, int max)
        {
            int index = -1;
            for (int i = min; i < max; i++)
            {
                index = source.IndexOf(toFind, index + 1);

                if (index == -1)
                    break;
            }

            return index;
        }
    }
}
