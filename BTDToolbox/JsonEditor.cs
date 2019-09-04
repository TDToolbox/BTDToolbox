using Newtonsoft.Json;
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
        string livePath = Environment.CurrentDirectory;

        Window jsonEditor;
        string jsonEditorOutput;
        
        public JsonEditor(string Path)
        {
            InitializeComponent();
            this.FormClosed += exitHandling;
            this.Path = Path;

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
            int TabsMultiplier = 1;
            string Tabs = new string ('\t', TabsMultiplier);
            
            for (int i = 0; i < unformattedText.Length; i++)
            {
                if (unformattedText[i] == '{')
                {
                    TabsMultiplier = TabsMultiplier + 1;
                    formattedText = formattedText + unformattedText[i];
                }
                else if (unformattedText[i] == '}')
                {
                    TabsMultiplier = TabsMultiplier - 1;
                    formattedText = formattedText + unformattedText[i] + "\r\n";
                }
                else
                {
                    formattedText = formattedText + unformattedText[i];
                }
            }
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
            FontSize = textBox1.Font.Size;
        }

        private void JsonEditor_Load(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
        private void exitHandling(object sender, EventArgs e)
        {
            jsonEditor = new Window("Json Editor", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, jsonEditorFont);
            jsonEditorOutput = JsonConvert.SerializeObject(jsonEditor);

            StreamWriter writeJsonEditorForm = new StreamWriter(livePath + "\\config\\json_editor.json", false);
            writeJsonEditorForm.Write(jsonEditorOutput);
            writeJsonEditorForm.Close();
        }
    }
}
