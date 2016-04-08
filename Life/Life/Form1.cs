using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Life
{
    public partial class Form1 : Form
    {
        _Life life;
        Thread lifeThread;
        bool alive = false;
        public Form1()
        {
            InitializeComponent();
            life = new _Life(new World(1), this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lifeThread == null)
                {
                    lifeThread = new Thread(_life);
                    lifeThread.Start();
                }
                if (!alive)
                {
                    alive = true;
                }
                if (!lifeThread.IsAlive)
                {
                    lifeThread = new Thread(_life);
                    alive = true;
                    lifeThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void _life()
        {
            //life = new _Life(new World(1), this);
            while (alive)
            {
                try
                {
                    life.iterLife();
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    richTextBox1.Text += ex.Message + "\n";
                    alive = false;
                    lifeThread = null;
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            life = new _Life(new World(1), this);
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int len = life.visualizer.len;
            int x = (int)(e.X / len);
            int y = (int)(e.Y / len);
            WorldObject obj = life.world.getObjectByXY(x,y);
            if (obj != null)
            {
                Info inf = new Info();
                inf.label1.Text += " " + obj.GetType();
                inf.label2.Text += " " + obj.x;
                inf.label3.Text += " " + obj.y;
                inf.label4.Text += " " + obj.age;
                inf.label5.Text += " " + obj.maxAge;
                inf.label6.Text += " " + obj.health;
                inf.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                if (obj is Plant)
                {
                    inf.pictureBox1.Image = Life.Properties.Resources.plant;
                    inf.label7.Visible = false;
                    inf.label8.Visible = false;
                    inf.label9.Visible = false;
                    inf.Size = new Size(287,188);
                    inf.Show();
                    return;
                }
                if (obj is Herbivorous) inf.pictureBox1.Image = Life.Properties.Resources.herbivorous;
                else inf.pictureBox1.Image = Life.Properties.Resources.predator;

                inf.label7.Text += " " + ((Animal)obj).sex;
                inf.label8.Text += " " + ((Animal)obj).hunger;
                inf.label9.Text += " " + ((Animal)obj).type;
                inf.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                life.iterLife();
            }
            catch (Exception ex)
            {
                richTextBox1.Text += ex.Message + "\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lifeThread.IsAlive) alive = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int len = life.visualizer.len;
            int x = (int)(e.X / len);
            int y = (int)(e.Y / len);
            WorldObject obj = life.world.getObjectByXY(x, y);
            if (obj != null)
            {
                Info inf = new Info();
                inf.label1.Text += " " + obj.GetType();
                inf.label2.Text += " " + obj.x;
                inf.label3.Text += " " + obj.y;
                inf.label4.Text += " " + obj.age;
                inf.label5.Text += " " + obj.maxAge;
                inf.label6.Text += " " + obj.health;
                inf.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                if (obj is Plant)
                {
                    inf.pictureBox1.Image = Life.Properties.Resources.plant;
                    inf.label7.Visible = false;
                    inf.label8.Visible = false;
                    inf.label9.Visible = false;
                    inf.Size = new Size(287, 188);
                    inf.Show();
                    return;
                }
                if (obj is Herbivorous) inf.pictureBox1.Image = Life.Properties.Resources.herbivorous;
                else inf.pictureBox1.Image = Life.Properties.Resources.predator;

                inf.label7.Text += " " + ((Animal)obj).sex;
                inf.label8.Text += " " + ((Animal)obj).hunger;
                inf.label9.Text += " " + ((Animal)obj).type;
                inf.Show();
            }
        }
    }
}
