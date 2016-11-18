using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    /// <summary>
    /// Класс, описывающий точку на карте
    /// </summary>
    class Point2D
    {
        /// <summary>
        /// Координата X
        /// </summary>
        private double x;
        /// <summary>
        /// Координата Y
        /// </summary>
        private double y;

        /// <summary>
        /// Метод конструктор заданными координатами
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Метод конструктор без параметров
        /// </summary>
        public Point2D()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Возвращает координату X
        /// </summary>
        /// <returns>Координата X в виде дробного числа</returns>
        public double getX()
        {
            return x;
        }

        /// <summary>
        /// Возвращает координату Y
        /// </summary>
        /// <returns>Координата Y в виде дробного числа</returns>
        public double getY()
        {
            return y;
        }

        /// <summary>
        /// Определение дистанции до точки с заданными координатами
        /// </summary>
        /// <param name="x">Координата X определяемой точки</param>
        /// <param name="y">Координата Y определяемой точки</param>
        /// <returns>Дистанция в виде дробного числа</returns>
        public double DistanceTo(double x, double y)
        {
            return Math.Sqrt(this.x - x * this.x - x + this.y - y * this.y - y);
        }

        /// <summary>
        /// Определение дистанции до точки
        /// </summary>
        /// <param name="point">Точка, до которой опредеяется дистанция</param>
        /// <returns>Дистанция в виде дробного числа</returns>
        public double DistanceTo(Point2D point)
        {
            return DistanceTo(point.x, point.y);
        }

        /// <summary>
        /// Определение дистанции до юнита
        /// </summary>
        /// <param name="unit">Юнит, до которого определяется дистанция</param>
        /// <returns>Дистанция в виде дробного числа</returns>
        public double DistanceTo(Unit unit)
        {
            return DistanceTo(unit.X, unit.Y);
        }
    }
}
