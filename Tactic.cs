﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{

    /// <summary>
    /// Класс, описывающий тактические приемы при встрече с врагом
    /// </summary>
    class Tactic
    {
        double DistanceK = 0.7;//коэффицент удаления
        double HealthK = 125;
        double CRITICAL_ENEMY_HEALTH_POROG = 30;
        double FATALITY_PRIORITY_BONUS = 250;//Бонус за добивание
        double CRITICAL_LIFE_POROG = 0.6;
        double obxoddist = 200;
        double FATALITY_WIZARD_K = 1.7;


        /// <summary>
        /// Точка для проверки на залипание
        /// </summary>
        public Point2D ZalipP = new Point2D(10000, 10000);

        /// <summary>
        /// Пороговое значение счетчика тиков на залипание
        /// </summary>
        public int controlZalipDistance = 30;

        /// <summary>
        /// Пороговое значение дистанции между магом и ближайшим врагом для отступления
        /// </summary>
        public int MIN_VRAG_DISTANCE = 140;

        /// <summary>
        /// Выбор тактических приемов
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <param name="move">Управление магом</param>
        /// <returns>Объект-движение</returns>
        // Добавил move, в который мы будем присваивать дополнительное движение.
        public Move getTacticMove(World world, Game game, Wizard self, Move move)
        {
               if ((IsZalip(world, game, self, move)) || (self.GetDistanceTo(ZalipP.getX(), ZalipP.getY()) < controlZalipDistance))
            {
                move.Speed = game.WizardBackwardSpeed;

            }
            LivingUnit target = getTartgetwithHigestPriority(world, self, game);
            
            getRocket(world, game, self, target, move);
            Point2D enemybase = GetEnemyBase(self);
            if (GetDistance(self.X, self.Y, enemybase.getX(), enemybase.getY()) < obxoddist)
            {

                BaseObxod(world, game, self, move, target, 30D);


            }
            else
            {
                Otstup(world, game, self, move);
            }
            if (((double)self.Life / self.MaxLife <= CRITICAL_LIFE_POROG) && (target != null))
            {

                LowHealthCheck(world, game, self, move);
                //move.Speed = -game.WizardBackwardSpeed;
            }
            return move;


            //return new Move();
        }
        int ZalipCount = 0;
        double LastX = 0;
        double LastY = 0;
        int TimeInZalip = 30;

        /// <summary>
        /// Определение "залипания" мага
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <param name="move">Управление магом</param>
        /// <returns>Залип ли маг?</returns>
        public bool IsZalip(World world, Game game, Wizard self, Move move)
        {
            double speed = Math.Sqrt(self.SpeedX * self.SpeedX + self.SpeedY * self.SpeedY);
            if (speed > 0)
            {
                double dist = GetDistance(self.X, self.Y, LastX, LastY);
                if (dist < speed / 10)
                {
                    ZalipCount++;

                } else
                {

                    ZalipCount = 0;
                }

            } else
            {
                ZalipP = new Point2D(self.X, self.Y);
            }
            LastX = self.X;
            LastY = self.Y;
            if (ZalipCount > TimeInZalip)
            {
                ZalipCount = 0;
                return true;

            }
            return false;
        }

        /// <summary>
        /// Определение наиболее приоритетной цели для атаки
        /// </summary>
        /// <param name="targets">Коллекция целей</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Приоритетная цель в формате живого юнита</returns>
        public LivingUnit getMostImportantTarget(List<LivingUnit> targets, Wizard self)//Подразумевается, что в коллекции идут элементы последоваттельно:волшебники, башни, миньоны
        {
            double mindist = double.MaxValue;

            Wizard targetwizard = null;
            foreach (var wizard in targets)
            {
                if (wizard is Wizard)
                {
                    double distance = GetDistance(self.X, self.Y, wizard.X, wizard.Y);
                    if (distance < mindist)
                    {
                        mindist = distance;
                        targetwizard = wizard as Wizard;
                    }
                }
            }

            if (targetwizard != null) { return targetwizard; }

            Building targetBuild = null;

            foreach (var build in targets)
            {
                if (build is Building)
                {
                    double distance = GetDistance(self.X, self.Y, build.X, build.Y);
                    if (distance < mindist)
                    {
                        mindist = distance;
                        targetBuild = build as Building;
                    }

                }
            }

            if (targetBuild != null) { return targetBuild; }

            Minion targetminion = null;

            foreach (var minion in targets)
            {
                if (minion is Minion)
                {
                    double distance = GetDistance(self.X, self.Y, minion.X, minion.Y);
                    if (distance < mindist)
                    {
                        mindist = distance;
                        targetminion = minion as Minion;
                    }
                }
            }

            return targetminion;

        }
                        
        public LivingUnit getTartgetwithHigestPriority(World world, Wizard self,Game game)
        {
            LivingUnit restarget = null;
            double maxPriority = Double.MinValue;
            var targets = getTargets(world, self);
            foreach (var target in targets)
            {
                
                double distance = GetDistance(self.X, self.Y, target.X, target.Y);
                if (distance < game.WizardCastRange)
                {
                    double targetpriority = game.WizardCastRange / distance * DistanceK;// + (double)target.MaxLife / (target.Life * target.Life) * HealthK;
                    if(target.Life<CRITICAL_ENEMY_HEALTH_POROG)
                    {
                        targetpriority += FATALITY_PRIORITY_BONUS;
                        if(target is Wizard)
                        {
                            targetpriority *= FATALITY_WIZARD_K;
                        }

                    }
                    if (targetpriority > maxPriority)
                    {
                        restarget = target;
                        maxPriority = targetpriority;
                    }
                }

            }


            return restarget;
        }


        public LivingUnit getNearlestTarget(List<LivingUnit> targets, Wizard self)//Подразумевается, что в коллекции идут элементы последоваттельно:волшебники, башни, миньоны
        {
            double mindist = double.MaxValue;

            LivingUnit target=null;
            foreach (var elem in targets)
            {
               
                    double distance = GetDistance(self.X, self.Y, elem.X, elem.Y);
                    if (distance < mindist)
                    {
                        mindist = distance;
                        target = elem;
                    }
                
            }

            //if (targetwizard != null) { return targetwizard; }



            return target;

        }

        

        /// <summary>
        /// Посик ближайшего врага
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Ближайший враг</returns>
        public LivingUnit getNearlestTarget(World world, Wizard self)
        {
            double mindist = double.MaxValue;
            List<LivingUnit> targets = getTargets(world, self);
            LivingUnit target = null;
            foreach (var elem in targets)
            {

                double distance = GetDistance(self.X, self.Y, elem.X, elem.Y);
                if (distance < mindist)
                {
                    mindist = distance;
                    target = elem;
                }

            }

            //if (targetwizard != null) { return targetwizard; }



            return target;
        }

       

        /// <summary>
        /// Определение расстояния до ближайшего врага
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Если врагов нет, то возвращает ширину игрового мира</returns>
        public double getNearestTargetDistance(World world, Wizard self)
        {
            LivingUnit target = getNearlestTarget(world, self);
            double distance = world.Width;
            if(target!=null)
            {
                distance = GetDistance(self.X, self.Y, target.X, target.Y);
            }
            return distance;
        }
        
        /// <summary>
        /// Определение доступных целей для атаки
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Коллекция доступных целей</returns>
        public List<LivingUnit> getTargets(World world, Wizard self)//0-Волшебники,1-миньоны, 2-строения
        {
            List<LivingUnit> targets = new List<LivingUnit>();

            foreach (var wizard in world.Wizards)
            {
                if (wizard.Faction != self.Faction)
                { targets.Add(wizard); }
            }


            foreach (var build in world.Buildings)
            {
                if (build.Faction != self.Faction)
                { targets.Add(build); }
            }


            foreach (var minion in world.Minions)
            {
                if ((minion.Faction != self.Faction) && (minion.Faction != Faction.Neutral))
                { targets.Add(minion); }
            }

            return targets;
        }


        /// <summary>
        /// Определение дистанции
        /// </summary>
        /// <param name="X1">Координата X первой точки</param>
        /// <param name="Y1">Координата Y первой точки</param>
        /// <param name="X2">Координата X второй точки</param>
        /// <param name="Y2">Координата Y второй точки</param>
        /// <returns>Дистанция в формате дробного числа</returns>
        public double GetDistance(double X1, double Y1, double X2, double Y2)
        {
            return Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));




        }

        public bool TacticIsAll(World world,Wizard self,Game game)
        {
            bool isAll = true;

            if (self.Life / self.MaxLife <= CRITICAL_LIFE_POROG)
            {
                isAll = false;
                
                
            }

            if (getTartgetwithHigestPriority(world,self,game)!=null)
            {
                isAll = false;


            }
            return isAll;

        }
        Point2D GetEnemyBase(Wizard self)
        {
          //  if (self.Faction == Faction.Academy)
          //{
              return new Point2D(3600, 400);
          //} else
          //{
          //      return new Point2D(400, 3600);
          //}

        }





        //
        //*******************************************************************************************************************************************************************************************
        //Некое разделение областей работы. Ниже будут описаны действия, используемые в тактике.
        //


        //Простейший запуск ракеты в заданного противника. Переписан с офсайта.
        //targetUnit здесь - цель, которую выбрали для атаки.
        //World, Game добавил на всякий случай. Могут пригодиться.
        /// <summary>
        /// Совершает выстрел в заданного протиыника
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <param name="targetUnit">Выбранная цель</param>
        /// <param name="move">Управление магом</param>
        public void getRocket(World world, Game game, Wizard self, LivingUnit targetUnit, Move move)
        {
            if (targetUnit != null)
            {

                if (self.GetDistanceTo(targetUnit) <= self.CastRange)
                {
                    double angle = self.GetAngleTo(targetUnit);
                    move.Turn = angle;
                    move.Speed = 0;
                    if (Math.Abs(angle) <= self.VisionRange)
                    {
                        move.Action = ActionType.MagicMissile;
                        move.CastAngle = angle;
                        move.MinCastDistance = self.GetDistanceTo(targetUnit) + self.Radius - targetUnit.Radius;
                    }


                }

            }
           


        }


        
        public void Otstup(World world, Game game, Wizard self,Move move)
        {
            LivingUnit vrag = getNearlestTarget(world, self);
            if(vrag!=null)
            {
                double d = self.GetDistanceTo(vrag);
                if(self.GetDistanceTo(vrag) <= MIN_VRAG_DISTANCE)
                {
                   
                    double angle = self.GetAngleTo(vrag);
                    move.Turn = angle;
                    if(angle==0)
                    {
                        move.Action = ActionType.Staff;
                        move.CastAngle = 0;

                        //move.Speed = -game.WizardBackwardSpeed;
                        LowHealthCheck(world, game, self, move);
                    }

                }

            }

        }


        public void LowHealthCheck(World world, Game game, Wizard self,Move move)
        {

            Tracer tr = new Tracer();
            double BaseX = tr.getLastPointLine(world,game,self).X, BaseY= tr.getLastPointLine(world, game, self).Y;

            double dist = Math.Sqrt(Math.Pow(self.X - BaseX, 2) + Math.Pow(self.Y - BaseY, 2));

            double angle = 180 - self.GetAngleTo(BaseX, BaseY) * 180 / Math.PI;
            if (angle >= 180)
                angle = angle - 360;           
            angle = angle / 180 * Math.PI;
            move.StrafeSpeed = 3.0 *  Math.Sin(angle);
            move.Speed = -3.0 * Math.Cos(angle);
            

        }
        public void BaseObxod(World world, Game game, Wizard self, Move move,LivingUnit enemy,double dist)
        {
            Point2D enemybase = GetEnemyBase(self);
            move.Turn = self.GetAngleTo(self.X,self.Y);
            move.StrafeSpeed = -5.0D;
            //if(50>GetDistance(self.X,self.Y,400,3600))
            //{
            //    move.Speed = 2.0D;

            //}
            //else
            //{
            //    move.Speed = -2.0D;
            //}

        }



    }
}
