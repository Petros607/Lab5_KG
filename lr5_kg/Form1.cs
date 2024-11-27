using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr5_kg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            s = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            m = new Pen(Color.White, 1); ;
            pictureBox1.Image = s;
        }
        Bitmap s;
        Pen m;
        double rad = Math.PI / 180;
        int radius = 70;
        //текущие координаты точки
        double x = 0;
        double y = 0;
        double z = 0;
        //значения сдвига
        double dx = 0;
        double dy = 0;
        double dz = 1;
        //углы для поворота
        double sx = 0;
        double sy = 0;
        double sz = 0;
        double k = 2;

        public void Figura()
        {
            Graphics g = Graphics.FromImage(s);
            g.Clear(Color.Black);
            Class1[,] fg = new Class1[46, 46];
            for (int i = 0; i < 46; i++)
            {
                for (int j = 0; j < 46; j++)
                {
                    // вычисление углов для вычисления трехмерных координат точек
                    double a = i * 4 * rad;
                    double b = j * 8 * rad;
                    // трехмерные координаты точек в трехмерном пространстве
                    x = radius * Math.Sin(a) * Math.Cos(b);
                    y = radius * Math.Sin(a) * Math.Sin(b);
                    if (radius * Math.Cos(a) > 0) z = 2 * k * radius * Math.Cos(a);
                    else z = k * radius * Math.Cos(a);


                    // масштабируем
                    x = x * dz + dx;
                    y = y * dz + dy;
                    // для вращения фигуры
                    double x0 = x, y0 = y, z0 = z;
                    Spinx(y0, z0);
                    y0 = y;
                    z0 = z;
                    Spiny(x0, z0);
                    x0 = x;
                    z0 = z;
                    Spinz(x0, y0);
                    x0 = x;
                    y0 = y;
                    // фигура в центре
                    x = x + pictureBox1.Width / 2;
                    y = y + pictureBox1.Height / 2;

                    fg[i, j] = new Class1(x, y, z);
                }
            }
            for (int i = 0; i < 45; i++)
                for (int j = 0; j < 45; j++)
                {
                    g.DrawLine(m, (int)fg[i, j].X, (int)fg[i, j].Y, (int)fg[i, j + 1].X, (int)fg[i, j + 1].Y);
                    g.DrawLine(m, (int)fg[i, j + 1].X, (int)fg[i, j + 1].Y, (int)fg[i + 1, j + 1].X, (int)fg[i + 1, j + 1].Y);
                    g.DrawLine(m, (int)fg[i + 1, j + 1].X, (int)fg[i + 1, j + 1].Y, (int)fg[i + 1, j].X, (int)fg[i + 1, j].Y);
                    g.DrawLine(m, (int)fg[i + 1, j].X, (int)fg[i + 1, j].Y, (int)fg[i, j].X, (int)fg[i, j].Y);
                }
            pictureBox1.Refresh();
        }
        // аксонометрическая проекция
        public void Spinx(double y0, double z0)
        {
            y = (y0) * Math.Cos(sx * rad) - (z0) * Math.Sin(sx * rad);
            z = (y0) * Math.Sin(sx * rad) + (z0) * Math.Cos(sx * rad);
        }
        public void Spiny(double x0, double z0)
        {
            x = (x0) * Math.Cos(sy * rad) + (z0) * Math.Sin(sy * rad);
            z = (-x0) * Math.Sin(sy * rad) + (z0) * Math.Cos(sy * rad);
        }
        public void Spinz(double x0, double y0)
        {
            x = (x0) * Math.Cos(sz * rad) - (y0) * Math.Sin(sz * rad);
            y = (x0) * Math.Sin(sz * rad) + (y0) * Math.Cos(sz * rad);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        // создаём фигуру
        private void Form1_Load(object sender, EventArgs e)
        {
            Figura();

        }

        // перемещение по х ++
        private void button2_Click(object sender, EventArgs e)
        {
            dx = dx - 10;
            dy = dy + 10;
            Figura();

        }
        // перемещение по у ++
        private void button3_Click(object sender, EventArgs e)
        {
            dx = dx + 10;
            dy = dy + 10;
            Figura();
        }
        // перемещение по z ++
        private void button4_Click(object sender, EventArgs e)
        {
            dy = dy - 10;
            Figura();

        }
        // перемещение по х --
        private void button5_Click(object sender, EventArgs e)
        {
            dx = dx + 10;
            dy = dy - 10;
            Figura();

        }
        // перемещение по у --
        private void button6_Click(object sender, EventArgs e)
        {
            dx = dx - 10;
            dy = dy - 10;
            Figura();
        }
        // перемещение по z --
        private void button7_Click(object sender, EventArgs e)
        {
            dy = dy + 10;
            Figura();

        }

        // вращение по у 
        private void button9_Click(object sender, EventArgs e)
        {
            sy = sy + 6;
            Figura();
        }
        // вращение по х
        private void button8_Click(object sender, EventArgs e)
        {
            sx = sx + 6;
            Figura();
        }
        // вращение по z 
        private void button10_Click(object sender, EventArgs e)
        {
            sz = sz + 6;
            Figura();
        }
        // масштабирование ++
        private void button11_Click(object sender, EventArgs e)
        {
            k += 1;
            if (dz <= 1) dz *= 2;
            else dz++;
            x *= dz;
            y *= dz;
            //z *= dz;
            Figura();
        }
        // масштабирование --
        private void button12_Click(object sender, EventArgs e)
        {
            k -= 1;
            if (dz <= 1) dz /= 2;
            else dz--;
            x /= dz;
            y /= dz;
            //z *= dz;
            Figura();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rad = Math.PI / 180;
            radius = 70;
            x = 0;
            y = 0;
            z = 0;
            dx = 0;
            dy = 0;
            dz = 1;
            sx = 0;
            sy = 0;
            sz = 0;
            k = 2;
            //MessageBox.Show("Фигура обновлена");
            Figura();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }

}

