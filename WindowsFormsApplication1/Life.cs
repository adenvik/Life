using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Life
    {
        private List<WorldObject> objects = new List<WorldObject>();

        public Life(List<WorldObject> objects)
        {
            this.objects = objects;
        }

        public void life()
        {
            int predatorCount = 0;
            int herbivorousCount = 0;
            int plantCount = 0;
            foreach (WorldObject w in objects)
            {
                if (w is AnimalDecotator)
                {
                    if (((AnimalDecotator)w).getAnimal())
                    {
                        //хищник
                        predatorCount++;
                    }
                    else
                    {
                        //траводяное
                        herbivorousCount++;
                    }
                }
                else
                {
                    //растение
                    plantCount++;
                }
            }
            while (true)
            {

                if (predatorCount == 0 && herbivorousCount == 0) break;
            }
        }

    }
}
