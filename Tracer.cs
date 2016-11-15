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
        private double WAYPOINT_RADIUS = 100.0D;
          
        private Wizard self;
        private World world;
        private Game game;
        private Move move;
        /// <summary>
        /// Прокладка маршрута до приоритетной зоны
        /// </summary>
        /// <param name="point">Координаты приоритетной зоны в виде точки</param>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Объект-движение</returns>
        /// private void initializeTick(Wizard self, World world, Game game, Move move)
        private void initializeTick(Wizard self, World world, Game game, Move move)
        {
            this.self = self;
            this.world = world;
            this.game = game;
            this.move = move;
        }
        public List<LinePoint> TracerBase(List<LinePoint> points, LinePoint needPoint)
        {

            //List<LinePoint>[] FullWaypoints = new List<LinePoint>[3];
            int CenterLinePoint = 3;
            List<LinePoint> Waypoints = new List<LinePoint>();
            double Min = 2000;
            double Minf = 0;
            int NearestPoint = 0;
            for (int i = 0; i < points.Count-1; i++)
            {
                double px = points[i].X;
                double py = points[i].Y;
                double resX = px-self.X;
                double resY = py-self.Y;
                Minf = Math.Sqrt(Math.Pow(resX, 2) + Math.Pow(resY, 2));
                if (Min>Minf)
                {
                    Min = Minf;
                    NearestPoint = i;
                }
            }

            if(points[NearestPoint].line != needPoint.line)//
            {
                if (points[NearestPoint].index != CenterLinePoint)
                {
                    if (points[NearestPoint].index < CenterLinePoint)
                    {
                        for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)
                        {
                            Waypoints.Add(points[i]);
                        }
                        Waypoints.Add(points[CenterLinePoint]);//line1
                        if (needPoint.line == 0)
                        {
                            Waypoints.Add(points[CenterLinePoint]);//line0
                            if (points[NearestPoint].index < CenterLinePoint)//needpoint
                            {
                                for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)//i--     (i=3;i>=цели;i--)
                                {
                                    Waypoints.Add(points[i]);
                                }
                            }
                            if (points[NearestPoint].index > CenterLinePoint)
                            {
                                for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)//(i=3;i<=цели;i++)
                                {
                                    Waypoints.Add(points[i]);
                                }
                            }
                        }
                        else
                        {
                            if (needPoint.line == 0)
                            {
                                Waypoints.Add(points[CenterLinePoint]);//line2
                                if (points[NearestPoint].index < CenterLinePoint)//need
                                {
                                    for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)
                                    {
                                        Waypoints.Add(points[i]);
                                    }
                                }
                                else
                                {
                                    for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)
                                    {
                                        Waypoints.Add(points[i]);
                                    }
                                }
                            }
                            else
                            {
                                Waypoints.Add(points[CenterLinePoint]);
                                if (points[NearestPoint].index < CenterLinePoint)
                                {
                                    for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)
                                    {
                                        Waypoints.Add(points[i]);
                                    }
                                }
                                else
                                {
                                    for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)
                                    {
                                        Waypoints.Add(points[i]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)
                        {
                            Waypoints.Add(points[i]);
                        }
                        Waypoints.Add(points[CenterLinePoint]);
                        if (points[NearestPoint].line != needPoint.line)
                        {
                            if (points[NearestPoint].index < needPoint.index)
                            {
                                for (int i = points[NearestPoint].index; i <= needPoint.index; i++)
                                {
                                    Waypoints.Add(points[i]);
                                }
                            }
                            else
                            {
                                for (int i = points[NearestPoint].index; i >= needPoint.index; i++)
                                {
                                    Waypoints.Add(points[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Waypoints.Add(points[CenterLinePoint]);
                    if (needPoint.line == 0)
                    {
                        Waypoints.Add(points[CenterLinePoint]);
                        if (points[NearestPoint].index < CenterLinePoint)
                        {
                            for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)
                            {
                                Waypoints.Add(points[i]);
                            }
                        }
                        else
                        {
                            for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)
                            {
                                Waypoints.Add(points[i]);
                            }
                        }
                    }
                    else
                    {
                        Waypoints.Add(points[CenterLinePoint]);
                        if (points[NearestPoint].index < CenterLinePoint)
                        {
                            for (int i = points[NearestPoint].index; i <= CenterLinePoint; i++)
                            {
                                Waypoints.Add(points[i]);
                            }
                        }
                        else
                        {
                            for (int i = points[NearestPoint].index; i >= CenterLinePoint; i++)
                            {
                                Waypoints.Add(points[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (points[NearestPoint].index < needPoint.index)
                {
                    for (int i = points[NearestPoint].index; i <= needPoint.index; i++)
                    {
                        Waypoints.Add(points[i]);
                    }
                }
                else
                {
                    for (int i = points[NearestPoint].index; i >= needPoint.index; i++)
                    {
                        Waypoints.Add(points[i]);
                    }
                }
            }
            return Waypoints;
        }

        public void TraceMid()
        {
            //return;
        }


        public Move getTrace(Point2D point, World world, Game game, Wizard self)
        {

            return new Move();
        }

        /*private void goTo(Point2D point)
        {
            double angle = self.GetAngleTo(point.X, point.Y);

            move.Turn = angle;

            if (Math.Abs(angle) < game.StaffSector / 4.0D)
            {
                move.Speed = game.WizardForwardSpeed;
            }
        }*/

        /*private Point2D NextWaypoint()
        {
            int lastWaypointIndex = waypoints.Length - 1;
            Point2D lastWaypoint = waypoints[lastWaypointIndex];

            for (int waypointIndex = 0; waypointIndex < lastWaypointIndex; ++waypointIndex)
            {
                Point2D waypoint = waypoints[waypointIndex];

                if (waypoint.DistanceTo(self) <= WAYPOINT_RADIUS)
                {
                    return waypoints[waypointIndex + 1];
                }

                if (lastWaypoint.DistanceTo(waypoint) < lastWaypoint.DistanceTo(self))
                {
                    return waypoint;
                }
            }

            return lastWaypoint;
        }
        private Point2D PreviousWaypoint()
        {
            Point2D firstWaypoint = waypoints[0];

            for (int waypointIndex = waypoints.Length - 1; waypointIndex > 0; --waypointIndex)
            {
                Point2D waypoint = waypoints[waypointIndex];

                if (waypoint.DistanceTo(self) <= WAYPOINT_RADIUS)
                {
                    return waypoints[waypointIndex - 1];
                }

                if (firstWaypoint.DistanceTo(waypoint) < firstWaypoint.DistanceTo(self))
                {
                    return waypoint;
                }
            }
            return firstWaypoint;
        }*/
    }
}
