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
using Sprite_Class;

namespace BTDToolbox.Extra_Forms
{
    public partial class SpriteVisualizer : Form
    {
        public string path { get; set; }

        SpriteReader sprite;
        PictureBox[] pb = new PictureBox[] { };
        ImageList imgList;
        string[] spriteInfo = new string[] { };
        string[] spriteInfo_Texture = new string[] { };
        string textures_Dir = "";
        string textureFile_Path = "";
        public SpriteVisualizer()
        {
            InitializeComponent();
            
        }
        public void CreateSpriteObject(string path)
        {
            string json = File.ReadAllText(path);
            sprite = SpriteReader.FromJson(json);
            ReadJSON();
        }
        private void ReadJSON()
        {
            pb = new PictureBox[spriteInfo.Length];

            GetSpriteInfo();
            FindTextureFile("InGame");
            ReadSpriteFile(FindTextureFile("InGame"));

        }
        private void GetSpriteInfo()
        {
            foreach (SpriteInfo a in sprite.StageOptions.SpriteInfo)
            {
                Array.Resize(ref spriteInfo, spriteInfo.Length + 1);
                spriteInfo[spriteInfo.Length - 1] = a.SpriteInfoSpriteInfo;

                Array.Resize(ref spriteInfo_Texture, spriteInfo_Texture.Length + 1);
                spriteInfo_Texture[spriteInfo_Texture.Length - 1] = a.Texture;
            }
        }

        private void ReadSpriteFile(string textureFile_Name)
        {
            
            string[] lines = File.ReadAllLines(textureFile_Name);

            foreach (string line in lines)
            {
                
                if (line.Contains("dart_monkey_body"))
                {
                    int x = 0;
                    int y = 0;
                    int w = 0;
                    int h = 0;
                    int ax = 0;
                    int ay = 0;
                    int aw = 0;
                    int ah = 0;

                    string[] split = line.Split('=');
                    //MessageBox.Show(split[2].Trim().Replace("\"", "").Replace("y", ""));
                    x = Int32.Parse(split[2].Trim().Replace("\"", "").Replace("y", ""));
                    y = Int32.Parse(split[3].Trim().Replace("\"", "").Replace("w", ""));
                    w = Int32.Parse(split[4].Trim().Replace("\"", "").Replace("h", ""));
                    h = Int32.Parse(split[5].Trim().Replace("\"", "").Replace("ax", ""));

                    ax = Int32.Parse(split[6].Trim().Replace("\"", "").Replace("ay", ""));
                    ay = Int32.Parse(split[7].Trim().Replace("\"", "").Replace("aw", ""));
                    aw = Int32.Parse(split[8].Trim().Replace("\"", "").Replace("ah", ""));
                    ah = Int32.Parse(split[9].Trim().Replace("\"", "").Replace("/>", ""));


                    Bitmap source = new Bitmap(textureFile_Path.Replace(".xml", ".png"));

                    Rectangle section = new Rectangle(new Point(x, y), new Size(w, h));
                    Bitmap CroppedImage = CropImage(source, section);
                    CroppedImage.MakeTransparent();

                    Array.Resize(ref pb, pb.Length + 1);
                    pb[pb.Length - 1] = new PictureBox();

                    Array.Resize(ref pb, pb.Length + 1);
                    pb[pb.Length - 1] = new PictureBox();

                    pb[pb.Length - 1].Size = new Size(w, h);
                    pb[pb.Length - 1].Image = CroppedImage;
                    //pb[pb.Length - 1].Visible = true;
                    pb[pb.Length - 1].Location = new Point(0, 0);
                    pb[pb.Length - 1].BackColor = Color.Transparent;

                    //pb[pb.Length - 1].SendToBack();
                    pb[pb.Length - 1].Parent = Visualizer_PictureBox;
                    
                    Visualizer_PictureBox.Controls.Add(pb[pb.Length - 1]);
                }
                else if (line.Contains("dart_monkey_arm_"))
                {
                    int x = 0;
                    int y = 0;
                    int w = 0;
                    int h = 0;
                    int ax = 0;
                    int ay = 0;
                    int aw = 0;
                    int ah = 0;

                    string[] split = line.Split('=');
                    //MessageBox.Show(split[2].Trim().Replace("\"", "").Replace("y", ""));
                    x = Int32.Parse(split[2].Trim().Replace("\"", "").Replace("y", ""));
                    y = Int32.Parse(split[3].Trim().Replace("\"", "").Replace("w", ""));
                    w = Int32.Parse(split[4].Trim().Replace("\"", "").Replace("h", ""));
                    h = Int32.Parse(split[5].Trim().Replace("\"", "").Replace("ax", ""));

                    ax = Int32.Parse(split[6].Trim().Replace("\"", "").Replace("ay", ""));
                    ay = Int32.Parse(split[7].Trim().Replace("\"", "").Replace("aw", ""));
                    aw = Int32.Parse(split[8].Trim().Replace("\"", "").Replace("ah", ""));
                    ah = Int32.Parse(split[9].Trim().Replace("\"", "").Replace("/>", ""));


                    Bitmap source = new Bitmap(textureFile_Path.Replace(".xml", ".png"));
                    source.MakeTransparent();
                    Rectangle section = new Rectangle(new Point(x, y), new Size(w, h));
                    Bitmap CroppedImage = CropImage(source, section);
                    CroppedImage.MakeTransparent();

                    Array.Resize(ref pb, pb.Length + 1);
                    pb[pb.Length - 1] = new PictureBox();
                    pb[pb.Length - 1].Location = new Point(0, 0);
                    pb[pb.Length - 1].BackColor = Color.Transparent;
                    pb[pb.Length - 1].Size = new Size(w, h);
                    pb[pb.Length - 1].Image = CroppedImage;
                    //pb[pb.Length - 1].Visible = true;

                    //pb[pb.Length - 1].SendToBack();
                    pb[pb.Length - 1].Parent = Visualizer_PictureBox;

                    Visualizer_PictureBox.Controls.Add(pb[pb.Length - 1]);
                }
            }
        }
   
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }

        private string FindTextureFile(string textureFile_Name)
        {
            if(textures_Dir != "")
            {
                var allfiles = Directory.GetFiles(textures_Dir, textureFile_Name + ".xml");
                if (allfiles.Length == 1)
                {
                    textureFile_Path = allfiles[0];
                    return textureFile_Path;
                }
                else if (allfiles.Length < 1)
                {
                    ConsoleHandler.appendLog("XML File for sprite not detected...");
                }
                else if (allfiles.Length > 1)
                {
                    ConsoleHandler.appendLog("Multiple XML files for sprite detected...");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Game directory not set!");
            }
            return "";
        }


        private void Bg_White_Button_Click(object sender, EventArgs e)
        {
            Visualizer_PictureBox.BackColor = Color.White;
        }

        private void Bg_Black_Button_Click(object sender, EventArgs e)
        {
            Visualizer_PictureBox.BackColor = Color.Black;
        }

        private void SpriteVisualizer_Load(object sender, EventArgs e)
        {
            if(Serializer.Deserialize_Config().CurrentGame == "BTD5")
            {
                textures_Dir = Serializer.Deserialize_Config().BTD5_Directory + "\\Assets\\Textures\\Ultra";
            }
            else
            {
                textures_Dir = Serializer.Deserialize_Config().BTDB_Directory + "\\Assets\\Textures\\High";
            }
        }

        private void SpriteVisualizer_Shown(object sender, EventArgs e)
        {
            imgList = new ImageList();
            imgList.TransparentColor = Color.White;
            CreateSpriteObject(path);
        }
    }
}
