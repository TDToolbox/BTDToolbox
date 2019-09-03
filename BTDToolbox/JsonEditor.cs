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
            textBox1.Text = File.ReadAllText(Path);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(Path, textBox1.Text);
        }
    }
}
