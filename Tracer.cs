using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    /// <summary>
    /// Класс, описывающий движение игрока до приоритетной зоны
    /// </summary>
    class Tracer
    {
        /// <summary>
        /// Прокладка маршрута до приоритетной зоны
        /// </summary>
        /// <param name="point">Координаты приоритетной зоны в виде точки</param>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Объект-движение</returns>
        public Move getTrace(Point2D point, World world, Game game, Wizard self)
        {
            return new Move();
        }
    }
}
