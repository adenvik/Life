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
            Creator creator = new Creator();
            creator.initialize(100);
            //Life life = new Life(new World((new Creator(100)).objects));      // <- создание жизни
            //Life life = new Life(new World(creator.objects));                 // <- создание жизни
            foreach (WorldObject w in creator.objects)                          // выводит созданные объекты
            {
                string info = "";
                if (w is AnimalDecotator)
                {
                    if (((AnimalDecotator)w).getAnimal())
                    {
                        //хищник
                        info = "Predator : age: " + ((Predator)((AnimalDecotator)w).animal).age + " sex: " + ((Predator)((AnimalDecotator)w).animal).sex;
                        if (w is Type1) info += " Type1";
                        else info += " Type2";
                        textBox2.Text = (int.Parse(textBox2.Text) + 1).ToString();
                    }
                    else
                    {
                        //Траводяное
                        info = "Herbivorous : age: " + ((Herbivorous)((AnimalDecotator)w).animal).age + " sex: " + ((Herbivorous)((AnimalDecotator)w).animal).sex;
                        if (w is Type1) info += " Type1";
                        else info += " Type2";
                        textBox3.Text = (int.Parse(textBox3.Text) + 1).ToString();
                    }
                }
                else
                {
                    info = "Plant : age: " + ((Plant)w).age;
                    textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
                }
                listView1.Items.Add(new ListViewItem(info));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
