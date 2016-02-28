using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Creator
    {
        public List<WorldObject> objects;
        public Creator()
        {
            objects = new List<WorldObject>();
        }

        public void initialize(int count)
        {
            //Создает как минимум по одному животному каждого вида и растения

            //Создаем 2 хищника 1 типа
            objects.Add(new Type1(new Predator(10.0, true)));
            objects.Add(new Type1(new Predator(10.0, false)));

            //Создаем 2 хищника 2 типа
            objects.Add(new Type2(new Predator(10.0, true)));
            objects.Add(new Type2(new Predator(10.0, false)));

            //Создаем 2 травоядных 1 типа
            objects.Add(new Type1(new Herbivorous(10.0, true)));
            objects.Add(new Type1(new Herbivorous(10.0, false)));

            //Создаем 2 травоядных 2 типа
            objects.Add(new Type2(new Herbivorous(10.0, true)));
            objects.Add(new Type2(new Herbivorous(10.0, false)));

            //Создаем 4 растения для животных
            objects.Add(new Plant(10.0));
            objects.Add(new Plant(10.0));
            objects.Add(new Plant(10.0));
            objects.Add(new Plant(10.0));
            
            int createdCount = 12;
            int treeCreated = 4;

            Random r = new Random();
            while (createdCount <= count)
            {
                /*
                 * 1 - Хищник тип 1 м
                 * 2 - Хищник тип 1 ж
                 * 3 - Хищник тип 2 м
                 * 4 - Хищник тип 2 ж
                 * 5 - Травоядное тип 1 м
                 * 6 - Травоядное тип 1 ж
                 * 7 - Травоядное тип 2 м
                 * 8 - Травоядное тип 2 ж
                 * 9 - Растение
                 */
                int n = r.Next(1, 10);
                double age = Math.Round(r.NextDouble() * 10 + 5, 1);
                if (n == 1) objects.Add(new Type1(new Predator(age, true)));
                if (n == 2) objects.Add(new Type1(new Predator(age, false)));
                if (n == 3) objects.Add(new Type2(new Predator(age, true)));
                if (n == 4) objects.Add(new Type2(new Predator(age, false)));
                if (n == 5) objects.Add(new Type1(new Herbivorous(age, true)));
                if (n == 6) objects.Add(new Type1(new Herbivorous(age, false)));
                if (n == 7) objects.Add(new Type2(new Herbivorous(age, true)));
                if (n == 8) objects.Add(new Type2(new Herbivorous(age, false)));
                if (n == 9)
                {
                    treeCreated++;
                    objects.Add(new Plant(age));
                }
                createdCount++;
                if (treeCreated < createdCount / 5)
                {
                    objects.Add(new Plant(age));
                    createdCount++;
                }
            }
        }

        public void life()
        {
            //Если два объекта стоят рядом - делать новый объект
        }
    }
}
