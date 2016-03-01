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

        public World(List<WorldObject> objects)
        {
            Random r = new Random();
            for (int i = 0; i < objects.Count; i++)
            {
                while (true)
                {
                    int x = r.Next(0, 100);
                    int y = r.Next(0, 100);
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

        private bool isClear(int x, int y)
        {
            foreach (WorldObject loc in objects)
            {
                if (loc.x == x && loc.y == y) return false;
            }
            return true;
        }
    }
}
