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

        public void growthOfHunger()
        {
            this.hunger += 1.2;
        }
    }
}
