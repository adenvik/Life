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

        public void growthOfHunger()
        {
            this.hunger += 0.6;
        }
    }
}
