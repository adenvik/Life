using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class _Life
    {
        public World world;
        LifeController controller;
        public Visualizer visualizer;
        /*
         *  Переменные отвечающие за изменение координат при переходе
         *  0 1 2
         *  7   3
         *  6 5 4
        */
        int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1};
        int[] dy = { -1, -1, -1, 0, 1, 1, 1, 0};

        public _Life(World world, Form1 form)
        {
            this.world = world;
            controller = new LifeController(this);
            visualizer = new Visualizer(this.world, form);
            visualizer.drawGrid();
        }

        public void iterLife()
        {
            controller.check();
            for (int i = 0; i < world.objects.Count; i++)
            {
                world.log.add("Life.iterLife() : " + world[i].ToString());
                move(world[i]);
            }
            visualizer.draw();
        }

        public void move(WorldObject wo)
        {
            world.log.add("Life.move() : начало цикла перемещений");
            if (wo is Plant)
            {
                
                world.log.add("Life.move() : объект - растение");
                if (world.creator.getInt(world.size * world.size) == world.size)
                {
                    world.log.add("Life.move() : добавление растения");
                    try
                    {
                        int[] xy = world.getClearCoords();
                        world.objects.Add(world.creator.createPlant(xy[0], xy[1], true));
                        world.log.add("Life.move() : растение добавлено");
                    }
                    catch (Exception ex)
                    {
                        //
                        world.log.add("Life.move() : " + ex.Message);
                    }
                }
            }
            else
            {
                int count = 0;

                world.log.add("Life.move() : объект - животное");
                int startIndex = world.creator.getInt(8);
                for (int i = startIndex; i < dx.Length + startIndex; i++)
                {
                    int index = i;
                    if (i >= dx.Length) index = i - dx.Length - 1;

                    try
                    {
                        //Если клетка не пуста смотрим кто там стоит
                        if (!world.isClear(wo.x + dx[index], wo.y + dy[index]))
                        {
                            world.log.add("Life.move() : клетка не пуста, проверяем что за объкт");
                            //Получаем объект на этой клетке
                            WorldObject obj = world.getObjectByXY(wo.x + dx[index], wo.y + dy[index]);
                            
                            //Если объект на клетке расстение, а текущее животное травоядное
                            if (obj is Plant && wo is Herbivorous)
                            {
                                world.log.add("Life.move() : травоядное съело растение");
                                ((Herbivorous)wo).eat();
                                world.objects.Remove(obj);
                                wo.x = wo.x + dx[index];
                                wo.y = wo.y + dy[index];
                                break;
                            }
                            //Если объект на клетке травоядное, а текущее животное хищник
                            if (obj is Herbivorous && wo is Predator)
                            {
                                world.log.add("Life.move() : хищник съел травоядное");
                                ((Predator)wo).eat();
                                world.objects.Remove(obj);
                                wo.x = wo.x + dx[index];
                                wo.y = wo.y + dy[index];
                                break;
                            }
                            count++;
                            continue;
                        }
                        //Иначе переходим на эту клетку и наращиваем голод
                        else
                        {
                            world.log.add("Life.move() : клетка пуста - перемещаемся");
                            ((Animal)wo).growthOfHunger();
                            wo.x = wo.x + dx[index];
                            wo.y = wo.y + dy[index];
                            break;
                        }
                    }
                    //Попали на граничные координаты
                    catch (Exception ex)
                    {
                        world.log.add("Life.move() : " + ex.Message + " index:" + index);
                        if (ex.Message[0] != 'W') count++;
                        continue;
                    }
                }
                //Объект зажат, через 2 хода - умрет
                if (count >= 7)
                {
                    world.log.add("Life.move() : животное зажато");
                    wo.health -= 50;
                }
            }
        }
    }
}
