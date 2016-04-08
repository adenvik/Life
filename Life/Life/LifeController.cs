using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class LifeController
    {
        int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1 };
        int[] dy = { -1, -1, -1, 0, 1, 1, 1, 0 };
        World world;
        _Life life;

        public LifeController(_Life life/*World world*/)
        {
            this.life = life;
            this.world = life.world;
        }

        public void check()
        {
            world.objects = world.objects.OrderBy(x => Guid.NewGuid()).ToList();

            if (world.creator.getInt(world.size * world.size) == world.size)
            {
                world.log.add("LifeController.check() : добавление растения");
                try
                {
                    int[] xy = world.getClearCoords();
                    world.creator.createPlant(xy[0], xy[1], true);
                }
                catch(Exception ex)
                {
                    world.log.add("LifeController.check() : " + ex.Message);
                }
            }

            foreach (WorldObject wo in world.objects)
            {
                world.log.add("LifeController.check() : поиск похожих объектов");
                //Если не животное - идем дальше
                if (!(wo is Animal)) continue;
                //Иначе получаем список его соседей
                List<Animal> animalsAround = getObjectsAroud(wo);
                //Узнаем нет ли соседей, похожих на наш объект
                bool parent = false;
                foreach (Animal obj in animalsAround)
                {
                    if (parent) break;

                    if ( (obj is Predator && wo is Predator) ||
                         (obj is Herbivorous && wo is Herbivorous))
                    {
                        world.log.add("LifeController.check() : объект найден");
                        if ((obj.sex != ((Animal)wo).sex) 
                            && (obj.age > 5 && wo.age > 5))
                        {
                            world.log.add("LifeController.check() : параметры подходят");
                            bool exist = false;
                            //Проверяем нет ли рядом с этими объекта уже "новорожденного"
                            foreach (Animal animal in animalsAround)
                            {
                                if (animal.age == 0.1 && animal.type == ((Animal)wo).type)
                                {
                                    //Объект уже существует - переходим к следующему объекту
                                    exist = true;
                                    break;
                                }
                            }
                            if (exist)
                            {
                                world.log.add("LifeController.check() : объект уже существует");
                                parent = true;
                                continue;
                            }
                            List<Animal> _animalsAround = getObjectsAroud(obj);
                            foreach (Animal animal in _animalsAround)
                            {
                                if (animal.age == 0.1 && animal.type == ((Animal)wo).type)
                                {
                                    //Объект уже существует - переходим к следующему объекту
                                    exist = true;
                                    break;
                                }
                            }
                            if (exist)
                            {
                                world.log.add("LifeController.check() : объект уже существует");
                                parent = true;
                                continue;
                            }
                            //Иначе добавляем объект
                            //Найдем свободные координаты рядом с родителями
                            int x = -1, y = -1;
                            for (int i = 0; i < dx.Length; i++)
                            {
                                try
                                {
                                    if (world.isClear(wo.x + dx[i], wo.y + dy[i]))
                                    {
                                        x = wo.x + dx[i];
                                        y = wo.y + dy[i];
                                        break;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    world.log.add("LifeController.check() : добавление объекта " + ex.Message);
                                    continue;
                                }
                            }
                            //Если рядом с одним родителем нет свободных клеток - ищем рядом со вторым
                            if (x == -1)
                            {
                                for (int i = 0; i < dx.Length; i++)
                                {
                                    try
                                    {
                                        if (world.isClear(obj.x + dx[i], obj.y + dy[i]))
                                        {
                                            x = obj.x + dx[i];
                                            y = obj.y + dy[i];
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        world.log.add("LifeController.check() : добавление объекта " + ex.Message);
                                        continue;
                                    }
                                }
                            }
                            //Если и тут нет - не будем создавать новый объект
                            if (x == -1) continue;

                            //Иначе смотрим кто у нас и создаем рядом с родителем
                            if (wo is Herbivorous) world.creator.createHerbivorous(x, y, true);
                            else world.creator.createPredator(x, y, true);
                            world.log.add("LifeController.check() : объект добавлен");
                            //Выставляем флаг родительства
                            parent = true;
                        }
                    }
                }
            }
            world.log.add("LifeController.check() : цикл поиска окончен");
            incAge();
            isEnd();
        }

        public List<Animal> getObjectsAroud(WorldObject wo)
        {
            world.log.add("LifeController.check() : поиск животных вокруг");
            List<Animal> animalsAround = new List<Animal>();
            for (int i = 0; i < dx.Length; i++)
            {
                try
                {
                    if (!world.isClear(wo.x + dx[i], wo.y + dy[i]))
                    {
                        world.log.add("LifeController.check() : проверка объекта");
                        WorldObject obj = world.getObjectByXY(wo.x + dx[i], wo.y + dy[i]);
                        if (obj is Animal) animalsAround.Add((Animal)obj);
                    }
                }
                catch (Exception ex)
                {
                    world.log.add("LifeController.getObjectsAround() : " + ex.Message);
                    continue;
                }
            }
            world.log.add("LifeController.check() : поиск животных окончен");
            return animalsAround;
        }

        public void incAge()
        {
            world.log.add("LifeController.incAge() : начало цикла повышения возраста");
            for (int i = 0; i < world.objects.Count; i++)
            {
                world[i].age = Math.Round(world[i].age + 0.2, 1);
                if (world[i].age >= world[i].maxAge)
                {
                    world.objects.Remove(world[i]);
                    i--;
                    world.log.add("LifeController.incAge() : объект умер от старости");
                    continue;
                }
                if (world[i].health <= 0)
                {
                    world.objects.Remove(world[i]);
                    world.log.add("LifeController.incAge() : объект умер от голода");
                    i--;
                }
            }
            world.log.add("LifeController.incAge() : конец цикла повышения возраста");
        }
    
        public void isEnd()
        {
            int countPredator = 0;
            int countHerbivorous = 0;
            foreach(WorldObject wo in world.objects)
            {
                if (wo is Herbivorous)
                {
                    countHerbivorous++;
                    continue;
                }
                if (wo is Predator) countPredator++;
            }
            if (countHerbivorous == 0 && countPredator == 0)
            {
                life.visualizer.draw();
                world.log.add("LifeController.isEnd() : Все животные вымерли");
                throw new Exception("Все животные вымерли");
            }
        }
    }
}
