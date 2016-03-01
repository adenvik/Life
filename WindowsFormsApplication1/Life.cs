using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Life
    {
        public World obj;
        
        public Life(World obj)
        {
            this.obj = obj;
            //Бесконечный цикл жизни
            life();
        }

        public void life()
        {
            int predatorCount = 0;
            int herbivorousCount = 0;
            int plantCount = 0;
            foreach (WorldObject w in obj.objects)
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
            Random rand = new Random();
            while (true)
            {
                int i = rand.Next(0, obj.objects.Count);
                if (obj.objects[i] is AnimalDecotator)
                {
                    if (((AnimalDecotator)obj.objects[i]).getAnimal())
                    {
                        //хищник
                        predatorCount--;
                    }
                    else
                    {
                        //траводяное
                        herbivorousCount--;
                    }
                }
                die(obj.objects[i]);
                if (predatorCount == 0 && herbivorousCount == 0) break; // потому что while не хочет слушать данное условие с "!=" вместо "=="
            }
        }

        private void die(WorldObject wo)
        {
            obj.objects.Remove(wo);
        }

        private void reproduction()
        {

        }

        private void move(WorldObject wo)
        {
            if (wo is Plant) return;  //РАСТЕНИЯ ТУТ НЕ ХОДЯТ
        }
    }
}
