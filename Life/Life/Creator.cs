using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life
{
    class Creator
    {
        private Random random;

        public Creator()
        {
            random = new Random();
        }

        public List<WorldObject> createObjects(int maxCount, int size)
        {
            List<WorldObject> objects = new List<WorldObject>();

            int randValue = 0;

            if (size == 1)
            {
                randValue = 10;
            }
            if (size == 2)
            {
                randValue = 50;
            }
            if (size > 2)
            {
                randValue = 100;
            }
            
            while (objects.Count < maxCount)
            {
                int x = getInt(randValue);
                int y = getInt(randValue);
                
                if (objects.Find(obj => obj.x == x && obj.y == y) == null ? true : false)
                {
                    /*
                     * 0 - 9   - хищник
                     * 10 - 29 - травоядное
                     * 30 - 49 - растение
                     */
                    int typeObj = getInt(50);
                    if (typeObj < 10) objects.Add(createPredator(x, y, false));
                    if (typeObj >= 10 && typeObj < 30) objects.Add(createHerbivorous(x, y, false));
                    if (typeObj >= 30) objects.Add(createPlant(x, y, false));
                }
            }

            return objects;
        }

        public Herbivorous createHerbivorous(int x, int y, bool newborn)
        {
            double age;
            double maxAge;
            if (newborn)
            {
                age = 0.1;
                maxAge = getDouble(getInt(60)) + getInt(getInt(3) * 13);
            }
            else
            {
                age = getDouble(getInt(60));
                maxAge = getDouble(getDouble((int)getInt((int)age)) + getInt(getInt(4) * 13));
                maxAge = maxAge > age ? maxAge : maxAge + age;
            }
            bool sex = getInt(2) == 1 ? true : false;
            int type = getInt(4);
            return new Herbivorous(x, y, Math.Round(age, 1), Math.Round(maxAge, 1), sex, type);
        }

        public Predator createPredator(int x, int y, bool newborn)
        {
            double age;
            double maxAge;
            if (newborn)
            {
                age = 0.1;
                maxAge = getDouble(getInt(50)) + getInt(getInt(3) * 13);
            }
            else
            {
                age = getDouble(getInt(50));
                maxAge = getDouble(getDouble((int)getInt((int)age)) + getInt(getInt(4) * 13));
                maxAge = maxAge > age ? maxAge : maxAge + age;
            }
            bool sex = getInt(2) == 1 ? true : false;
            int type = getInt(4);
            return new Predator(x, y, Math.Round(age, 1), Math.Round(maxAge, 1), sex, type);
        }

        public Plant createPlant(int x, int y, bool newborn)
        {
            double age, maxAge;
            if (newborn)
            {
                age = 0.1;
                maxAge = getDouble(getInt(100)) + getInt(getInt(5) * 13);
            }
            else
            {
                age = getDouble(getInt(100));
                maxAge = getDouble(getDouble((int)getInt((int)age)) + getInt(getInt(4) * 13));
                maxAge = maxAge > age ? maxAge : maxAge + age;
            }
            return new Plant(x, y, Math.Round(age, 1), Math.Round(maxAge, 1));
        }

        public  double getDouble(double value)
        {
            return random.NextDouble() + value;
        }

        public int getInt(int maxValue)
        {
            return random.Next(0, maxValue);
        }
    }
}
