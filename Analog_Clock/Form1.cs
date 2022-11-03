using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Timers;

namespace Analog_Clock
{
    public partial class Form1 : Form
    {
        Bitmap Clock_Face = Properties.Resources.clock_face;
        Bitmap Clock_Hour = Properties.Resources.clock_hour;
        Bitmap Clock_Minute = Properties.Resources.clock_minute;
        Bitmap Clock_Second = Properties.Resources.clock_second;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.TransparencyKey = this.BackColor;  //���ߐF�ɔw�i�F��ݒ�
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(10, 10);

            System.Drawing.Graphics g;

            pictureBox1.Dock = DockStyle.Fill;      // pictureBox��Form1�ɍ��킹�ēK�؂ȃT�C�Y�ɒ���
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;      // pictureBox���T�C�Y�䗦���ێ����Ċg��E�k��

            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox3.Dock = DockStyle.Fill;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox4.Dock = DockStyle.Fill;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            Clock_Face.MakeTransparent();
            Clock_Hour.MakeTransparent();
            Clock_Minute.MakeTransparent();
            Clock_Second.MakeTransparent();

            g = pictureBox2.CreateGraphics();
            g.DrawImage(Clock_Hour, new System.Drawing.Point(0, 0));
            g.Dispose();

            g = pictureBox3.CreateGraphics();
            g.DrawImage(Clock_Minute, new System.Drawing.Point(0, 0));
            g.Dispose();

            g = pictureBox4.CreateGraphics();
            g.DrawImage(Clock_Second, new System.Drawing.Point(0, 0));
            g.Dispose();

            pictureBox1.Image = Clock_Face;

            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox2;
            pictureBox4.Parent = pictureBox3;

            pictureBox2.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender_Temp, e_Temp) =>
            {
                Draw_Clock();
            };
            timer.Start();
            
        }

        //���v�̕\��
        private void Draw_Clock()
        {
            DateTime time = DateTime.Now;
            float HourAng = (float)((time.Hour + time.Minute / 60.0) * 30.0);
            float MinuteAng = (float)((time.Minute + time.Second / 60.0) * 6.0);
            float SecondAng = (float)(time.Second * 6.0);

            pictureBox2.Image = RotateBitmap(Clock_Hour, HourAng, Clock_Hour.Width / 2, Clock_Hour.Height / 2);
            pictureBox3.Image = RotateBitmap(Clock_Minute, MinuteAng, Clock_Minute.Width / 2, Clock_Minute.Height / 2);
            pictureBox4.Image = RotateBitmap(Clock_Second, SecondAng, Clock_Second.Width / 2, Clock_Second.Height / 2);


        }

        //���v�̐j�摜�̉�]
        public Bitmap RotateBitmap(Bitmap org_bmp, float angle, int x, int y)
        {
            Bitmap result_bmp = new Bitmap((int)org_bmp.Width, (int)org_bmp.Height);
            Graphics g = Graphics.FromImage(result_bmp);

            g.TranslateTransform(-x, -y);
            g.RotateTransform(angle, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.TranslateTransform(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(org_bmp, 0, 0);
            g.Dispose();

            return result_bmp;
        }

        //pictureBox1.Dispose();
        //pictureBox2.Dispose();
        //pictureBox3.Dispose();
        //pictureBox4.Dispose();
    }
}