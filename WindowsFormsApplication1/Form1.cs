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
            listView1.Items.Clear();
            textBox1.Text = (0).ToString();
            textBox2.Text = (0).ToString();
            textBox3.Text = (0).ToString();

            int countObj = 100;
            Creator creator = new Creator();
            creator.initialize(countObj);
            //Life life = new Life(new World((new Creator(100)).objects));                       // <- создание жизни
            Life life = new Life(new World(creator.objects, countObj / 10 + 5));                 // <- создание жизни
            /*foreach (WorldObject w in life.world.objects)                                         // выводит созданные объекты
            {
                if (w is AnimalDecotator)
                {
                    if (((AnimalDecotator)w).getAnimal())
                    {
                        //хищник
                        textBox2.Text = (int.Parse(textBox2.Text) + 1).ToString();
                    }
                    else
                    {
                        //Траводяное
                        textBox3.Text = (int.Parse(textBox3.Text) + 1).ToString();
                    }
                }
                else
                {
                    //Растение
                    textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
                }
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
