using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Life
{
    class Visualizer
    {
        World world;
        Form1 form;

        Pen pen;
        Graphics buf;
        Graphics graphic;
        Bitmap bitmap;
        public int len;
        //TimeSpan maxTime;

        public Visualizer(World world, Form1 form)
        {
            this.world = world;
            this.form = form;
            pen = new Pen(new SolidBrush(Color.Black), 1);
            bitmap = new Bitmap(this.form.pictureBox1.Width, this.form.pictureBox1.Height);
            buf = Graphics.FromImage(bitmap);
            graphic = this.form.pictureBox1.CreateGraphics();
            len = this.form.pictureBox1.Width / world.size;

            //maxTime = DateTime.Now - DateTime.Now;
        }

        public void drawGrid()
        {
            buf.Clear(Color.Silver);
            for (int i = 0; i < this.form.pictureBox1.Width; i += len)
            {
                buf.DrawLine(pen, 0, i, this.form.pictureBox1.Height, i);
            }
            for (int j = 0; j < this.form.pictureBox1.Height; j += len)
            {
                buf.DrawLine(pen, j, 0, j, this.form.pictureBox1.Height);
            }
            graphic.DrawImage(bitmap, 0, 0);
        }

        public void draw()
        {
            //DateTime start = DateTime.Now;
            drawGrid();

            foreach (WorldObject wo in world.objects)
            {
                if (wo is Herbivorous)
                {
                    buf.DrawImage(Life.Properties.Resources.herbivorous, len * wo.x, len * wo.y, len, len);
                    //buf.DrawEllipse(new Pen(new SolidBrush(Color.Green), 5), len * wo.x, len * wo.y, len, len);
                    buf.DrawString(((Animal)wo).type.ToString() + " " + ((Animal)wo).health.ToString(), new Font("Arial", 10), new SolidBrush(Color.Red), len * wo.x, len * wo.y);
                    buf.DrawString(((Animal)wo).age.ToString(),new Font("Arial", 10), new SolidBrush(Color.Red), len * wo.x, len * wo.y + 10);
                }
                if (wo is Predator)
                {
                    buf.DrawImage(Life.Properties.Resources.predator, len * wo.x, len * wo.y, len, len);
                    //buf.DrawEllipse(new Pen(new SolidBrush(Color.Red), 5), len * wo.x, len * wo.y, len, len);
                    buf.DrawString(((Animal)wo).type.ToString() + " " + ((Animal)wo).health.ToString(), new Font("Arial", 10), new SolidBrush(Color.Yellow), len * wo.x, len * wo.y);
                    buf.DrawString(((Animal)wo).age.ToString(), new Font("Arial", 10), new SolidBrush(Color.Yellow), len * wo.x, len * wo.y + 10);
                }
                if (wo is Plant)
                {
                    buf.DrawImage(Life.Properties.Resources.plant, len * wo.x, len * wo.y, len, len);
                    //buf.DrawEllipse(new Pen(new SolidBrush(Color.Yellow), 5), len * wo.x, len * wo.y, len, len);
                }
            }
            graphic.DrawImage(bitmap, 0, 0);
            /*TimeSpan t = DateTime.Now - start;
            if (maxTime < t) maxTime = t;
            while (t < maxTime)
            {
                t = DateTime.Now - start;
            }*/
        }
    }
}
