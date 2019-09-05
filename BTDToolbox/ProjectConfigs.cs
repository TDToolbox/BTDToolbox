using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox
{
    class ProjectConfigs
    {
        public class Window
        {
            public string WindowName { get; set; }
            public int SizeX { get; set; }
            public int SizeY { get; set; }
            public int PosX { get; set; }
            public int PosY { get; set; }
            public float FontSize { get; set; }
            public Window(string windowName, int sizeX, int sizeY, int posX, int posY, float fontSize)
            {
                WindowName = windowName;
                SizeX = sizeX;
                SizeY = sizeY;
                PosX = posX;
                PosY = posY;
                FontSize = fontSize;
            }

        }
        public class MainWindow : Window
        {
            public bool EnableConsole { get; set; }
            public MainWindow(string windowName, int sizeX, int sizeY, int posX, int posY, float fontSize, bool enableConsole) : base(windowName, sizeX, sizeY, posX, posY, fontSize)
            {
                WindowName = windowName;
                SizeX = sizeX;
                SizeY = sizeY;
                PosX = posX;
                PosY = posY;
                FontSize = fontSize;
                EnableConsole = enableConsole;
            }
        }
        public class JetExplorer : Window
        {
            public int TreeViewX { get; set; }
            public int TreeViewY { get; set; }
            public float TreeViewFontSize { get; set; }
            public JetExplorer(string windowName, int sizeX, int sizeY, int posX, int posY, float fontSize, int treeViewX, int treeViewY, float treeViewFontSize) : base(windowName, sizeX, sizeY, posX, posY, fontSize)
            {
                WindowName = windowName;
                SizeX = sizeX;
                SizeY = sizeY;
                PosX = posX;
                PosY = posY;
                FontSize = fontSize;
                TreeViewX = treeViewX;
                TreeViewY = treeViewY;
                TreeViewFontSize = treeViewFontSize;
            }
        }
    }
}
