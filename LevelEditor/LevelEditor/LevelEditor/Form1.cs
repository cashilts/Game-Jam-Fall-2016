using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor
{
    enum tileTypes { PLAIN='p', WATER='w', TREE='t', GRASS='g', HILL='h', MOUNTAIN='m' };
    struct tile
    { 
        public tileTypes tileType;
    };

    
    public partial class Form1 : Form
    {
        
        Dictionary<char, Color> tileColorLookup = new Dictionary<char, Color>();
        int tileX = 0;
        int tileY = 0;
        int mapSize = 20;
        tile[,] tiles;
        Bitmap pictureBoxMap;
        Graphics graphics;
        tileTypes toAssign = tileTypes.PLAIN;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tileColorLookup.Add('p', Color.LightGreen);
            tileColorLookup.Add('w', Color.Blue);
            tileColorLookup.Add('t', Color.Green);
            tileColorLookup.Add('m', Color.Gray);
            tileColorLookup.Add('g', Color.GreenYellow);
            tileColorLookup.Add('h', Color.Brown);

            label1.Text = "Assign mode: " + toAssign.ToString();

            tiles = new tile[20,20];
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j<20; j++)
                {
                    tile temp;
                    temp.tileType = tileTypes.PLAIN;
                    tiles[i, j] = temp;
                }
            }
            this.Size = new Size(1100, 1100);
            pictureBox1.Size = new Size(1050, 1050);
            pictureBoxMap = new Bitmap(1000, 1000);
            graphics = Graphics.FromImage(pictureBoxMap);
            pictureBox1.Image = pictureBoxMap;
            Pen pen = new Pen(Color.Black);
            for (int i = 0; i <= 1000; i += 50) {
                graphics.DrawLine(pen, i, 0, i, 1000);
                graphics.DrawLine(pen, 0, i, 1000, i);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.FindForm().Focus();
            tileX = (e.X % 1000) / 50;
            tileY = e.Y / 50;
            tiles[tileX, tileY].tileType = toAssign;
            Pen pen = new Pen(Color.Black);
            graphics.Clear(Color.White);
            for (int i = 0; i <= 1000; i += 50)
            {
                graphics.DrawLine(pen, i, 0, i, 1000);
                graphics.DrawLine(pen, 0, i, 1000, i);
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Color tileColor;
                    tileColorLookup.TryGetValue((char)tiles[i, j].tileType, out tileColor);
                    graphics.FillRectangle(new SolidBrush(tileColor), i * 50, j * 50, 50, 50);
                    graphics.DrawString(((char)tiles[i, j].tileType).ToString(), SystemFonts.DefaultFont, Brushes.White, i * 50, j * 50);
                }
                Pen redPen = new Pen(Color.Red);
                graphics.DrawRectangle(redPen, tileX * 50, tileY * 50, 50, 50);
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.P: toAssign = tileTypes.PLAIN; break;
                case Keys.W: toAssign = tileTypes.WATER; break;
                case Keys.G: toAssign = tileTypes.GRASS; break;
                case Keys.H: toAssign = tileTypes.HILL; break;
                case Keys.M: toAssign = tileTypes.MOUNTAIN; break;
                case Keys.T: toAssign = tileTypes.TREE; break;
            }

            label1.Text = "Assign mode: " + toAssign.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string[] lines = new string[20];
            for (int i = 0; i < 20; i++) {
                string toAdd = "";
                for (int j = 0; j < 20; j++) {
                    toAdd += ((char)tiles[i, j].tileType).ToString();
                }
                lines[i] = toAdd;
            }
            System.IO.File.WriteAllLines(saveFileDialog1.FileName, lines);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            String[] readData;
            readData = System.IO.File.ReadAllLines(openFileDialog1.FileName);
            for (int i = 0; i < 20; i++) {
                for(int j = 0; j<20; j++){
                    tiles[i,j].tileType = (tileTypes)readData[i].ElementAt(j);
                }
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(sender, e);
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(sender, e);
        }
    }
}
