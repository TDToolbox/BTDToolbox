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
using static BTDToolbox.ProjectConfigs;

namespace BTDToolbox
{
    public partial class JsonEditor : Form
    {
        string livePath = Environment.CurrentDirectory;
        private string Path;
        public string fileName;
        
        public int numPhraseFound;
        public int startPosition;
        public int endPosition;
        public int endEditor;
        public bool isCurrentlySearching;
        public string previousSearchPhrase;
        public bool isReplacing;
        public bool isFinding;
        public bool findNextPhrase = true;

        Window jsonEditor;
        string jsonEditorOutput;
        public static float jsonEditorFont;

        public JsonEditor(string Path)
        {
            InitializeComponent();

            JsonProps.increment(this);

            this.Path = Path;
            this.FormClosed += exitHandling;

            FileInfo info = new FileInfo(Path);
            this.Text = info.Name;

            this.Find_TextBox.Visible = false;
            this.FindNext_Button.Visible = false;
            this.toolStripSeparator2.Visible = false;
            this.Replace_TextBox.Visible = false;
            this.Replace_Button.Visible = false;

            try
            {
                string json = File.ReadAllText(livePath + "\\config\\json_editor.json");
                Window deserializedJsonEditor = JsonConvert.DeserializeObject<Window>(json);

                Size JsonEditorSize = new Size(deserializedJsonEditor.SizeX, deserializedJsonEditor.SizeY);
                this.Size = JsonEditorSize;

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(deserializedJsonEditor.PosX, deserializedJsonEditor.PosY);

                jsonEditorFont = deserializedJsonEditor.FontSize;
                Editor_TextBox.Font = new Font("Microsoft Sans Serif", jsonEditorFont);

                FontSize_TextBox.Text = jsonEditorFont.ToString();
            }
            catch (System.IO.FileNotFoundException)
            {
                jsonEditor = new Window("Json Editor", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, 10);
                jsonEditorOutput = JsonConvert.SerializeObject(jsonEditor);

                StreamWriter writeJsonEditorForm = new StreamWriter(livePath + "\\config\\json_editor.json", false);
                writeJsonEditorForm.Write(jsonEditorOutput);
                writeJsonEditorForm.Close();
            }
            catch (System.ArgumentException)
            {
                jsonEditorFont = 10;
            }

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

        }

        private void JsonEditor_Load(object sender, EventArgs e)
        {
            try
            {
                JToken.Parse(this.Editor_TextBox.Text);
                linticator.BackColor = Color.Green;
            }
            catch (Exception)
            {
                linticator.BackColor = Color.Red;
            }
        }
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Path, Editor_TextBox.Text);
            try
            {
                JToken.Parse(this.Editor_TextBox.Text);
                linticator.BackColor = Color.Green;
            }
            catch (Exception)
            {
                linticator.BackColor = Color.Red;
            }
        }
        private void FindButton_Click(object sender, EventArgs e)
        {
            FindText();
        }
        private void ReplaceButton_DropDown_Click(object sender, EventArgs e)
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
                        //findNextPhrase = false;
                        numPhraseFound++;
                        //Editor_TextBox.SelectionColor = Color.Blue;       //saving this value for later use
                        endPosition = this.Find_TextBox.Text.Length;
                        startPosition = startPosition + endPosition;
                        break;
                    }
                }
            }
            
        }
        private void ReplaceAllButton_DropDown_Click(object sender, EventArgs e)
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
        private void FontSize_TextBox_TextChanged(object sender, EventArgs e)
        {
            float FontSize = 0;
            float.TryParse(FontSize_TextBox.Text, out FontSize);
            try
            {
                Editor_TextBox.Font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Regular);
            }
            catch (Exception)
            {
                Editor_TextBox.Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Regular);
            }
        }
        private void exitHandling(object sender, EventArgs e)
        {
            jsonEditor = new Window("Json Editor", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, Editor_TextBox.Font.Size);
            jsonEditorOutput = JsonConvert.SerializeObject(jsonEditor);

            JsonProps.decrement(this);

            StreamWriter writeJsonEditorForm = new StreamWriter(livePath + "\\config\\json_editor.json", false);
            writeJsonEditorForm.Write(jsonEditorOutput);
            writeJsonEditorForm.Close();
        }
        private void ShowFindMenu_Button_Click(object sender, EventArgs e)
        {
            ShowFindMenu();
        }
        private void ShowReplaceMenu_Button_Click(object sender, EventArgs e)
        {
            ShowReplaceMenu();
        }
        private void Editor_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
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
                    this.Replace_Button.Visible = false;
                }
            }
            if (e.Control && e.KeyCode == Keys.R)
            {
                isReplacing = !isReplacing;
                this.Find_TextBox.Visible = !this.Find_TextBox.Visible;
                this.FindNext_Button.Visible = !this.FindNext_Button.Visible;
                this.toolStripSeparator2.Visible = !this.toolStripSeparator2.Visible;
                this.Replace_TextBox.Visible = !this.Replace_TextBox.Visible;
                this.Replace_Button.Visible = !this.Replace_Button.Visible;
                if (isFinding)
                {
                    isFinding = false;
                    isReplacing = true;
                    this.Find_TextBox.Visible = true;
                    this.FindNext_Button.Visible = true;
                    this.toolStripSeparator2.Visible = true;
                    this.Replace_TextBox.Visible = true;
                    this.Replace_Button.Visible = true;
                }
            }
        }
        private void Find_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindText();
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
                this.Replace_Button.Visible = false;
            }
        }
        private void ShowReplaceMenu()
        {
            isReplacing = !isReplacing;
            this.Find_TextBox.Visible = !this.Find_TextBox.Visible;
            this.FindNext_Button.Visible = !this.FindNext_Button.Visible;
            this.toolStripSeparator2.Visible = !this.toolStripSeparator2.Visible;
            this.Replace_TextBox.Visible = !this.Replace_TextBox.Visible;
            this.Replace_Button.Visible = !this.Replace_Button.Visible;
            if (isFinding)
            {
                isFinding = false;
                isReplacing = true;
                this.Find_TextBox.Visible = true;
                this.FindNext_Button.Visible = true;
                this.toolStripSeparator2.Visible = true;
                this.Replace_TextBox.Visible = true;
                this.Replace_Button.Visible = true;
            }
        }
    }
}
