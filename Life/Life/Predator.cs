using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class Predator
        : Animal
    {
        public Predator(int x, int y, double age, double maxAge, bool sex, int type)
            : base(x, y, age, maxAge, sex, type)
        {

        } 

        public override void eat()
        {
            this.hunger = 0.0;
            this.health = 100;
        }

        public override void growthOfHunger()
        {
            if (type == 1)
            {
                this.hunger = Math.Round(this.hunger + 6.4, 1);
                this.health = Math.Round(this.health - 7, 1);
            }
            if (type == 2)
            {
                this.hunger = Math.Round(this.hunger + 5.7, 1);
                this.health = Math.Round(this.hunger - 5, 1);
            }
            if (type > 2)
            {
                this.hunger = Math.Round(this.hunger + type + 2.13, 1);
                this.health = Math.Round(this.hunger - type - 2.21, 1);
            }
        }

        public override string ToString()
        {
            return "Predator " + base.ToString();
        }
    }
}
