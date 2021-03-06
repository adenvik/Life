﻿using System;
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

        public WorldObject this[int index]
        {
            get
            {
                return this.objects[index];
            }
        }

        public bool isClear(int x, int y)
        {
            if (x < 0 || y < 0 || x > worldSize - 1 || y > worldSize - 1)
            {
                throw new Exception("Не правильное значение");
            }
            foreach (WorldObject woLoc in objects)
            {
                if (woLoc.x == x && woLoc.y == y) return false;
            }
            return true;
        }

        /*
         * return the first clear coordinates
         */
        public int[] getClearCoords()
        {
            int[] coords = new int[2];
            coords[0] = -1;

            for (int i = 0; i < worldSize; i++)
            {
                for (int j = 0; j < worldSize; j++)
                {
                    if (isClear(i, j))
                    {
                        coords[0] = i;
                        coords[1] = j;
                        break;
                    }
                }
            }
            if (coords[0] == -1) throw new Exception("World is full!");
            return coords;
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
