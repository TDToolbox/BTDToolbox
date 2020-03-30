using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BTDToolbox.Classes.Spritesheets
{
    class SpriteSheet_Handler
    {
        public void Extract(string filename, string elementName)
        {
            string[] split = filename.Split('\\');
            string[] fileSplit = split[split.Length - 1].Split('.');
            string file = fileSplit[0];
            string xmlPath = filename.Replace(split[split.Length-1], "") + "\\" + file + ".xml";

            StreamReader str = new StreamReader(xmlPath);
            //ConsoleHandler.appendLog_CanRepeat(str.ReadToEnd());
            XDocument xml = XDocument.Load(xmlPath);
            //XDocument xml = XDocument.Parse(str.ReadToEnd());

            var items = from item in xml.Descendants(elementName)
                        select new
                        {
                            Name = item.Attribute("name").Value,
                            Aw = Int32.Parse(item.Attribute("aw").Value),
                            Ah = Int32.Parse(item.Attribute("ah").Value),
                            Ax = Int32.Parse(item.Attribute("ax").Value),
                            Ay = Int32.Parse(item.Attribute("ay").Value),
                            W = Int32.Parse(item.Attribute("w").Value),
                            H = Int32.Parse(item.Attribute("h").Value),
                            X = Int32.Parse(item.Attribute("x").Value),
                            Y = Int32.Parse(item.Attribute("y").Value)
                        };

            Bitmap spritesheet = new Bitmap(filename);
            string extractpath = Environment.CurrentDirectory + "\\ExtractedSprites\\" + file;
            if (!Directory.Exists(extractpath))
                Directory.CreateDirectory(extractpath);
            foreach (var a in items)
            {
                if(a.Aw != 0 && a.Ah != 0)
                {
                    Rectangle srcRect = new Rectangle(a.X, a.Y, a.W, a.H);
                    Bitmap sprite = (Bitmap)spritesheet.Clone(srcRect, spritesheet.PixelFormat);

                    if (File.Exists(extractpath + "\\" + a.Name + ".png"))
                        File.Delete(extractpath + "\\" + a.Name + ".png");
                    sprite.Save(extractpath + "\\" + a.Name + ".png");
                    //Bitmap atlasImage = new Bitmap(filename);
                    //PixelFormat pixelFormat = atlasImage.PixelFormat;
                    //ExtractSprite2(atlasImage, pixelFormat, filename, file, a.Name, a.Aw, a.Ah, a.Ax, a.Ay, a.W, a.H, a.X, a.Y);
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
        private void ExtractSprite2(Bitmap atlasImage, PixelFormat pixelFormat, string spritesheetPath, string filename, string name, int aw, int ah, int ax, int ay, int w, int h, int x, int y)
        {
            string extractpath = Environment.CurrentDirectory + "\\ExtractedSprites\\" + filename + "\\" + name + ".png";
            try
            {
                var CroppedImage = new Bitmap(w, h, pixelFormat);
                // copy pixels over to avoid antialiasing or any other side effects of drawing
                // the subimages to the output image using Graphics
                for (int a = 0; a < w; a++)
                    for (int b = 0; b < h; b++)
                        CroppedImage.SetPixel(x, y, atlasImage.GetPixel(x + a, y + b));
                CroppedImage.Save(extractpath);
            }
            catch (Exception ex)
            {
                // handle the exception
            }
        }
        private void ExtractSprite(string spritesheetPath, string filename, string name, int aw, int ah, int ax, int ay, int w, int h, int x, int y)
        {
            Bitmap source = new Bitmap(spritesheetPath.Replace(".xml", ".png"));
            source.MakeTransparent();
            Rectangle section = new Rectangle(new Point(x, y), new Size(w, h));

            /*MessageBox.Show(aw.ToString());
            MessageBox.Show(ah.ToString());*/
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
            
            if (!Directory.Exists(Environment.CurrentDirectory + "\\ExtractedSprites\\" + filename))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\ExtractedSprites\\" + filename);
            string extractpath = Environment.CurrentDirectory + "\\ExtractedSprites\\" + filename + "\\" + name + ".png";
            if (File.Exists(extractpath))
                File.Delete(extractpath);

            image.Save(extractpath);
        }
        public void Compile()
        {

        }
    }
}
