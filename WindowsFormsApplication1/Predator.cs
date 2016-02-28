using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Predator : Animal
    {
        public Predator(double age, bool sex)
            : base(age, sex)
        {

        }

        public void eat(ref WorldObject wo)
        {
            if (wo is Herbivorous)
            {
                wo.health = 0;
                this.health = this.health + wo.health / 10;
                this.health = (this.health > 100) ? 100: this.health;
            }
            else
            {
                //сильно хочет ест
                if (this.hunger > 60)
                {
                    
                }
            }
        }
    }
}
