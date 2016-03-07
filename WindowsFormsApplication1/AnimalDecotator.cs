using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    abstract class AnimalDecotator : Animal
    {
        public Animal animal { get; set; }

        public AnimalDecotator(Animal animal)
            : base(animal.age, animal.sex)
        {
            this.animal = animal;
        }

        abstract public void growthOfHunger();

        public bool getAnimal()
        {
            if (animal is Predator) return true;
            return false;
        }
    }
}
