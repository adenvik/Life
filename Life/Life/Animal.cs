using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    abstract class Animal
        :WorldObject
    {
        public bool sex { set; get; }
        public double hunger { set; get; }
        public int type { set; get; }

        public Animal(int x,int y,double age, double maxAge, bool sex, int type)
            : base(x,y,age,maxAge)
        {
            this.hunger = 0.1;
            this.sex = sex;
            this.type = type;
        }

        public override string ToString()
        {
            return "sex: " + this.sex + " hunger:" + this.hunger + " type: " + this.type + " " + base.ToString();
        }
        abstract public void eat();
        abstract public void growthOfHunger();
    }
}
