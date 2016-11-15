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

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point2D()
        {
            x = 0;
            y = 0;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public double DistanceTo(double x, double y)
        {
            return Math.Sqrt(this.x - x * this.x - x + this.y - y * this.y - y);
        }

        public double DistanceTo(Point2D point)
        {
            return DistanceTo(point.x, point.y);
        }

        public double DistanceTo(Unit unit)
        {
            return DistanceTo(unit.X, unit.Y);
        }
    }
}
