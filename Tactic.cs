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
            

            return new Move();
        }
        public List<List<LivingUnit>> getTargets(World world, Wizard self)//0-Волшебники,1-миньоны, 2-строения
        {
            List<List<LivingUnit>> targets = new List<List<LivingUnit>>();
            targets.Add(new List<LivingUnit>());
            foreach (var wizard in world.Wizards)
            {
                if(wizard.Faction!=self.Faction)
                { targets[0].Add(wizard); }
            }

            targets.Add(new List<LivingUnit>());
            foreach (var wizard in world.Wizards)
            {
                if (wizard.Faction != self.Faction)
                { targets[1].Add(wizard); }
            }

            targets.Add(new List<LivingUnit>());
            foreach (var wizard in world.Wizards)
            {
                if (wizard.Faction != self.Faction)
                { targets[2].Add(wizard); }
            }

            return targets;
        }



        




        //
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
            if(targetUnit.GetDistanceTo(targetUnit)<=self.CastRange)
            {
                double angle = self.GetAngleTo(targetUnit);
                move.Turn = angle;
                
                if(Math.Abs(angle)<=self.VisionRange)
                {
                    move.Action = ActionType.MagicMissile;
                    move.CastAngle = angle;
                    move.MinCastDistance = self.GetDistanceTo(targetUnit) + self.Radius - targetUnit.Radius;
                }


            }

        }




    }
}
