using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Type1 : AnimalDecotator
    {
        public Type1(Animal animal)
            : base(animal)
        {

        }

        public override void growthOfHunger()
        {
            this.hunger += 4;
            if (this.getAnimal()) this.health -= 4.7;
            else this.health -= 4;
        }

        public override void eat(WorldObject wo)
        {
            this.animal.eat(wo);
        }
    }
}
