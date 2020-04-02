using DgDecryptor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox.SaveEditor
{
    class SaveEditor
    {
        public static string savemodDir = Environment.CurrentDirectory + "\\Save mods";
        public static void DecryptSave(string game, string browsedSave)
        {
            string save = "";
            if (game == "BTD5")
                save = Serializer.Deserialize_Config().SavePathBTD5 + "\\Profile.save";
            if (game == "BTDB")
                save = Serializer.Deserialize_Config().SavePathBTDB + "\\Profile.save";

            if (browsedSave != "" && browsedSave != null)
            {
                save = browsedSave;
                game = "UnknownGame";
            }

            if (!Directory.Exists(savemodDir))
                Directory.CreateDirectory(savemodDir);

            FileInfo f = new FileInfo(save);

            if (!f.Exists)
            {
                ConsoleHandler.force_appendNotice("Save file does not exist... Unable to continue");
                return;
            }

            byte[] encoded = File.ReadAllBytes(f.FullName);

            JObject decoded = DgUtil.nk_decrypt(encoded);

            if (decoded == null)
            {
                ConsoleHandler.force_appendNotice("This file is not a valid JSON file... Unable to continue");
                return;
            }

            byte[] newBytes = Encoding.ASCII.GetBytes(decoded.ToString());

            File.WriteAllBytes(savemodDir + "\\" + game + "_Profile.save", newBytes);
            ConsoleHandler.force_appendLog_CanRepeat("Finished decrypting save file...");
        }

        public static void EncryptSave(string game)
        {
            string unencryptedSave = "";
            string encryptedSave = "";
            if (game == "BTD5")
            {
                unencryptedSave = savemodDir + "\\BTD5_Profile.save";
                encryptedSave = Serializer.Deserialize_Config().SavePathBTD5 + "\\Profile.save";
            }
            if (game == "BTDB")
            {
                unencryptedSave = savemodDir + "\\BTDB_Profile.save";
                encryptedSave = Serializer.Deserialize_Config().SavePathBTDB + "\\Profile.save";
            }
            if (game == "UnknownGame")
            {
                MessageBox.Show("Your save file doesn't have a specified game. Please choose where you want to save it...");
                unencryptedSave = savemodDir + "\\UnknownGame_Profile.save";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Export save file";
                sfd.Filter = "Save files (*.save)|*.save|All files (*.*)|*.*";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                encryptedSave = sfd.FileName;

                if (!encryptedSave.EndsWith(".save"))
                    encryptedSave = encryptedSave + ".save";
            }

            if (!Directory.Exists(savemodDir))
                Directory.CreateDirectory(savemodDir);


            FileInfo f = new FileInfo(unencryptedSave);

            if (!f.Exists)
            {
                ConsoleHandler.force_appendNotice("Save file does not exist... Unable to continue");
                return;
            }

            byte[] encoded_bytes = File.ReadAllBytes(f.FullName);
            string encoded_str = Encoding.ASCII.GetString(encoded_bytes);


            JObject decoded = JObject.Parse(encoded_str);

            if (decoded == null)
            {
                ConsoleHandler.force_appendNotice("This file is not a valid JSON file... Unable to continue");
                return;
            }


            if (game == "BTDB")
            {
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
            }

            byte[] encoded = DgUtil.nk_encrypt(decoded);

            if (encoded == null)
            {
                ConsoleHandler.force_appendNotice("Encryption failed for some reason...");
                return;
            }

            File.WriteAllBytes(encryptedSave, encoded);
            ConsoleHandler.force_appendLog_CanRepeat("Finished writing save file...");
        }
    }
}
