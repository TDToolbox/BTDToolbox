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
using BTDToolbox.Classes;
using Sprite_Class;

namespace BTDToolbox.Extra_Forms
{
    public partial class SpriteVisualizer : Form
    {
        public string path { get; set; }

        //Tower_Class.Artist artist;
        //Tower_Class.Artist artist;
        SpriteReader sprite;
        Bitmap[] bm;
        Bitmap[] bm_processed;

        string[] bm_order;
        string[] spriteInfo;
        string[] spriteInfo_Texture;
        string textures_Dir = "";
        string textureFile_Path = "";
        public SpriteVisualizer()
        {
            InitializeComponent();
        }
        public void CreateSpriteObject(string path)
        {
            spriteInfo = new string[] { };
            spriteInfo_Texture = new string[] { };
            bm = new Bitmap[] { };
            bm_processed = new Bitmap[] { };
            bm_order = new string[] { };

            try
            {
                string json = File.ReadAllText(path);
                sprite = new SpriteReader();
                sprite = SpriteReader.FromJson(json);

                Visualizer_PictureBox.Image = null;
                ReadJSON();

                if (Sprites_ListBox.Items.Count > 0)
                    Sprites_ListBox.Items.Clear();
                CreateSprite();

                if (bm_processed.Length > 0)
                {
                    var c = ImageProcessing.CombineBitmap(bm_processed);
                    Visualizer_PictureBox.Image = c;
                }
            }
            catch
            {
                ConsoleHandler.append_CanRepeat("Something went wrong when trying to read that JSON file... Does it have invalid JSON?");
            }
        }
        private void CreateSprite()
        {
            string p = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON\\";// + path;
            string path = p + Folder_ComboBox.SelectedItem + "\\" + File_ComboBox.SelectedItem;
            sprite = SpriteReader.FromJson(File.ReadAllText(path));

            if (sprite.Actors != null)
            {
                int maxWidth = 0;
                int maxHeight = 0;
                //get the max size of canvas
                foreach (var m in bm)
                {
                    maxWidth = m.Width > maxWidth ? m.Width : maxWidth;
                    maxHeight = m.Height > maxHeight ? m.Height : maxHeight;
                }

                Point total_center = new Point(maxWidth / 2, maxHeight / 2);
                foreach (var a in sprite.Actors)
                {
                    int i = 0;
                    foreach(var b in bm_order)
                    {
                        if (b == a.Sprite)
                        {
                            Sprites_ListBox.Items.Add(b);
                            //ConsoleHandler.append_CanRepeat(a.Sprite.ToString());
                            
                            int posX_int = 0;
                            int posY_int = 0;
                            long posX_long = a.Position[1];
                            long posY_long = a.Position[0];
                            try
                            {
                                posX_int = Int32.Parse(posX_long.ToString());
                                posY_int = Int32.Parse(posY_long.ToString());
                            }
                            catch { ConsoleHandler.append_CanRepeat("One of the numbers being entered is not a valid number..."); };

                            Bitmap spr = new Bitmap(bm[i]);
                            spr.MakeTransparent();

                            /*width += spr.Width;
                            height = spr.Height > height ? spr.Height : height;*/




                            int x = 0;
                            int y = 0;
                            Point sprite_center = new Point(spr.Width / 2, spr.Height / 2);
                            switch (a.Alignment[0])
                            {
                                case 1:
                                    x = maxWidth;
                                    break;
                                case 2:
                                    //x = maxWidth;
                                    break;
                                default:
                                    x = total_center.X - (sprite_center.X);
                                    break;
                            }

                            switch (a.Alignment[1])
                            {
                                case 1:
                                    y = maxHeight;
                                    break;
                                case 2:
                                    //y = maxHeight;
                                    break;
                                default:
                                    y = total_center.Y - (sprite_center.Y / 2);
                                    break;
                            }
                            posX_int += x;
                            posY_int += y;

                            if (a.Flip == 1)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                posX_int += spr.Width;
                            }
                            if (a.Flip == 2)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipY);//.RotateNoneFlipX);
                                posY_int += spr.Height;
                            }
                            if (a.Flip == 3)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                posX_int += spr.Width;
                                posY_int += spr.Height;
                            }

                            

                            if (a.Shown == true)
                            {

                                System.Drawing.Bitmap finalImage = null;
                                finalImage = new System.Drawing.Bitmap(posX_int + spr.Width, posY_int + spr.Height);
                                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                                {
                                    g.Clear(System.Drawing.Color.Transparent);
                                    g.DrawImage(spr,
                                      new System.Drawing.Rectangle(posX_int, posY_int, spr.Width, spr.Height));
                                }
                                Array.Resize(ref bm_processed, bm_processed.Length + 1);
                                bm_processed[bm_processed.Length - 1] = finalImage;
                            }

                            /*if (a.Alignment[0] == 0 && a.Alignment[1] == 0)
                            {
                                *//*int x = 0;
                                int y = 0;
                                Point sprite_center = new Point(spr.Width / 2, spr.Height / 2);

                                x = total_center.X - (sprite_center.X);///2);// + posX_int);*//*
                                y = total_center.Y - (sprite_center.Y);///2);// + posY_int);


                                *//*if (posX_int < 0)
                                {
                                    posX_int = x - posX_int;
                                }
                                else
                                {
                                    posX_int += x;
                                }

                                if (posY_int < 0)
                                {
                                    posY_int = y - posY_int;
                                }
                                else
                                {
                                    posY_int += y;
                                }*//*
                                
                            }

                            *//*if (a.Flip == 1)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                posX += spr.Width;
                            }
                            if (a.Flip == 2)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipY);//.RotateNoneFlipX);
                                posY += spr.Height;
                            }
                            if (a.Flip == 3)
                            {
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                spr.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                posX += spr.Width;
                                posY += spr.Height;
                            }*/

                            /*width += spr.Width;
                            height = spr.Height > height ? spr.Height : height;
                            Point finalImage_center = new Point(posX + spr.Width/2, posY + spr.Height/2);
                            Point sprite_center = new Point(spr.Width/2,spr.Height/2);*/

                            
                        }
                        i++;
                    }
                    
                }
            }
        }
        private void ReadJSON()
        {
            GetSpriteInfo();
            FindTextureFile("InGame");
            ReadSpriteFile(FindTextureFile("InGame"));
        }
        private void GetSpriteInfo()
        {
            if(sprite.StageOptions != null)
            {
                ConsoleHandler.append_CanRepeat("Good sprite");
                foreach (SpriteInfo a in sprite.StageOptions.SpriteInfo)
                {
                    Array.Resize(ref spriteInfo, spriteInfo.Length + 1);
                    spriteInfo[spriteInfo.Length - 1] = a.SpriteInfoSpriteInfo;

                    Array.Resize(ref spriteInfo_Texture, spriteInfo_Texture.Length + 1);
                    spriteInfo_Texture[spriteInfo_Texture.Length - 1] = a.Texture;
                }
            }
            else
            {
                ConsoleHandler.append_CanRepeat("bad sprite");
            }
        }

        private void ReadSpriteFile(string textureFile_Name)
        {
            string[] lines = File.ReadAllLines(textureFile_Name);

            int i = 0;
            foreach (string line in lines)
            {
                foreach (string sInfo in spriteInfo)
                {
                    if (line.Contains(sInfo))
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

                        System.Drawing.Bitmap finalImage = null;
                        finalImage = new System.Drawing.Bitmap(aw, ah);

                        Bitmap image = null;
                        image = CropImage(source, section);
                        using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                        {
                            //set background color
                            g.Clear(System.Drawing.Color.Transparent);
                            g.DrawImage(image,
                                  new System.Drawing.Rectangle(ax, ay, image.Width, image.Height));
                            //offset += image.Width;
                        }
                        image.Save(Environment.CurrentDirectory + "\\Spri_" + i + ".png");


                        Array.Resize(ref bm, bm.Length + 1);
                        //bm[bm.Length - 1] = CropImage(source, section);
                        bm[bm.Length - 1] = finalImage;

                        Array.Resize(ref bm_order, bm_order.Length + 1);
                        bm_order[bm_order.Length - 1] = sInfo;
                    }
                }
                i++;
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
                    ConsoleHandler.append("XML File for sprite not detected...");
                }
                else if (allfiles.Length > 1)
                {
                    ConsoleHandler.append("Multiple XML files for sprite detected...");
                }
            }
            else
            {
                ConsoleHandler.append("Game directory not set!");
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
            if(Serializer.cfg.CurrentGame == "BTD5")
            {
                textures_Dir = Serializer.cfg.BTD5_Directory + "\\Assets\\Textures\\Ultra";
            }
            else
            {
                textures_Dir = Serializer.cfg.BTDB_Directory + "\\Assets\\Textures\\High";
            }
        }

        private void SpriteVisualizer_Shown(object sender, EventArgs e)
        {
            PopulateFolder_CB(path);
        }
        private void PopulateFolder_CB(string path)
        {
            var folders = Directory.GetDirectories(Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON");

            foreach (string folder in folders)
            {
                string[] split = folder.Split('\\');
                string foldername = split[split.Length - 1];
                Folder_ComboBox.Items.Add(foldername);
            }
        }
        private void PopulateFile_CB(string path)
        {
            File_ComboBox.Items.Clear();
            File_ComboBox.Text = "";

            string p = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON\\" + path;
            var files = Directory.GetFiles(p, "*", SearchOption.AllDirectories);
            foreach(string file in files)
            {
                string[] split = file.Split('\\');
                string filename = split[split.Length - 1];
                File_ComboBox.Items.Add(filename);
                
                if (File_ComboBox.Items.Count > 0)
                {
                    File_ComboBox.SelectedIndex = 0;
                }
            }
        }
        private void File_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string p = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON\\";

            if(Folder_ComboBox.Items.Count > 0 && Folder_ComboBox.SelectedItem != null)
            {
                if (File_ComboBox.Items.Count > 0 && File_ComboBox.SelectedItem != null)
                {
                    
                    if (spriteInfo != null && File_ComboBox.Items.Count == spriteInfo.Length)    //this wont work. Need to check if items are the same
                    {
                        ConsoleHandler.append_CanRepeat("File_ComboBox items: " + File_ComboBox.Items.Count);
                        ConsoleHandler.append_CanRepeat("SpriteInfo.Length: " + spriteInfo.Length);

                        bool sameSprites = true;
                        foreach(var a in spriteInfo)
                        {
                            //need to check to see if the elements are the same as before
                            //if they are, we don't need to make a new sprite
                            //if they are not the same, we need a new sprite
                            if(!File_ComboBox.Items.Contains(a))
                            {
                                sameSprites = false;
                                ConsoleHandler.append_CanRepeat("not same sprites");
                            }
                        }
                        if(sameSprites)
                        {
                            ConsoleHandler.append_CanRepeat("same sprites");
                            CreateSprite();
                        }
                    }
                    else
                    {
                        ConsoleHandler.append_CanRepeat("create new sprite object");
                        string y = p + Folder_ComboBox.SelectedItem + "\\" + File_ComboBox.SelectedItem;
                        CreateSpriteObject(y);
                    }
                }
            }
            
        }

        private void Folder_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateFile_CB(Folder_ComboBox.SelectedItem.ToString());
        }

        private void Reload_Button_Click(object sender, EventArgs e)
        {
            string p = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON\\";// + path;
            string path = p + Folder_ComboBox.SelectedItem + "\\" + File_ComboBox.SelectedItem;
            sprite = SpriteReader.FromJson(File.ReadAllText(path));

            if (Folder_ComboBox.Items.Count > 0 && Folder_ComboBox.SelectedItem != null)
            {
                if (File_ComboBox.Items.Count > 0 && File_ComboBox.SelectedItem != null)
                {
                    bool sameSprites = true;
                    foreach (var a in sprite.Actors)
                    {
                        if (!Sprites_ListBox.Items.Contains(a.Sprite.ToString()))
                        {
                            ConsoleHandler.append_CanRepeat("Missing a sprite");
                            sameSprites = false;
                            break;
                        }
                    }
                    if (sameSprites)
                    {
                        Sprites_ListBox.Items.Clear();
                        Visualizer_PictureBox.Image = null;

                        bm_processed = new Bitmap[] { };
                        CreateSprite();

                        var c = ImageProcessing.CombineBitmap(bm_processed);
                        Visualizer_PictureBox.Image = c;
                    }
                    else
                    {
                        bm_processed = new Bitmap[] { };
                        Sprites_ListBox.Items.Clear();
                        Visualizer_PictureBox.Image = null;

                        string y = p + Folder_ComboBox.SelectedItem + "\\" + File_ComboBox.SelectedItem;
                        CreateSpriteObject(y);
                    }
                }
            }
        }
    }
}
