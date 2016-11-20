using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    class HotZone: Point2D
    {

        public double hot = 0.0D;
        public bool ally = false;

        public HotZone(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public HotZone(double x, double y, double hot)
        {
            this.x = x;
            this.y = y;
            this.hot = hot;
        }

        public HotZone(double x, double y, double hot, bool ally)
        {
            this.x = x;
            this.y = y;
            this.hot = hot;
            this.ally = ally;
        }

        public HotZone()
        {
            x = 0.0D;
            y = 0.0D;
            hot = 0.0D;
            ally = false;
        }

    }
}
