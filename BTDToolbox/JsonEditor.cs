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
        public static float jsonEditorFont;
        private string Path;
        public string fileName;
        public string searchPhrase;
        string livePath = Environment.CurrentDirectory;

        Window jsonEditor;
        string jsonEditorOutput;
        
        public JsonEditor(string Path)
        {
            InitializeComponent();

            this.Path = Path;
            this.FormClosed += exitHandling;

            FileInfo info = new FileInfo(Path);
            this.Text = info.Name;
            
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

            JToken jt = JToken.Parse(unformattedText);
            formattedText = jt.ToString(Formatting.Indented);
            Editor_TextBox.Text = formattedText;
        }

        private void JsonEditor_Load(object sender, EventArgs e)
        {

        }
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Path, Editor_TextBox.Text);
        }

        
        private void FindButton_Click(object sender, EventArgs e)
        {
            if (Find_TextBox.Text.Length <= 0)
            {
                MessageBox.Show("You didn't enter anything to search for. Please Try Again");
            }
            else
            {
                this.Find_TextBox.Text = searchPhrase;
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {

        }

        private void ReplaceAllButton_Click(object sender, EventArgs e)
        {
            
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

            StreamWriter writeJsonEditorForm = new StreamWriter(livePath + "\\config\\json_editor.json", false);
            writeJsonEditorForm.Write(jsonEditorOutput);
            writeJsonEditorForm.Close();
        }
    }
}
