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
        private string reverseFileName;
        public string fileName;
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
                textBox1.Font = new Font("Microsoft Sans Serif", jsonEditorFont);

                toolStripTextBox1.Text = jsonEditorFont.ToString();
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
            textBox1.Text = formattedText;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Path, textBox1.Text);
        }

        private void ToolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            float FontSize = 0;
            float.TryParse(toolStripTextBox1.Text, out FontSize);
            try
            {
                textBox1.Font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Regular);
            } catch (Exception)
            {
                textBox1.Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Regular);
            }
        }

        private void JsonEditor_Load(object sender, EventArgs e)
        {

        }
        
        private void exitHandling(object sender, EventArgs e)
        {
            jsonEditor = new Window("Json Editor", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, textBox1.Font.Size);
            jsonEditorOutput = JsonConvert.SerializeObject(jsonEditor);

            StreamWriter writeJsonEditorForm = new StreamWriter(livePath + "\\config\\json_editor.json", false);
            writeJsonEditorForm.Write(jsonEditorOutput);
            writeJsonEditorForm.Close();
        }

        private void ToolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
