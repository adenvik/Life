using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class Plant
        : WorldObject
    {
        public Plant(int x, int y, double age, double maxAge)
            : base(x, y, age, maxAge)
        {

        }

        public override string ToString()
        {
            return "Plant " + base.ToString();
        }
    }
}
