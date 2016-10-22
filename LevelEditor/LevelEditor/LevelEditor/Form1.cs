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

    public partial class Form1 : Form
    {
        int tileX = 0;
        int tileY = 0;

        Bitmap pictureBoxMap;
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            tileX = (e.X % 1000) / 50;
            tileY = e.Y / 50;
            Pen pen = new Pen(Color.Black);
            graphics.Clear(Color.White);
            for (int i = 0; i <= 1000; i += 50)
            {
                graphics.DrawLine(pen, i, 0, i, 1000);
                graphics.DrawLine(pen, 0, i, 1000, i);
            }
            Pen redPen = new Pen(Color.Red);
            graphics.DrawRectangle(redPen, tileX * 50, tileY * 50, 50, 50);
            pictureBox1.Refresh();
           
            

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
