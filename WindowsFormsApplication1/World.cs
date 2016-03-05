using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class World
    {
        public List<WorldObject> objects = new List<WorldObject>();
        public int worldSize { set; get; }

        public World(List<WorldObject> objects, int worldSize)
        {
            this.worldSize = worldSize;
            Random r = new Random();
            for (int i = 0; i < objects.Count; i++)
            {
                while (true)
                {
                    int x = r.Next(0, this.worldSize);
                    int y = r.Next(0, this.worldSize);
                    if (isClear(x, y))
                    {
                        objects[i].x = x;
                        objects[i].y = y;
                        break;
                    }
                }
            }
            this.objects = objects;
        }

        public bool isClear(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new Exception("Отрицательное значение");
            }
            foreach (WorldObject woLoc in objects)
            {
                if (woLoc.x == x && woLoc.y == y) return false;
            }
            return true;
        }

        public WorldObject getObjectByXY(int x, int y)
        {
            foreach(WorldObject wo in objects)
            {
                if (wo.x == x && wo.y == y) return wo;
            }
            return null;
        }
    }
}
