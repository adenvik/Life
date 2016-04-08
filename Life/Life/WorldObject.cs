using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class WorldObject
    {
        public double health { set; get; }
        public double age { set; get; }
        public int x { set; get; }
        public int y { set; get; }
        public double maxAge { set; get; }

        public WorldObject(int x, int y, double age, double maxAge)
        {
            this.health = 100;
            this.x = x;
            this.y = y;
            this.age = age;
            this.maxAge = maxAge;
        }

        public override string ToString()
        {
            return "x: " + this.x + " y: " + this.y + " age: " + this.age +
                   " maxAge: " + this.maxAge + " health: " + this.health;
        }

    }
}
