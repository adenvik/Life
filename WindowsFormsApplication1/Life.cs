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
                    if (index >= world.objects.Count) index = index - world.objects.Count;

                    if (world.objects[index] is AnimalDecotator)
                    {
                        if (((AnimalDecotator)world.objects[index]).getAnimal())
                        {
                            log.add(index.ToString() + ": Хищник " + world.objects[index].health.ToString());
                        }
                        else
                        {
                            log.add(index.ToString() + ": Травоядное " + world.objects[index].health.ToString());
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
                if (new Random().Next(0, 2) == 1)
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
                    if (obj is Plant && !((AnimalDecotator)wo).getAnimal())
                    {
                        log.add("Объект растение, а животное - травоядное. Растение съедено");
                        plantCount--;

                        //Сьедает растение и переходит на его позицию
                        ((AnimalDecotator)wo).eat(obj);
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

                            ((AnimalDecotator)wo).eat(obj);
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
                    ((AnimalDecotator)wo).growthOfHunger();
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
                if (world.objects[index].health <= 0)
                {
                    if (world.objects[index] is AnimalDecotator)
                    {
                        if (((AnimalDecotator)world.objects[index]).getAnimal())
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
                    if (world.objects[index].age > 35)
                    {
                        plantCount--;
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
                        predatorCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): хищник типа 1 умер");
                        continue;
                    }
                    //Если хищник типа 2: старше 30 лет - умирает
                    if (((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type2 && world.objects[index].age > 30)
                    {
                        predatorCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): хищник типа 2 умер");
                        continue;
                    }
                    //Если травоядное типа 1: старше 60 лет - умирает
                    if (!((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type1 && world.objects[index].age > 60)
                    {
                        herbivorousCount--;
                        die(world.objects[index]);
                        log.add("Функция old(): травоядное типа 1 умерло");
                        continue;
                    }
                    //Если травоядное типа 2: старше 47 лет - умирает
                    if (!((AnimalDecotator)world.objects[index]).getAnimal() && ((AnimalDecotator)world.objects[index]) is Type2 && world.objects[index].age > 47)
                    {
                        herbivorousCount--;
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
            if (first.age > 7 && second.age > 7)
            {
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
            }
            log.add("Функция reproduction(): выход");
        }

        private void animalCreate(bool type, Animal first, Animal second)
        {
            log.add("Функция animalCreate(): вход");
            if (plantCount + herbivorousCount + predatorCount >= (world.worldSize * world.worldSize) - world.worldSize) return;

            bool flag = false;

            Animal animal;
            if (first is Predator)
            {
                animal = new Predator(0.1, first.sex);
                //predatorCount++;
                flag = true;
                if (predatorCount > maxPredator) maxPredator = predatorCount;
            }
            else
            {
                animal = new Herbivorous(0.1, second.sex);
                //herbivorousCount++;
                flag = false;
                if (herbivorousCount > maxHerbivorous) maxHerbivorous = herbivorousCount;
            }
            AnimalDecotator animalDec;
            if (type) animalDec = new Type1(animal);
            else animalDec = new Type2(animal);
            //Ищем свободную ячейку рядом с родителями
            int[] resXY = new int[2];
            try
            {
                if (!first.sex) resXY = getClearPos(first);
                else resXY = getClearPos(second);

                animalDec.x = resXY[0];
                animalDec.y = resXY[1];
                world.objects.Add(animalDec);

                if (flag) predatorCount++;
                else herbivorousCount++;
            }
            catch (Exception ex)
            {
                try
                {
                    if (first.sex) resXY = getClearPos(first);
                    else resXY = getClearPos(second);

                    animalDec.x = resXY[0];
                    animalDec.y = resXY[1];
                    world.objects.Add(animalDec);

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
            if (plantCount + herbivorousCount + predatorCount >= (world.worldSize * world.worldSize) / 2) return;
            log.add("Функция plantCreate: вход");
            Plant plant = new Plant(0.1);
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
