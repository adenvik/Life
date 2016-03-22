using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Creator
    {
        public List<WorldObject> objects;
        public Creator()
        {
            objects = new List<WorldObject>();
        }

        public Creator(int count)
        {
            objects = new List<WorldObject>();
            initialize(count);
        }

        public void initialize(int count)
        {
            int createdAnimal = 0;
            int createdTree = 0;

            Random rand = new Random();
            while (createdAnimal + createdTree <= count / 2)
            {
                /*
                 * 1 - Хищник м
                 * 2 - Хищник ж
                 * 3 - Травоядное м
                 * 4 - Травоядное ж
                 * 5 - Растение
                 */
                int n = rand.Next(1, 50);
                double age = newAge(rand);//Math.Round(r.NextDouble() * 10 + 5, 1);
                double maxAge = newAge(rand);
                if (maxAge < age)
                {
                    maxAge = age + maxAge;
                }
                int type = newType(rand);
                if (n < 5) objects.Add(new Predator(age, maxAge + 27, true, type));
                if (n >= 5 && n <= 10) objects.Add(new Predator(age, maxAge + 27, false, type));
                if (n > 10 && n < 20) objects.Add(new Herbivorous(age, maxAge + 27, true, type));
                if (n >= 20 && n < 30) objects.Add(new Herbivorous(age, maxAge + 27, false, type));
                if (n >= 30)
                {
                    createdTree++;
                    objects.Add(new Plant(age, maxAge + 35));
                    createdAnimal--;
                }
                createdAnimal++;
                if (createdTree < createdAnimal / 3)
                {
                    objects.Add(new Plant(newAge(rand), maxAge + 35));
                    createdTree++;
                }
            }
        }

        static public double newAge(Random rand)
        {
            return Math.Round(rand.NextDouble() * 10 + 5, 1);
        }

        static public int newType(Random rand)
        {
            return rand.Next(1, 5);
        }
    }
}
