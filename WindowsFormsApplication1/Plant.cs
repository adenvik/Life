using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Plant : WorldObject
    {
        public Plant(double age, double maxAge)
            : base(age, maxAge)
        {

        }

        public override string ToString()
        {
            return "Plant " + this.health + " " + this.age;
        }
    }
}
