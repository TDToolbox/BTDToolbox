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

namespace BTDToolbox
{
    public partial class JsonEditor_UserControl : UserControl
    {
        Font font;
        public bool jsonError;
        public JsonEditor_UserControl()
        {
            InitializeComponent();

            font = new Font("Consolas", 14);
            Editor_TextBox.Font = font;
            tB_line.Font = Editor_TextBox.Font;
            Editor_TextBox.Select();
            AddLineNumbers();
        }

        //
        //JSON
        //
        private void Editor_TextBox_TextChanged(object sender, EventArgs e)
        {
            CheckJSON(Editor_TextBox.Text);
        }
        private void CheckJSON(string text)
        {
            if (JSON_Reader.IsValidJson(text) == true)
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
    }
}
