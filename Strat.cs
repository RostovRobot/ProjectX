using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    /// <summary>
    /// Класс, описывающий стратегию игрока
    /// </summary>
    class Strat
    {
        private double K_ally = 0.000001D;
        private double K_enemy = 0.0000013D;
        private double K_allyHP = 7.0D;
        private double K_enemyHP = 10.0D;
        private double K_distance = 20.0D;
        private int lineType = -1; //линия игры нашего мага

        /*public Strat()
        {

        }
        public Strat(Wizard self)
        {
            switch ((int)self.Id)//если наш айди
            {
                case 1:
                case 2:
                case 6:
                case 7:
                    lineType = 1; //то линия - топ
                    break;
                case 3:
                case 8:
                    lineType = 2; //то линия - мид
                    break;
                case 4:
                case 5:
                case 9:
                case 10:
                    lineType = 3; //то линия - боттом
                    break;
            }
        }*/

        /// <summary>
        /// Возвращает координаты самой важной зоны
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Точка на двухмерной карте</returns>
        public Point2D getHotZone(World world, Game game, Wizard self)
        {
            Point2D HotZone = new Point2D();

            double lowHPFactor = 0.3D;

            LivingUnit nearestTarget = getNearestTarget(world, self);
            LivingUnit nearestBuilding = getNearestBuilding(world, self);

            Point2D myBase = getMyBase(self);
            Point2D enemyBase = getEnemyBase(self);

            if (self.Life > self.MaxLife * lowHPFactor) //если у нас достаточно ХП
            {
                if (lineType < 0) //если линия еще не выбрана, то выбираем ее по айди
                {
                    switch ((int)self.Id)//если наш айди
                    {
                        case 1:
                        case 2:
                        case 6:
                        case 7:
                            lineType = 2;// 1; //то линия - топ
                            break;
                        case 3:
                        case 8:
                            lineType = 2; //то линия - мид
                            break;
                        case 4:
                        case 5:
                        case 9:
                        case 10:
                            lineType = 2;// 3; //то линия - боттом
                            break;
                    }
                }
                switch (lineType)//выбираем координаты по нашей линии
                {
                    case 3:
                        HotZone = new Point2D(3700, 3700);//то боттом
                        break;
                    case -1:
                    case 2:
                        HotZone = new Point2D(2000, 2000);//то мид
                        break;
                    case 1:
                        HotZone = new Point2D(300, 300);//то топ
                        break;
                }
                double allyBaseDist = self.GetDistanceTo(myBase.getX(), myBase.getY()); //измеряем расстояние до своей базы
                double enemyBaseDist = self.GetDistanceTo(enemyBase.getX(), enemyBase.getY()); //измеряем расстояние до базы врага
                if(enemyBaseDist<allyBaseDist) //если до базы врага ближе
                {
                    HotZone = enemyBase;//то хотзон - база врага
                }
                if (self.X < 500 /*&& self.X > 400*/)//если наш икс в диапазоне
                {
                    if (self.Y < 500 /*&& self.Y > 400*/)//и игрек тоже
                    {
                        HotZone = enemyBase;//то хотзон - база врага
                        lineType = 1; //и наша линия - топ
                    }
                }
                if (/*self.X < 3600.0D &&*/ self.X > 3500.0D)//если наш икс в диапазоне
                {
                    if (/*self.Y < 3600.0D &&*/ self.Y > 3500.0D)//и игрек тоже
                    {
                        HotZone = enemyBase;//то хотзон - база врага
                        lineType = 3; //и наша линия - боттом
                    }
                }
                if (self.X < 2350.0D && self.X > 1650.0D)//если наш икс в диапазоне
                {
                    if (self.Y < 2350.0D && self.Y > 1650.0D)//и игрек тоже
                    {
                        HotZone = enemyBase;//то хотзон - база врага
                        lineType = 2; //и наша линия - мид
                    }
                }
            } else
            {
                HotZone = myBase;
            }
            return HotZone;
        }

        /// <summary>
        /// !!! НЕ РЕАЛИЗОВАНО !!! Возвращает координаты самой важной зоны (реализация Овсянникова)
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Точка на двухмерной карте</returns>
        public Point2D getHotZone2(World world, Game game, Wizard self, VisualClient vc)
        {
            //определяеам список возможных зон
            List<HotZone> hotZones = getZones(world, self);
            //если список возможных зон не пуст
            if (hotZones.Count > 0)
            {
                double maxHot = 0.0D;
                Point2D returnPoint = new Point2D(hotZones.ElementAt(0).getX(), hotZones.ElementAt(0).getY());

                //расставляем "температуру" каждой зоны
                foreach (HotZone hZ in hotZones)
                {
                    double distanceFactor = 0.0D;
                    double allyFactor = 0.0D;
                    double enemyFactor = 0.0D;
                    double allyHPFactor = 0.0D;
                    double enemyHPFactor = 0.0D;

                    //!!! НЕ РЕАЛИЗОВАНО ОПРЕДЕЛЕНИЕ ВЕЛИЧИН РАЗЛИЧНЫХ ФАКТОРОВ !!!
                    foreach (Wizard wz in world.Wizards) //перебираем всех магов
                    {
                        double dist = wz.GetDistanceTo(hZ.getX(), hZ.getY()); //определение дистанции между текущей зоной и магом
                        if (dist > 20 && dist < 600)
                        {
                            if (wz.Faction != self.Faction)
                            {
                                enemyFactor += 216000000.0 * K_enemy - dist * dist * dist * K_enemy;
                                enemyHPFactor += K_enemyHP * (wz.MaxLife / wz.Life - 1);
                            } else
                            {
                                allyFactor -= 216000000.0 * K_ally - dist * dist * dist * K_ally;
                                allyHPFactor -= K_allyHP * (wz.MaxLife / (wz.MaxLife - wz.Life + 1) - 1);
                            }
                        }
                    }
                    foreach (Building build in world.Buildings) //перебираем все здания
                    {
                        double dist = build.GetDistanceTo(hZ.getX(), hZ.getY()); //определение дистанции между текущей зоной и зданием
                        if (dist > 20 && dist < 600)
                        {
                            if (build.Faction != self.Faction)
                            {
                                enemyFactor += 2.0 * (216000000.0 * K_enemy - dist * dist * dist * K_enemy);
                                enemyHPFactor += 2.3 * K_enemyHP * (build.MaxLife / build.Life - 1);
                            } else
                            {
                                allyFactor -= 2.0 * (216000000.0 * K_ally - dist * dist * dist * K_ally);
                                allyHPFactor -= 2.3 * K_allyHP * (build.MaxLife / (build.MaxLife - build.Life + 1) - 1);
                            }
                        }
                    }
                    foreach (Minion minion in world.Minions) //перебираем всех миньонов
                    {
                        double dist = minion.GetDistanceTo(hZ.getX(), hZ.getY()); //определение дистанции между текущей зоной и миньоном
                        if (dist > 20 && dist < 600)
                        {
                            if (minion.Faction != self.Faction)
                            {
                                enemyFactor += 0.05 * (216000000.0 * K_enemy - dist * dist * dist * K_enemy);
                                enemyHPFactor += 0.005 * K_enemyHP * (minion.MaxLife / minion.Life - 1);
                            } else
                            {
                                allyFactor -= 0.05 * (216000000.0 * K_ally - dist * dist * dist * K_ally);
                                allyHPFactor -= 0.005 * K_allyHP * (minion.MaxLife / (minion.MaxLife - minion.Life + 1) - 1);
                            }
                        }
                    }
                    double dist2 = self.GetDistanceTo(hZ.getX(), hZ.getY());
                    if (dist2 > 100)
                    {
                        distanceFactor = K_distance * (3000 / dist2);
                    }

                    hZ.hot = distanceFactor + allyFactor + enemyFactor + allyHPFactor + enemyHPFactor;
                    if (vc != null)
                    {
                        vc.Text(hZ.getX(), hZ.getY(), Convert.ToString(hZ.hot), 1.0f, 0.0f, 0.0f);
                    }
                }

                //выбираем зону с максимальной "температурой"
                foreach (HotZone hZ in hotZones)
                {
                    if (hZ.hot > maxHot)
                    {
                        maxHot = hZ.hot;
                        returnPoint = new Point2D(hZ.getX(), hZ.getY());
                    }
                }

                if (vc != null)
                {
                    vc.Line(returnPoint.getX(), returnPoint.getY(), self.X, self.Y, 0.0f, 0.0f, 0.0f);
                }
                return returnPoint;
            } else
            {
                //иначе, вызываем метод Литвинова
                return getHotZone(world, game, self);
            }
        }

        /// <summary>
        /// Возвращает список возможных горячих зон
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <returns>Коллекция точек возможных горячих зон</returns>
        public List<HotZone> getZones(World world, Wizard self)
        {
            List<HotZone> returnList = new List<HotZone>();
            foreach (var wizard in world.Wizards)
            {
                HotZone myZone = new HotZone(wizard.X, wizard.Y);
                if (wizard.Faction == self.Faction)
                { myZone.ally = true; }
                returnList.Add(myZone);
            }


            foreach (var build in world.Buildings)
            {
                HotZone myZone = new HotZone(build.X, build.Y);
                if (build.Faction == self.Faction)
                { myZone.ally = true; }
                returnList.Add(myZone);
            }


            /*foreach (var minion in world.Minions)
            {
                HotZone myZone = new HotZone(minion.X, minion.Y);
                if (minion.Faction == self.Faction)
                { myZone.ally = true; }
                returnList.Add(myZone);
            }*/
            return returnList;
        }

        /// <summary>
        /// !!! НЕ РЕАЛИЗЛВАНО!!! Рассылает команды союзникам, если мы - верховный маг
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        public void setKommand(World world, Game game, Wizard self)
        {
            //первым делом проверяем - вырховный ли мы маг?
            if (self.IsMaster)
            {
                //рассылаем команды
            }
        }

        /// <summary>
        /// Метод возвращает ближайшую вражескую цель для атаки
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Живой юнит для атаки</returns>
        private LivingUnit getNearestTarget(World world, Wizard self)
        {
            List<LivingUnit> targets = new List<LivingUnit>();
            foreach (LivingUnit element in world.Buildings)
            {
                targets.AddRange(world.Buildings);
            }
            foreach (LivingUnit element in world.Wizards)
            {
                targets.AddRange(world.Wizards);
            }
            foreach (LivingUnit element in world.Minions)
            {
                targets.AddRange(world.Minions);
            }

            LivingUnit nearestTarget = null;
            double nearestTargetDistance = double.MaxValue;

            foreach (LivingUnit target in targets)
            {
                if (target.Faction == Faction.Neutral || target.Faction == self.Faction)
                {
                    continue;
                }

                double distance = self.GetDistanceTo(target);

                if (distance < nearestTargetDistance)
                {
                    nearestTarget = target;
                    nearestTargetDistance = distance;
                }
            }
            return nearestTarget;
        }

        /// <summary>
        /// Возвращает ближайшую союзную постройку
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="self">Собственный маг</param>
        /// <returns></returns>
        private LivingUnit getNearestBuilding(World world, Wizard self)
        {
            List<LivingUnit> targetsBuilding = new List<LivingUnit>();
            foreach (LivingUnit element in world.Buildings)
            {
                targetsBuilding.AddRange(world.Buildings);
            }
            LivingUnit nearestBuilding = null;
            double nearestBuildingDistance = double.MaxValue;

            foreach (LivingUnit targetBuilding in targetsBuilding)
            {
                if (targetBuilding.Faction == Faction.Neutral || targetBuilding.Faction != self.Faction)
                {
                    continue;
                }

                double distance = self.GetDistanceTo(targetBuilding);

                if (distance < nearestBuildingDistance)
                {
                    nearestBuilding = targetBuilding;
                    nearestBuildingDistance = distance;
                }

            }
            return nearestBuilding;
        }

        /// <summary>
        /// Возвращает координату нашей базы
        /// </summary>
        /// <returns></returns>
        private Point2D getMyBase(Wizard self)
        {
            Point2D myBase;
            if (self.Faction == Faction.Academy)
            {
                myBase = new Point2D(500, 3500);
            } else
            {
                myBase = new Point2D(3500, 500);
            }
            return myBase;
        }

        /// <summary>
        /// Возвращает координату вражеской базы
        /// </summary>
        /// <param name="self">Собственный маг</param>
        /// <returns></returns>
        private Point2D getEnemyBase(Wizard self)
        {
            Point2D enemyBase;
            if (self.Faction == Faction.Academy)
            {
                enemyBase = new Point2D(3500, 500);
            } else
            {
                enemyBase = new Point2D(500, 3500);
            }
            return enemyBase;
        }
    }
}
