using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Life
    {
        public World world;
        int predatorCount = 0;
        int herbivorousCount = 0;
        int plantCount = 0;

        int maxPredator = 0;
        int maxHerbivorous = 0;
        int maxPlant = 0;
        Logger log = new Logger();
        
        public Life(World obj)
        {
            this.world = obj;
            //Бесконечный цикл жизни
            life();
        }

        public void life()
        {
            log.add("Старт жизни..");
            foreach (WorldObject w in world.objects)
            {
                if (w is AnimalDecotator)
                {
                    if (((AnimalDecotator)w).getAnimal())
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
            log.add("Травоядных: " + herbivorousCount.ToString()
                    + "\nХищников: " + predatorCount.ToString()
                    + "\nРастений: " + plantCount.ToString());
            maxHerbivorous = herbivorousCount;
            maxPlant = plantCount;
            maxPredator = predatorCount;

            Random rand = new Random();
            double iter = 0;
            DateTime start = DateTime.Now;
            while (world.objects.Count > 0)
            {
                int i = rand.Next(0, world.objects.Count);

                if (world.objects[i] is AnimalDecotator)
                {
                    if (((AnimalDecotator)world.objects[i]).getAnimal())
                    {
                        log.add(i.ToString() + ": Хищник");
                    }
                    else
                    {
                        log.add(i.ToString() + ": Травоядное");
                    }
                }
                else
                {
                    log.add(i.ToString() + ": Растение");
                }

                move(world.objects[i]);
                old();
                iter++;

                if (iter == 100)
                {
                    incYear();
                    iter = 0;
                    log.add("\tТравоядных: " + herbivorousCount.ToString()
                            + "\n\tХищников: " + predatorCount.ToString()
                            + "\n\tРастений: " + plantCount.ToString());
                }
                if (herbivorousCount == 0 && predatorCount == 0 && plantCount == 0) break;
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
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
                log.add("Добавление дерева");
                treeCreate();
                log.add("Окончание добавления дерева");
                return;  //РАСТЕНИЯ ТУТ НЕ ХОДЯТ
            }

            Random rand = new Random();
            //Проверяем клетку рядом с объектом
            log.add("Вход в цикл перемещения животного");
            while (true)
            {
                int index = rand.Next(0, 8);
                /*    0 1 2
                 *    7   3
                 *    6 5 4
                */
                int dX = 0, dY = 0;
                if (index == 0)
                {
                    dX = -1;
                    dY = -1;
                }
                if (index == 1)
                {
                    dY = -1;
                }
                if (index == 2)
                {
                    dX = 1;
                    dY = -1;
                }
                if (index == 3)
                {
                    dX = 1;
                }
                if (index == 4)
                {
                    dX = 1;
                    dY = 1;
                }
                if (index == 5)
                {
                    dY = 1;
                }
                if (index == 6)
                {
                    dX = -1;
                    dY = 1;
                }
                if (index == 7)
                {
                    dX = -1;
                }
                int res = canMove(wo.x + dX, wo.y + dY);
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
                    WorldObject obj = world.getObjectByXY(wo.x + dX, wo.y + dY);
                    if (obj == null)
                    {
                        log.add("Объект не получен: null");
                        continue;
                    }
                    //Если объект - растение и текущее животное - травоядное
                    if (obj is Plant && !((AnimalDecotator)wo).getAnimal())
                    {
                        log.add("Объект растение, а животное - травоядное. Растение съедено");
                        plantCount--;

                        //Сьедает растение и переходит на его позицию
                        ((AnimalDecotator)wo).eat(ref obj);
                        die(obj);
                        wo.x += dX;
                        wo.y += dY;
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
                        if (((AnimalDecotator)obj).getAnimal() && !((AnimalDecotator)wo).getAnimal())
                        {
                            log.add("Объект хищник и текущее животное травоядное . Continue");
                            continue;
                        }

                        //---------------------------------------------------------------Размножение------------------------------------------------------------------
                        //Если объект хищник и текущее животное хищник 
                        if (((AnimalDecotator)obj).getAnimal() && ((AnimalDecotator)wo).getAnimal())
                        {
                            log.add("Объект хищник и текущее животное хищник . Попытка размножить");
                            reproduction((AnimalDecotator)obj, (AnimalDecotator)wo);
                            return;
                        }
                        //Если объект травоядное и текущее животное травоядное
                        if (!((AnimalDecotator)obj).getAnimal() && !((AnimalDecotator)wo).getAnimal())
                        {
                            log.add("Объект травоядное и текущее животное травоядное . Попытка размножить");
                            reproduction((AnimalDecotator)obj, (AnimalDecotator)wo);
                            return;
                        }
                        //--------------------------------------------------------------------------------------------------------------------------------------------

                        //Если объект травоядное и текущее животное хищник
                        if (!((AnimalDecotator)obj).getAnimal() && ((AnimalDecotator)wo).getAnimal())
                        {
                            log.add("Объект травоядное и текущее хищник травоядное . Ест травоядное");
                            herbivorousCount--;

                            ((AnimalDecotator)wo).eat(ref obj);
                            die(obj);
                            wo.x += dX;
                            wo.y += dY;
                            return;
                        }
                    }
                }
                //Клетка пуста
                if (res == 1)
                {
                    log.add("Клетка пуста, переходим на нее, увеличивая голод");
                    //Поднимаем голод у животного и перемещаем его
                    ((AnimalDecotator)wo).growthOfHunger();
                    wo.x += dX;
                    wo.y += dY;
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

        private void incYear()
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
                if (world.objects[index].health == 0)
                {
                    die(world.objects[index]);
                    log.add("Функция old(): объекта с 0 хп уничтожен");
                    continue;
                }
                //Если растение:
                if (world.objects[index] is Plant)
                {
                    //старше 100 лет - умирает
                    if (world.objects[index].age > 100)
                    {
                        die(world.objects[index]);
                        log.add("Функция old(): растение умерло");
                        continue;
                    }
                }
                else
                {
                    //Если хищник типа 1: старше 40 лет - умирает
                    if (((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type1 && world.objects[index].age > 40)
                    {
                        die(world.objects[index]);
                        log.add("Функция old(): хищник типа 1 умерло");
                        continue;
                    }
                    //Если хищник типа 2: старше 30 лет - умирает
                    if (((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type2 && world.objects[index].age > 30)
                    {
                        die(world.objects[index]);
                        log.add("Функция old(): хищник типа 2 умерло");
                        continue;
                    }
                    //Если травоядное типа 1: старше 60 лет - умирает
                    if (!((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type1 && world.objects[index].age > 60)
                    {
                        die(world.objects[index]);
                        log.add("Функция old(): травоядное типа 1 умерло");
                        continue;
                    }
                    //Если травоядное типа 2: старше 47 лет - умирает
                    if (!((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type2 && world.objects[index].age > 47)
                    {
                        die(world.objects[index]);
                        log.add("Функция old(): травоядное типа 2 умерло");
                        continue;
                    }
                }
            }
            log.add("Функция old(): выход");
        }

        private void reproduction(AnimalDecotator first, AnimalDecotator second)
        {
            log.add("Функция reproduction(): вход");
            if (first is Type1 && second is Type1)
            {
                if (first.sex != second.sex)
                {
                    animalCreate(true, first, second);
                }
            }
            if (first is Type2 && second is Type2)
            {
                if (first.sex != second.sex)
                {
                    animalCreate(true, first, second);
                }
            }
            log.add("Функция reproduction(): выход");
        }

        private void animalCreate(bool type, Animal first, Animal second)
        {
            log.add("Функция animalCreate(): вход");
            if (plantCount + herbivorousCount + predatorCount == (world.worldSize * world.worldSize) / 2) return;

            Animal animal;
            if (first is Predator)
            {
                animal = new Predator(0.1, first.sex);
                predatorCount++;
                if (predatorCount > maxPredator) maxPredator = predatorCount;
            }
            else
            {
                animal = new Herbivorous(0.1, second.sex);
                herbivorousCount++;
                if (herbivorousCount > maxHerbivorous) maxHerbivorous = herbivorousCount;
            }
            AnimalDecotator animalDec;
            if (type) animalDec = new Type1(animal);
            else animalDec = new Type2(animal);
            //Ищем свободную ячейку рядом с родителями
            if (!first.sex)
            {
                int[] resXY = getClearPos(first);
                if (resXY[0] == -1) resXY = getClearPos(second);
                animalDec.x = resXY[0];
                animalDec.y = resXY[1];
                world.objects.Add(animalDec);
            }
            log.add("Функция animalCreate(): выход");
        }

        private int[] getClearPos(Animal animal)
        {
            log.add("Функция getClearPos(): вход");
            int[] mas = new int[2];
            mas[0] = -1;
            mas[1] = -1;

            /*
             * 0 1 2
             * 7   3
             * 6 5 4
             */

            int[] dx = new int[8];
            int[] dy = new int[8];
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


            for (int i = 0; i < mas.Length; i++)
            {
                if (canMove(animal.x + dx[i],animal.y + dy[i]) == 1)
                {
                    mas[0] = dx[i];
                    mas[1] = dy[i];
                    break;
                }
            }
            log.add("Функция getClearPos(): выход");
            return mas;
        }

        private void treeCreate()
        {
            if (plantCount + herbivorousCount + predatorCount == (world.worldSize * world.worldSize) / 2) return;
            log.add("Функция treeCreate: вход");
            Plant plant = new Plant(0.1);
            Random random = new Random();
            log.add("Функция treeCreate: вход в цикл");
            while (true)
            {
                int x = random.Next(0, world.worldSize);
                int y = random.Next(0, world.worldSize);
                log.add("Функция treeCreate: x: " + x.ToString() + " y: " + y.ToString());
                if (world.isClear(x, y))
                {
                    log.add("Координаты свободны, добавлено");
                    plant.x = x;
                    plant.y = y;
                    world.objects.Add(plant);

                    if (plantCount > maxPlant) maxPlant = plantCount;

                    break;
                }
                log.add("Функция treeCreate: координаты заняты");
            }
            log.add("Функция treeCreate: выход");
        }
    }
}
