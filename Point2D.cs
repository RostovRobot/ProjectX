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
        /// Координата X точки
        /// </summary>
        public double X;
        /// <summary>
        /// Координата Y точки
        /// </summary>
        public double Y;

        /// <summary>
        /// Метод-конструктор с заданными координатами
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Метод-конструктор с заданными координатами
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Метод-конструктор без параметров (создает точку с нулевыми координатами)
        /// </summary>
        public Point2D()
        {
            X = 0;
            Y = 0;
        }
    }
}
