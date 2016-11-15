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
        /// <summary>
        /// !!! НЕ РЕАЛИЗОВАНО!!! Возвращает координаты самой важной зоны
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Точка на двухмерной карте</returns>
        public Point2D getHotZone(World world, Game game, Wizard self)
        {
            lane = LaneType.Middle;// наша линия - мид
            Point2D HotZone = new Point2D(2000, 2000);
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

        public void Alliance()
        {

        }
    }
}
