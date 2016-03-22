using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(new SolidBrush(Color.Black), 2);
            SolidBrush bg = new SolidBrush(Color.Blue);

            Bitmap bbuffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gbuffer = Graphics.FromImage(bbuffer);
            Graphics g = pictureBox1.CreateGraphics();

            for (int i = 0; i < pictureBox1.Width; i += pictureBox1.Width / 10)
            {
                gbuffer.DrawLine(pen, 0, i, pictureBox1.Height, i);
            }
            for (int i = 0; i < pictureBox1.Height; i+= pictureBox1.Height / 10)
            {
                gbuffer.DrawLine(pen, i, 0, i, pictureBox1.Height);
            }
            g.DrawImage(bbuffer, 0, 0);



            listView1.Items.Clear();
            textBox1.Text = (0).ToString();
            textBox2.Text = (0).ToString();
            textBox3.Text = (0).ToString();

            int countObj = 100;
            Creator creator = new Creator();
            creator.initialize(countObj);
            //Life life = new Life(new World((new Creator(100)).objects));                   // <- создание жизни
            Life life = new Life(new World(creator.objects, countObj / 10));                 // <- создание жизни

            int iter = 0;
            while (true)
            {
                if (iter == 100)
                {
                    life.incYear();
                    iter = 0;
                }
                bool flag = life.iterLife(gbuffer);
                g.DrawImage(bbuffer, 0, 0);
                //MessageBox.Show("ris");
                if (!flag) break;
                iter++;
            }
            MessageBox.Show("Травоядные Никиты и хищники Никиты вымерли :(");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
