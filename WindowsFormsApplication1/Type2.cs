using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Type2 : AnimalDecotator
    {
        public Type2(Animal animal)
            : base(animal)
        {

        }

        public override void growthOfHunger()
        {
            this.hunger += 2.5;
            if (this.getAnimal()) this.health -= 3.1;
            else this.health -= 2.2;
        }

        public override void eat(WorldObject wo)
        {
            this.animal.eat(wo);
        }
    }
}
