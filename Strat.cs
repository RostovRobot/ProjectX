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
        private LaneType lane; // переменная в которую задаем нашу линию
        /*private Wizard self;
        private World world;
        private Game game;
        private Move move;*/

        private double lowHPFactor;

        /// <summary>
        /// Возвращает координаты самой важной зоны
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Точка на двухмерной карте</returns>
        public Point2D getHotZone(World world, Game game, Wizard self)
        {
            Point2D HotZone = new Point2D(world.Width / 2, world.Height/2);
            LivingUnit nearestTarget = getNearestTarget(world, self);
            LivingUnit nearestBuilding = getNearestBuilding(world, self);
            Point2D myBase = getMyBase(self);

            if (self.Life < self.MaxLife * lowHPFactor)
            {
                HotZone = myBase;

                if (nearestTarget != null)
                {
                    double distance = self.GetDistanceTo(nearestTarget);

                    if (distance <= self.CastRange)
                    {
                        HotZone = new Point2D(nearestTarget.X,nearestTarget.Y);
                    }
                }
            }
            else
            {
                HotZone = new Point2D(2000, 2000);
            }
            return HotZone;
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
            if(self.IsMaster)
            {
                //рассылаем команды
            }
        }
        /// <summary>
        /// Метод возвращает ближайшую вражескую цель для атаки
        /// </summary>
        /// <param name="world"></param>
        /// <param name="self"></param>
        /// <returns></returns>
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
                myBase = new Point2D(500, 500);
            } else
            {
                myBase = new Point2D(3500, 3500);
            }
            return myBase;
        }
    }
}
