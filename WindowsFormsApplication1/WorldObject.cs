using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class WorldObject
    {
        public double health { set; get; }
        public double age { set; get; }
        public double maxAge { set; get; }

        public int x { set; get; }
        public int y { set; get; }

        public WorldObject(double age, double maxAge)
        {
            this.health = 100;
            this.age = age;
            this.maxAge = maxAge;
        }
    }
}
