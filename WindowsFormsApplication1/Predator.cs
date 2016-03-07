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

        public override void eat(WorldObject wo)
        {
            this.hunger = 0.0;
            this.health = this.health + wo.health / 10;
            this.health = (this.health > 100) ? 100 : this.health;
        }
    }
}
