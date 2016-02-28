using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class World
    {
        List<Location> objects = new List<Location>();

        public World(List<WorldObject> objects)
        {
            Random r = new Random();
            foreach (WorldObject wo in objects)
            {
                while (true)
                {
                    int x = r.Next(0, 100);
                    int y = r.Next(0, 100);
                    if (isClear(x, y))
                    {
                        this.objects.Add(new Location(wo, x, y));
                        break;
                    }
                }
            }
        }

        private bool isClear(int x, int y)
        {
            foreach (Location loc in objects)
            {
                if (loc.x == x && loc.y == y) return false;
            }
            return true;
        }
    }
}
