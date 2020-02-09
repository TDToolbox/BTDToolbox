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

namespace BTDToolbox.Extra_Forms
{
    public partial class FlashReader : ThemedForm
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
        string formattedText = "";
        string unformattedText = "";
        int num_space_in_tab = 5;
        int num_of_tabs = 0;

        public FlashReader()
        {
            InitializeComponent();
            this.FormClosing += exitHandling;

            this.Find_TextBox.Visible = false;
            this.FindNext_Button.Visible = false;
            this.toolStripSeparator2.Visible = false;
            this.Replace_TextBox.Visible = false;
            this.ReplaceDropDown.Visible = false;
            

            //tabstops
            this.tB_line.TabStop = false;
            this.Find_TextBox.AcceptsTab = false;
            this.Editor_TextBox.TabStop = true;
            
            StartUp();
            this.Load += EditorLoading;
        }
        private void StartUp()
        {
            jsonEditorFont = 13;
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

            if (close)
            {
                this.Close();
            }
        }
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
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

        private void InputScript_Textbox_TextChanged(object sender, EventArgs e)
        {
            if(InputScript_Textbox.TextLength >= 3276799)
            {
                MessageBox.Show("The text you entered was TOO LONG!!!");
                ConsoleHandler.appendLog("You entered a text string that was too long");
            }
            else
            {
                unformattedText = InputScript_Textbox.Text;
                formattedText = FormatText(unformattedText);
                Editor_TextBox.Text = formattedText;
                NumOfRounds_Label.Text = "Num of rounds: " + Count("new RoundDef()", Editor_TextBox.Text);
            }
        }
        private string FormatText(string unformattedText)
        {
            ConsoleHandler.appendLog("Processing text...");
            string formattedText = "";
            num_of_tabs = 0;

            foreach (char c in unformattedText)
            {
                formattedText = formattedText + c;
                if (c == '{')
                {
                    num_of_tabs++;
                    formattedText = formattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                }
                else if (c == '[')
                {
                    num_of_tabs++;
                    formattedText = formattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                }
                else if (c == '}')
                {
                    num_of_tabs--;
                    formattedText = formattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                }
                else if (c == ']')
                {
                    num_of_tabs--;
                    formattedText = formattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                }
                else if (c == ')')
                {
                    formattedText = formattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                }
            }
            ConsoleHandler.appendLog("Finished Processing");
            return formattedText;
        }

        private void Compile_Button_Click(object sender, EventArgs e)
        {
            if (unformattedText.Length == 0)
                ConsoleHandler.appendLog("You need to input code before you can compile it...");
            else
            {
                unformattedText = CompileText(Editor_TextBox.Text);
                Clipboard.SetText(unformattedText);
                ConsoleHandler.appendLog("Copied code to clipboard");
            }
        }

        private string CompileText(string formattedText)
        {
            ConsoleHandler.appendLog("Compiling text...");
            string unformattedText = "";
            int skip = 0;
            num_of_tabs = 0;

            foreach (char c in formattedText)
            {
                if (skip > 0)
                {
                    skip--;
                }
                else
                {
                    unformattedText = unformattedText + c;
                    if (c == '{')
                    {
                        num_of_tabs++;
                        skip = (num_of_tabs * num_space_in_tab + 1);
                        //unformattedText = unformattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                    }
                    else if (c == '[')
                    {
                        num_of_tabs++;
                        skip = (num_of_tabs * num_space_in_tab + 1);
                        //unformattedText = unformattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                    }
                    else if (c == '}')
                    {
                        num_of_tabs--;
                        skip = (num_of_tabs * num_space_in_tab + 1);
                        //unformattedText = unformattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                    }
                    else if (c == ']')
                    {
                        num_of_tabs--;
                        skip = (num_of_tabs * num_space_in_tab + 1);
                        //unformattedText = unformattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                    }
                    else if (c == ')')
                    {
                        skip = (num_of_tabs * num_space_in_tab + 1);
                        //unformattedText = unformattedText + "\n" + string.Concat(Enumerable.Repeat(" ", (num_of_tabs * num_space_in_tab)));
                    }
                }
            }
            ConsoleHandler.appendLog("Finished compiling");


            return unformattedText;
        }

        private void CompileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unformattedText.Length == 0)
                ConsoleHandler.appendLog("You need to input code before you can compile it...");
            else
            {
                unformattedText = CompileText(Editor_TextBox.Text);
                Clipboard.SetText(unformattedText);
                ConsoleHandler.appendLog("Copied code to clipboard");
            }
        }

        private void Calc_NumOfRounds_button_Click(object sender, EventArgs e)
        {
            if (Editor_TextBox.TextLength > 0)
            {
                NumOfRounds_Label.Text = "Num of rounds: " + Count("new RoundDef()", Editor_TextBox.Text);
            }
            else
                ConsoleHandler.appendLog("You need to input code before you can calculate the number of rounds...");
        }

        private int Count(string searchTerm, string inputText)
        {
            string[] a = inputText.Split('.');

            // search for pattern in string 
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if(a[i].Contains(searchTerm))
                    count++;
            }
            return count;
        }

        private void BloonTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Console.getInstance().Visible == false)
            {
                Console.getInstance().Visible = true;
            }
            ConsoleHandler.appendLog("BLOON TYPES:\n>> Red:  0\n>> Blue:  1\n>> Green:  2\n>> Yellow:  3\n>> Pink:  4\n>> Black:  5\n>> White:  6\n>> Lead:  7\n>> Zebra:  8\n>> Rainbow:  9\n>> Ceramic:  10\n>> MOAB:  11\n>> BFB:  12\n>> ZOMG:  13");
        }
    }
}
