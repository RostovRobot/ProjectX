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
        /// <summary>
        /// !!! НЕ РЕАЛИЗОВАНО!!! Возвращает координаты самой важной зоны
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns></returns>
        public Point2D getHotZone(World world, Game game, Wizard self)
        {
            return new Point2D();
        }

        public void setKommand(World world, Game game, Wizard self)
        {

        }
    }
}
