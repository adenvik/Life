using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class Herbivorous
        : Animal
    {
        public Herbivorous(int x, int y, double age, double maxAge, bool sex, int type)
            : base(x,y, age, maxAge, sex, type)
        {

        }

        public override void eat()
        {
            this.hunger = 0.0;
            this.health = 100;
        }

        public override void growthOfHunger()
        {
            if (type == 0)
            {
                this.hunger = Math.Round(this.hunger + 4, 1);
                this.health = Math.Round(this.health - 4, 1);
            }
            if (type == 1)
            {
                this.hunger = Math.Round(this.hunger + 3.3, 1);
                this.health = Math.Round(this.health - 3, 1);
            }
            if (type > 1)
            {
                this.hunger = Math.Round(this.hunger + type + 0.23, 1);
                this.health = Math.Round(this.health - type - 0.23, 1);
            }
        }

        public override string ToString()
        {
            return "Herbivorous " + base.ToString();
        }
    }
}
