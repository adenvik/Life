using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Animal : WorldObject
    {
        public bool sex { set; get; } //0 - Female 1 - Male
        public double hunger { set; get; } //Показатель голода животного

        public Animal(double age, bool sex) 
            : base(age)
        {
            this.hunger = 0.1;
            this.sex = sex;
        }

        public void eat(ref WorldObject wo)
        {

        }
    }
}
