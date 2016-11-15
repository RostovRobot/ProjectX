using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    /// <summary>
    /// Класс, описывающий актуальные точки на карте.
    /// </summary>
    class LinePoint : Point2D
    {
        /// <summary>
        /// Линия, на которой находится точка.
        /// </summary>
        public int line;

        /// <summary>
        /// Номер точки на линии.
        /// </summary>
        public int index;

        /// <summary>
        /// Метод-конструктор с заданными координатами
        /// </summary>
        /// <param name="line">Линия, на которой находится точка</param>
        /// <param name="index">Номер точки на линии</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public LinePoint(double x, double y, int line , int index)
        {
            X = x;
            Y = y;
            this.index = index;
            this.line = line;
        }

        /// <summary>
        /// Метод-конструктор с заданными координатами
        /// </summary>
        /// <param name="line">Линия, на которой находится точка</param>
        /// <param name="index">Номер точки на линии</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public LinePoint(int x, int y, int line, int index)
        {
            X = x;
            Y = y;
            this.index = index;
            this.line = line;
        }

        public LinePoint()
        {
            X = 0;
            Y = 0;
        }
    }
}
