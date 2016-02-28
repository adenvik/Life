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

        public WorldObject(double age)
        {
            this.health = 100;
            this.age = age;
        }
    }
}
