using DgDecryptor;
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

namespace BTDToolbox.SaveEditor
{
    public partial class SaveEditorTest : Form
    {
        public SaveEditorTest()
        {
            InitializeComponent();
        }

        private void Decrypt_Button_Click(object sender, EventArgs e)
        {
            if (pathBox.Text.Length < 1)
            {
                return;
            }

            FileInfo f = new FileInfo(pathBox.Text);

            if (!f.Exists)
            {
                return;
            }

            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Title = "Save";

            if (save_dialog.ShowDialog() != DialogResult.OK) return;

            byte[] encoded = File.ReadAllBytes(f.FullName);

            JObject decoded = DgUtil.nk_decrypt(encoded);

            if (decoded == null)
            {
                return;
            }

            byte[] newBytes = Encoding.ASCII.GetBytes(decoded.ToString());

            File.WriteAllBytes(save_dialog.FileName, newBytes);
        }

        private void Encrypt_Button_Click(object sender, EventArgs e)
        {
            if (pathBox.Text.Length < 1)
            {
                return;
            }

            FileInfo f = new FileInfo(pathBox.Text);

            if (!f.Exists)
            {
                return;
            }

            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Title = "Save";

            if (save_dialog.ShowDialog() != DialogResult.OK) return;

            byte[] encoded_bytes = File.ReadAllBytes(f.FullName);
            string encoded_str = Encoding.ASCII.GetString(encoded_bytes);



            JObject decoded = JObject.Parse(encoded_str);

            if (decoded == null)
            {
                return;
            }


            if (flag_bypass.Checked)
            {
                bool didBypass = false;
                int bypasses = 0;

                if (decoded["HigherVersionProfile"] != null && ((string)decoded["HigherVersionProfile"]).Length > 0)
                {
                    JObject HigherVersionProfile = JObject.Parse(decoded["HigherVersionProfile"] + "");

                    foreach (JProperty hvProp in HigherVersionProfile.Properties())
                    {
                        if (decoded[hvProp.Name] != null)
                        {
                            HigherVersionProfile[hvProp.Name] = decoded[hvProp.Name];
                        }
                    }

                    bypasses++;

                }

                if (decoded["DetectedHacks"] != null)
                {
                    decoded["DetectedHacks"] = 0;
                    bypasses++;

                }
                if (decoded["DateTime"] != null)
                {
                    decoded["DateTime"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    bypasses++;
                }

                flag_bypass.Text = "Battles Bypass [" + bypasses + "/3]";
            }

            byte[] encoded = DgUtil.nk_encrypt(decoded);

            if (encoded == null)
            {
                return;
            }

            File.WriteAllBytes(save_dialog.FileName, encoded);
        }

        private void Browse_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Title = "Browse";
            if (open_dialog.ShowDialog() != DialogResult.OK) return;

            if (open_dialog.CheckFileExists) pathBox.Text = open_dialog.FileName;
        }
    }
}
