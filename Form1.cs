using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace graphics_lab_five
{
    public partial class Form1 : Form
    {
        private bool _drawn = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            drawGraphics();
        }

        private void drawGraphics()
        {
            _drawn = true;
            this.Refresh();
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            int xOffset = 65;
            int xMultiplier = 30;
            int yOffset = 0;
            int yMultiplier = 10;
            Point[] point =
            {
                new Point(1 * xMultiplier + xOffset, 5 * yMultiplier + yOffset),
                new Point(3 * xMultiplier + xOffset, 2 * yMultiplier + yOffset),
                new Point(7 * xMultiplier + xOffset, 1 * yMultiplier + yOffset),
                new Point(11 * xMultiplier + xOffset, 2 * yMultiplier + yOffset),
                new Point(13 * xMultiplier + xOffset, 5 * yMultiplier + yOffset),
                new Point(11 * xMultiplier + xOffset, 8 * yMultiplier + yOffset),
                new Point(7 * xMultiplier + xOffset, 10 * yMultiplier + yOffset),
                new Point(3 * xMultiplier + xOffset, 8 * yMultiplier + yOffset)
            };
            g.DrawPolygon(new Pen(Color.Navy, 2), point);
            var fn = new Font("Segoe UI", 10, FontStyle.Bold);
            var sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString("Средний доход бюджета семи городов.", fn, Brushes.Firebrick, new Rectangle(125, 20, 300, 70), sf);
            g.DrawRectangle(new Pen(Color.Blue, 1), 0, 0,
                pictureBox1.Width - 1, pictureBox1.Height - 1);
            int xMin = 50;
            int xMax = pictureBox1.Width - 10;
            int yMin = 120;
            int yMax = pictureBox1.Height - 60;
            g.DrawLine(new Pen(Color.Black, 1), xMin, yMax,
                xMax, yMax);
            g.DrawLine(new Pen(Color.Black, 1), xMin, yMin,
                xMin, yMax);
            string[] cities =
            {
                "Владикавказ", "Воронеж", "Иркутск",
                "Тюмень", "Санкт-Петербург", "Магадан", "Москва"
            };
            int[] value = { 4600, 5850, 7850, 9000, 10650, 13800, 16150};
            int max = -1;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] > max) max = value[i];
            }

            double mash = 5.0;
            double dy = (yMax - yMin) / (max / mash);
            int widthRect = ((xMax - xMin) / value.Length) / 2;
            SolidBrush sb = new SolidBrush(Color.Coral);
            HatchBrush hb = new HatchBrush(HatchStyle.BackwardDiagonal,
                Color.BlueViolet, Color.Turquoise);
            Image img = Image.FromFile("cobblestone.png");
            TextureBrush tb = new TextureBrush(img);
            int x = xMin + widthRect;
            for (int i = 0; i < value.Length; i++)
            {
                Rectangle rect = new Rectangle(x, yMax - (int) (dy * (value[i] /
                                                                         mash)), widthRect,
                    (int) (dy * (value[i] / mash)));
                if (i < 3) g.FillRectangle(sb, rect);
                if ((i >= 3) && (i < 6)) g.FillRectangle(hb, rect);
                if ((i >= 6) && (i < 8)) g.FillRectangle(tb, rect);
                g.DrawRectangle(new Pen(Color.Black, 1), rect);
                x += 2 * widthRect;
            }

            Pen p = new Pen(Color.Orchid, 2);
            p.DashStyle = DashStyle.Dash;
            fn = new Font("Segoe UI", 8, FontStyle.Bold);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < value.Length; i++)
            {
                g.DrawLine(p, xMin - 5, yMax - (int) (dy * (value[i] /
                                                                    mash)), xMax,
                    yMax - (int) (dy * (value[i] / mash)));
                g.DrawString(value[i].ToString(), fn, Brushes.Black,
                    new Rectangle(1, yMax - (int) (dy * (value[i] / mash)) -
                                     (int) fn.Size, 50, 17), sf);
            }

            sf.Alignment = StringAlignment.Center;
            x = xMin + widthRect + widthRect / 2;
            for (int i = 0; i < cities.Length; i++)
            {
                g.DrawLine(new Pen(Color.Black, 1), x, yMax - 5, x,
                    yMax + 5);
                g.DrawString(cities[i], fn, Brushes.Black, new Rectangle(x - 75/2,
                    yMax, 75, 50), sf);
                x += 2 * widthRect;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (_drawn)
                drawGraphics();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString("Кликните мышкой по элементу PictureBox",
                new Font("Segoe UI", 14, FontStyle.Bold), Brushes.Crimson, 0, 0);
        }
    }
}