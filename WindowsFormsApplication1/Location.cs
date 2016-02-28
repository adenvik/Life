using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Location
    {
        public WorldObject worldObject { set; get; }
        public int x { set; get; }
        public int y { set; get; }

        public Location(WorldObject wo, int x, int y)
        {
            this.worldObject = wo;
            this.x = x;
            this.y = y;
        }
    }
}
