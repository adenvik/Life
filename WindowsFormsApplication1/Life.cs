using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Life
    {
        public World world;
        //Текущее значение существ разных видос
        int predatorCount = 0;
        int herbivorousCount = 0;
        int plantCount = 0;
        //Максимальное значение существ различных видов
        int maxPredator = 0;
        int maxHerbivorous = 0;
        int maxPlant = 0;
        //Логгер, собирающий информацию
        Logger log = new Logger();

        /*
         *  Переменные отвечающие за изменение координат при переходе
         *  0 1 2
         *  7   3
         *  6 5 4
        */
        int[] dx = new int[8];
        int[] dy = new int[8];
        
        public Life(World obj)
        {
            this.world = obj;
            dx[0] = -1;
            dy[0] = -1;
            dx[1] = 0;
            dy[1] = -1;
            dx[2] = 1;
            dy[2] = -1;
            dx[3] = 1;
            dy[3] = 0;
            dx[4] = 1;
            dy[4] = 1;
            dx[5] = 0;
            dy[5] = 1;
            dx[6] = -1;
            dy[6] = 1;
            dx[7] = -1;
            dy[7] = 0;
            //Бесконечный цикл жизни
            //life();
        }

        public bool iterLife(Graphics g)
        {
            Brush b = new SolidBrush(Color.White);
            g.Clear(Color.Silver);
            for (int ind = 0; ind < 600; ind += 60)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, ind, 600, ind);
            }
            for (int ind = 0; ind < 600; ind += 60)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), ind, 0, ind, 600);
            }
            herbivorousCount = 0;
            predatorCount = 0;
            plantCount = 0;

            for (int i = 0; i < world.objects.Count; i++)
            {
                if (world[i] is Herbivorous)
                {
                    herbivorousCount++;
                }
                if (world[i] is Predator)
                {
                    predatorCount++;
                }
                if (world[i] is Plant)
                {
                    plantCount++;
                }
            }

            int startIndex = new Random().Next(0, world.objects.Count);
            for (int i = startIndex; i < startIndex + world.objects.Count; i++)
            {
                int index = i;

                if (index >= world.objects.Count) index -= world.objects.Count;
                if (index >= world.objects.Count) index -= world.objects.Count;

                if (world[index] is Herbivorous)
                {
                    //b = new SolidBrush(Color.Green);
                    g.DrawImage(WindowsFormsApplication1.Properties.Resources.herb, 60 * world[index].x, 60 * world[index].y, 60, 60);
                }
                if (world[index] is Predator)
                {
                    //b = new SolidBrush(Color.Red);
                    g.DrawImage(WindowsFormsApplication1.Properties.Resources.pred, 60 * world[index].x, 60 * world[index].y, 60, 60);
                }
                if (world[index] is Plant) 
                {
                    //b = new SolidBrush(Color.Yellow);
                    g.DrawImage(WindowsFormsApplication1.Properties.Resources.tree, 60 * world[index].x, 60 * world[index].y, 60, 60);
                }
                log.add(index.ToString() + " : " + world[index].ToString());

                move(world[index]);
                old();

                //g.FillEllipse(b, new Rectangle(60 * world[index].x, 60 * world[index].y, 60, 60));
            }

            if (herbivorousCount == 0 && predatorCount == 0) return false;
            return true;
        }

        public void life()
        {
            log.add("Старт жизни..");
            foreach (WorldObject w in world.objects)
            {
                if (w is Animal)
                {
                    if (w is Predator)
                    {
                        //хищник
                        predatorCount++;
                    }
                    else
                    {
                        //траводяное
                        herbivorousCount++;
                    }
                }
                else
                {
                    //растение
                    plantCount++;
                }
            }
            log.add("Сгенерированные объекты: ");
            log.add("Травоядных: " + herbivorousCount.ToString());
            log.add("Хищников: " + predatorCount.ToString());
            log.add("Растений: " + plantCount.ToString());

            maxHerbivorous = herbivorousCount;
            maxPlant = plantCount;
            maxPredator = predatorCount;

            double iter = 0;
            DateTime start = DateTime.Now;
            while (true)
            {
                int startIndex = new Random().Next(0, world.objects.Count);

                //-----------------------------
                for (int i = startIndex; i < startIndex + world.objects.Count; i++)
                {
                    int index = i;
                    if (index >= world.objects.Count) index -= world.objects.Count;
                    if (index >= world.objects.Count) index -= world.objects.Count;

                    if (world.objects[index] is Animal)
                    {
                        if (world.objects[index] is Predator)
                        {
                            log.add(index.ToString() + ": Хищник " + ((Animal)world.objects[index]).ToString());
                        }
                        else
                        {
                            log.add(index.ToString() + ": Травоядное " + ((Animal)world.objects[index]).ToString());
                        }
                    }
                    else
                    {
                        log.add(index.ToString() + ": Растение " + world.objects[index].health.ToString());
                    }
                    move(world.objects[index]);
                    old();
                    iter++;

                    if (iter == 100)
                    {
                        incYear();
                        iter = 0;
                        log.add("Спустя год: ");
                        log.add("Травоядных: " + herbivorousCount.ToString());
                        log.add("Хищников: " + predatorCount.ToString());
                        log.add("Растений: " + plantCount.ToString());
                    }
                    if (herbivorousCount == 0 && predatorCount == 0) break;
                }
                if (herbivorousCount == 0 && predatorCount == 0) break;
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            log.add("Всего: " + world.objects.Count);
            log.add("Травоядных: " + herbivorousCount.ToString());
            log.add("Хищников: " + predatorCount.ToString());
            log.add("Растений: " + plantCount.ToString());
            MessageBox.Show("Макс травоядных за время существования: " + maxHerbivorous.ToString()
                            + "\nМакс хищников за время существования: " + maxPredator.ToString()
                            + "\nМакс растений за время сущствования: " + maxPlant.ToString()
                            + "\nЖизнь существовала: " + ts.Hours.ToString() + " часов " + ts.Minutes.ToString() + " минут " + ts.Seconds.ToString() + " секунд");
        }

        private void move(WorldObject wo)
        {
            log.add("Вход в функцию move");
            if (wo is Plant)
            {
                if (new Random().Next(0, 3) == 1)
                {
                    log.add("Добавление растения");
                    plantCreate();
                    log.add("Окончание добавления растения");
                }
                return;  //РАСТЕНИЯ ТУТ НЕ ХОДЯТ
            }

            //Проверяем клетку рядом с объектом
            log.add("Вход в цикл перемещения животного");

            //Поочередно проходим все точки, исключая "случайное" бесконечное хождение
            
            //Выбираем случайное значение начального поворота
            int startIndex = new Random().Next(0,8);
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                int index = i;
                if (i >= 7) index = i - 7;

                int res = canMove(wo.x + dx[index], wo.y + dy[index]);
                
                //Нельзя перейти в выбранном направлении
                if (res == 3)
                {
                    log.add("Животное не может передвинуться, т.к. заданы отрицательные координаты");
                    continue;
                }
                //Клетка не пуста
                if (res == 2)
                {
                    log.add("Клетка не пуста, проверяется объект на клетке");
                    WorldObject obj = world.getObjectByXY(wo.x + dx[index], wo.y + dy[index]);
                    if (obj == null)
                    {
                        log.add("Объект не получен: null");
                        continue;
                    }
                    //Если объект - растение и текущее животное - травоядное
                    if (obj is Plant && wo is Herbivorous)
                    {
                        log.add("Объект растение, а животное - травоядное. Растение съедено");
                        plantCount--;

                        //Сьедает растение и переходит на его позицию
                        ((Herbivorous)wo).eat(obj);
                        die(obj);
                        wo.x += dx[index];
                        wo.y += dy[index];
                        return;
                    }
                    //Если объект растение и текущее животное хищник - пробуем еще
                    if (obj is Plant)
                    {
                        log.add("Объект растение, а животное - хищник. Continue");
                        continue;
                    }
                    //Иначе объект - животное
                    else
                    {
                        //Если объект хищник и текущее животное травоядное 
                        if (obj is Predator && wo is Herbivorous)
                        {
                            log.add("Объект хищник и текущее животное травоядное . Continue");
                            continue;
                        }

                        //---------------------------------------------------------------Размножение------------------------------------------------------------------
                        //Если объект хищник и текущее животное хищник 
                        if (obj is Predator && wo is Predator)
                        {
                            log.add("Объект хищник и текущее животное хищник . Попытка размножить");
                            reproduction((Animal)obj, (Animal)wo);
                            return;
                        }
                        //Если объект травоядное и текущее животное травоядное
                        if (obj is Herbivorous && wo is Herbivorous)
                        {
                            log.add("Объект травоядное и текущее животное травоядное . Попытка размножить");
                            reproduction((Animal)obj, (Animal)wo);
                            return;
                        }
                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Если объект травоядное и текущее животное хищник
                        if (obj is Herbivorous && wo is Predator)
                        {
                            log.add("Объект травоядное и текущее хищник травоядное . Ест травоядное");
                            herbivorousCount--;

                            ((Animal)wo).eat(obj);
                            die(obj);
                            wo.x += dx[index];
                            wo.y += dy[index];
                            return;
                        }
                    }
                }
                //Клетка пуста
                if (res == 1)
                {
                    log.add("Клетка пуста, переходим на нее, увеличивая голод");
                    //Поднимаем голод у животного и перемещаем его
                    ((Animal)wo).growthOfHunger();
                    wo.x += dx[index];
                    wo.y += dy[index];
                    return;
                }
            }
        }

        private int canMove(int x, int y)
        {
            log.add("Функция canMove(): вход");
            try
            {
                if (world.isClear(x, y)) return 1;
                else return 2;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        private void die(WorldObject wo)
        {
            log.add("Функция die(): вход");
            world.objects.Remove(wo);
            log.add("Функция die(): выход");
        }

        public void incYear()
        {
            log.add("Функция incYear(): вход");
            foreach (WorldObject wo in world.objects)
            {
                wo.age++;
            }
            log.add("Функция incYear(): выход");
        }

        private void old()
        {
            log.add("Функция old(): вход");
            for (int index = 0; index < world.objects.Count; index++)
            {
                if (world.objects[index].health <= 0)
                {
                    if (world.objects[index] is Animal)
                    {
                        if (world.objects[index] is Herbivorous)
                        {
                            //хищник
                            predatorCount--;
                        }
                        else
                        {
                            //траводяное
                            herbivorousCount--;
                        }
                    }
                    else
                    {
                        //растение
                        plantCount--;
                    }
                    die(world.objects[index]);
                    log.add("Функция old(): объект с 0 хп уничтожен");
                    continue;
                }
                //Если растение:
                if (world.objects[index] is Plant)
                {
                    //старше 35 лет - умирает
                    if (world.objects[index].age >= world.objects[index].maxAge)
                    {
                        plantCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): растение умерло");
                        continue;
                    }
                }
                else
                {
                    //Если хищник: старше 40 лет - умирает
                    if (world.objects[index] is Predator && world.objects[index].age >= world.objects[index].maxAge)
                    {
                        predatorCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): хищник умер");
                        continue;
                    }
                    //Если травоядное: старше 60 лет - умирает
                    if (world.objects[index] is Herbivorous && world.objects[index].age >= world.objects[index].maxAge)
                    {
                        herbivorousCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): травоядное умерло");
                        continue;
                    }
                }
            }
            log.add("Функция old(): выход");
        }

        private void reproduction(Animal first, Animal second)
        {
            log.add("Функция reproduction(): вход");
            if (first.age > 7 && second.age > 7)
            {
                if (first.type == second.type)
                {
                    if (first.sex != second.sex)
                    {
                        animalCreate(first, second);
                    }
                }
            }
            log.add("Функция reproduction(): выход");
        }

        private void animalCreate(Animal first, Animal second)
        {
            log.add("Функция animalCreate(): вход");
            if (plantCount + herbivorousCount + predatorCount >= (world.worldSize * world.worldSize) - world.worldSize) return;

            bool flag = false;

            Animal animal;
            if (first is Predator)
            {
                animal = new Predator(0.1, Creator.newAge(new Random()) + 27, first.sex, first.type);
                //predatorCount++;
                flag = true;
                if (predatorCount > maxPredator) maxPredator = predatorCount;
            }
            else
            {
                animal = new Herbivorous(0.1, Creator.newAge(new Random()) + 27, second.sex, first.type);
                //herbivorousCount++;
                flag = false;
                if (herbivorousCount > maxHerbivorous) maxHerbivorous = herbivorousCount;
            }
            //Ищем свободную ячейку рядом с родителями
            int[] resXY = new int[2];
            try
            {
                if (!first.sex) resXY = getClearPos(first);
                else resXY = getClearPos(second);

                animal.x = resXY[0];
                animal.y = resXY[1];
                world.objects.Add(animal);

                if (flag) predatorCount++;
                else herbivorousCount++;
            }
            catch (Exception ex)
            {
                try
                {
                    if (first.sex) resXY = getClearPos(first);
                    else resXY = getClearPos(second);

                    animal.x = resXY[0];
                    animal.y = resXY[1];
                    world.objects.Add(animal);

                    if (flag) predatorCount++;
                    else herbivorousCount++;
                }
                catch (Exception e)
                {
                    log.add("Функция animalCreate(): " + e.Message);
                }
            }
            log.add("Функция animalCreate(): выход");
        }

        private int[] getClearPos(Animal animal)
        {
            log.add("Функция getClearPos(): вход");
            int[] mas = new int[2];
            mas[0] = -1;

            for (int i = 0; i < mas.Length; i++)
            {
                if (canMove(animal.x + dx[i],animal.y + dy[i]) == 1)
                {
                    mas[0] = dx[i];
                    mas[1] = dy[i];
                    break;
                }
            }
            if (mas[0] == -1) throw new Exception("isFull");
            log.add("Функция getClearPos(): выход");
            return mas;
        }

        private void plantCreate()
        {
            if (plantCount >= (world.worldSize * world.worldSize)/ 2) return;
            log.add("Функция plantCreate: вход");
            Plant plant = new Plant(0.1, Creator.newAge(new Random()) + 30);
            try
            {
                int[] xy = world.getClearCoords();
                plant.x = xy[0];
                plant.y = xy[1];
                world.objects.Add(plant);
                plantCount++;
                if (plantCount > maxPlant) maxPlant = plantCount;
                log.add("Функция plantCreate: растение добавлено");
                log.add("Функция plantCreate: выход");
            }
            catch (Exception ex)
            {
                log.add("Функция plantCreate: " + ex.Message);
            }
        }
    }
}
