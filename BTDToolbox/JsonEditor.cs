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

namespace BTDToolbox
{
    public partial class JsonEditor : Form
    {
        private string Path;

        public JsonEditor(string Path)
        {
            InitializeComponent();
            this.Path = Path;
            
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
    }
}
