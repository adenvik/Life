using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class World
    {
        public List<WorldObject> objects;
        public Creator creator;
        public int size;
        public Logger log;

        /*
             * 1 2
             * 3 4
        */
        int[] start = { 1, 2, 3, 4 };

        public World(int size)
        {
            log = new Logger();
            creator = new Creator();
            if (size == 1) // small
            {
                this.size = 10;
            }
            else if (size == 2) // medium
            {
                this.size = 50;
            }
            else //big
            {
                this.size = 100;
            }
            objects = creator.createObjects(new Random().Next(size * 10, this.size * this.size), size);
        }

        public int[] getClearCoords()
        {
            start = start.OrderBy(x => Guid.NewGuid()).ToArray();

            if (start[0] == 1)
            {
                for(int i = 0; i < size; i++)
                {
                    for(int j = 0; j < size; j++)
                    {
                        if (isClear(i, j)) return new int[] {i, j};
                    }
                }
                throw new Exception("World.getFreeCoords() : Мир заполнен!");
            }
            if (start[0] == 2)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = size - 1; j >= 0; j--)
                    {
                        if (isClear(i, j)) return new int[] { i, j };
                    }
                }
                throw new Exception("World.getFreeCoords() : Мир заполнен!");
            }
            if (start[0] == 3)
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (isClear(i, j)) return new int[] { i, j };
                    }
                }
                throw new Exception("World.getFreeCoords() : Мир заполнен!");
            }
            if (start[0] == 4)
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    for (int j = size - 1; j >= 0; j--)
                    {
                        if (isClear(i, j)) return new int[] { i, j };
                    }
                }
                throw new Exception("World.getFreeCoords() : Мир заполнен!");
            }
            throw new Exception("World.getFreeCoords() : ошибка..");
        }

        public bool isClear(int x, int y)
        {
            if (x < 0 || y < 0 || x >= size || y >= size) throw new Exception("World.isClear() : Не верное значение координат: " + x + " " + y);
            return objects.Find(obj => obj.x == x && obj.y == y) == null ? true : false;
        }

        public WorldObject getObjectByXY(int x, int y)
        {
            return objects.Find(obj => obj.x == x && obj.y == y);
        }

        public WorldObject this[int index]
        {
            get
            {
                return this.objects[index];
            }
        }
    }
}
