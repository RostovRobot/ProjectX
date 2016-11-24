﻿using System;
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
        private void initializeTick(Wizard self, World world, Game game, Move move)
        {
            this.self = self;
            this.world = world;
            this.game = game;
            this.move = move;
        }

        /// <summary>
        /// Получение маршрута от точки к точке, через центр либо базу
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="needPoint2D">Точка, на которую надо прийти</param>
        /// <param name="transition">Индекс для перехода по линиям</param>
        /// <returns>Список вэйпоинтов для пути в виде коллекции</returns>
        public List<LinePoint> tracerGet(List<LinePoint> points, Point2D needPoint2D, int transition, Wizard self)
        {

            List<LinePoint>[] FullWaypoints = new List<LinePoint>[5];
            for (int i = 0; i < 5; i++)
                FullWaypoints[i] = new List<LinePoint>();
            for (int i = 0; i < points.Count; i++)
            {
                FullWaypoints[points[i].line].Add(points[i]);

            }
            List<LinePoint> Waypoints = new List<LinePoint>();
            double SMin = 2000;
            double SMinf = 0;
            double NMin = 2000;
            double NMinf = 0;
            int NearestPoint = 0;
            int NearestPointLine = 0;
            LinePoint needPoint;
            int ineedPoint = 0;
            for (int i = 0; i < points.Count; i++)
            {
                double px = points[i].X;
                double py = points[i].Y;
                double resX = px - self.X;
                double resY = py - self.Y;
                SMinf = Math.Sqrt(Math.Pow(resX, 2) + Math.Pow(resY, 2));
                if (SMin > SMinf)
                {
                    SMin = SMinf;
                    NearestPoint = i;
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                double px = points[i].X;
                double py = points[i].Y;
                double resX = px - needPoint2D.getX();
                double resY = py - needPoint2D.getY();
                NMinf = Math.Sqrt(Math.Pow(resX, 2) + Math.Pow(resY, 2));
                if (NMin > NMinf)
                {
                    NMin = NMinf;
                    ineedPoint = i;
                }
            }
            needPoint = points[ineedPoint];
            NearestPointLine = points[NearestPoint].line;
            double distans01;
            double distans12;
            double px = needPoint.X;
            double py = needPoint.Y;
            double resX1 = px - FullWaypoints[4][3].X;
            double resY1 = py - FullWaypoints[4][3].Y;
            double resX2 = px - FullWaypoints[5][3].X;
            double resY2 = py - FullWaypoints[5][3].Y;
            distans01 = Math.Sqrt(Math.Pow(resX1, 2) + Math.Pow(resY1, 2));
            distans12 = Math.Sqrt(Math.Pow(resX2, 2) + Math.Pow(resY2, 2));
            if (NearestPointLine == needPoint.line)
            {
                if (points[NearestPoint].index <= needPoint.index)
                {
                    for (int i = points[NearestPoint].index; i <= needPoint.index; i++)
                    {
                        Waypoints.Add(FullWaypoints[NearestPointLine][i]);
                    }
                }
                else
                {
                    for (int i = points[NearestPoint].index; i >= needPoint.index; i--)
                    {
                        Waypoints.Add(FullWaypoints[NearestPointLine][i]);
                    }
                }
            }
            else
            {
                if (points[NearestPoint].index < transition)
                {
                    for (int i = points[NearestPoint].index; i <= transition; i++)
                    {
                        Waypoints.Add(FullWaypoints[NearestPointLine][i]);
                    }
                }
                else
                {
                    for (int i = points[NearestPoint].index; i >= transition; i--)
                    {
                        Waypoints.Add(FullWaypoints[NearestPointLine][i]);
                    }
                }

                if (distans01 < distans12)
                {
                    Waypoints.Add(FullWaypoints[4][3]);
                }
                else
                {
                    Waypoints.Add(FullWaypoints[5][3]);
                }
                Waypoints.Add(FullWaypoints[needPoint.line][transition]);
                if (points[transition].index <= needPoint.index)
                {
                    for (int i = transition; i <= needPoint.index; i++)
                    {
                        Waypoints.Add(FullWaypoints[needPoint.line][i]);
                    }
                }
                else
                {
                    for (int i = transition; i >= needPoint.index; i--)
                    {
                        Waypoints.Add(FullWaypoints[needPoint.line][i]);
                    }
                }
            }
            return Waypoints;
        }

        /// <summary>
        /// Создание списка точек
        /// </summary>
        /// <param name="world">Игровой мир</param>
        /// <returns>Список точек в виде коллекции</returns>
        public List<LinePoint> getPointMap(World world)
        {
            List<LinePoint> FullWayPoint = new List<LinePoint>();
            //line0
            FullWayPoint.Add(new LinePoint(200, 3400, 0, 0));
            FullWayPoint.Add(new LinePoint(200, 2600, 0, 1));
            FullWayPoint.Add(new LinePoint(200, 1700, 0, 2));
            FullWayPoint.Add(new LinePoint(300, 300, 0, 3));
            FullWayPoint.Add(new LinePoint(1700, 200, 0, 4));
            FullWayPoint.Add(new LinePoint(2600, 200, 0, 5));
            FullWayPoint.Add(new LinePoint(3400, 200, 0, 6));
            //line1
            FullWayPoint.Add(new LinePoint(600, 3400, 1, 0));
            FullWayPoint.Add(new LinePoint(1100, 2900, 1, 1));
            FullWayPoint.Add(new LinePoint(1600, 2400, 1, 2));
            FullWayPoint.Add(new LinePoint(2000, 2000, 1, 3));
            FullWayPoint.Add(new LinePoint(2400, 1600, 1, 4));
            FullWayPoint.Add(new LinePoint(2900, 1100, 1, 5));
            FullWayPoint.Add(new LinePoint(3400, 600, 1, 6));
            //line2
            FullWayPoint.Add(new LinePoint(600, 3800, 2, 0));
            FullWayPoint.Add(new LinePoint(1400, 3800, 2, 1));
            FullWayPoint.Add(new LinePoint(2300, 3800, 2, 2));
            FullWayPoint.Add(new LinePoint(3700, 3700, 2, 3));
            FullWayPoint.Add(new LinePoint(3800, 2300, 2, 4));
            FullWayPoint.Add(new LinePoint(3800, 1400, 2, 5));
            FullWayPoint.Add(new LinePoint(3800, 600, 2, 6));
            //line0-1
            FullWayPoint.Add(new LinePoint(800, 800, 3, 3));
            //line1-2
            FullWayPoint.Add(new LinePoint(3200, 3200, 4, 3));
            return FullWayPoint;
        }
        
        /// <summary>
        /// Выбор пути через базу или центр
        /// </summary>
        /// <param name="point">Точка, в которую надо прийти</param>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <returns>Путь, состоящий из вэйпоинтов</returns>
        public List<LinePoint> getTrace(Point2D point, World world, Game game, Wizard self)
        {
            List<LinePoint> FullWayPoint;
            List<LinePoint> tracebase;
            FullWayPoint = getPointMap(world);
            if (self.Faction == Faction.Academy)
            {
                tracebase = tracerGet(FullWayPoint, point, 0, self);
            }
            else
            {
                tracebase = tracerGet(FullWayPoint, point, 6, self);
            }
            List<LinePoint> tracemid = tracerGet(FullWayPoint, point, 3, self);
            double summid = 0;
            double sumbase = 0;
            double px = 0;
            double py = 0;
            double resX = 0;
            double resY = 0;
            for (int i = 0; i < tracebase.Count - 2; i++)
            {
                px = tracebase[i].X;
                py = tracebase[i].Y;
                resX = px - tracebase[i + 1].X;
                resY = py - tracebase[i + 1].Y;
                sumbase = sumbase + Math.Sqrt(resX * resX + resY * resY);
            }
            for (int i = 0; i < tracemid.Count - 2; i++)
            {
                px = tracemid[i].X;
                py = tracemid[i].Y;
                resX = px - tracemid[i + 1].X;
                resY = py - tracemid[i + 1].Y;
                summid = summid + Math.Sqrt(resX * resX + resY * resY);
            }
            if (sumbase < summid)
            {
                return tracebase;
            }
            else
            {
                return tracemid;
            }
        }

        public Point2D GetNextWaypoint(Point2D NextWaypoint, Point2D point)
        {
            List<LinePoint> trace = new List<LinePoint>();
            trace = getTrace(point, world, game, self);
            NextWaypoint = trace[1];
            return NextWaypoint;
        }

        public Point2D GetLastWaypoint(Point2D LastWaypoint, Point2D point)
        {
            List<LinePoint> trace = new List<LinePoint>();
            trace = getTrace(point, world, game, self);
            if(LastWaypoint == null)
            {
                LastWaypoint = trace[0];
            }
            if (self.GetDistanceTo(trace[0].X, trace[0].Y) < 60)
            {
                LastWaypoint = trace[0];
            }
            return LastWaypoint;
        }

        public int i = 0; //i-логическая переменная
        /// <summary>
        /// Передвижение
        /// </summary>
        /// <param name="point">Класс, описывающий точку на карте</param>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <param name="move">Управление магом</param>
        public void goTo(Point2D point, World world, Game game, Wizard self,Move move)
        {
            List<LinePoint> trace = new List<LinePoint>();
            trace = getTrace(point, world, game, self);

            
            double angle = self.GetAngleTo(trace[0].X, trace[0].Y);
            double px = trace[0].X;
            double py = trace[0].Y;
            double distance10;
            double px1 = trace[0].X;
            double py1 = trace[0].Y;
            double px2 = trace[1].X - trace[0].X;
            double py2 = trace[1].Y - trace[0].Y;
            distance10 = Math.Sqrt(Math.Pow(px2, 2) + Math.Pow(py2, 2));
            if (trace.Count > 1)
            {
                if (isCrash(self) == true)
                {
                    CrashedMove(move, self, game);
                }
                else
                {
                    double resX = px - trace[1].Y;
                    double resY = py - trace[1].X;
                    double res = Math.Sqrt(Math.Pow(resX, 2) + Math.Pow(resY, 2));

                    if (self.GetDistanceTo(trace[0].X, trace[0].Y) < 60)
                    {
                        i = 1;
                    }
                    if (i == 1)
                    {
                        angle = self.GetAngleTo(trace[1].X, trace[1].Y);
                        if (self.GetDistanceTo(trace[0].X, trace[0].Y) >= res / 2)
                        {
                            i = 0;
                        }
                    }
                    else
                    {
                        angle = self.GetAngleTo(trace[0].X, trace[0].Y);
                        
                    }

                    move.Turn = angle;

                    if (Math.Abs(angle) < game.StaffSector / 4.0D)
                    {
                        move.Speed = game.WizardForwardSpeed;
                    }
                }
            }
            else
            {
                move.Turn = angle;

                if (Math.Abs(angle) < game.StaffSector / 4.0D)
                {
                    move.Speed = game.WizardForwardSpeed;
                }
            }
        }

        /// <summary>
        /// Передвижение с отрисовкой
        /// </summary>
        /// <param name="point">Класс, описывающий точку на карте</param>
        /// <param name="world">Игровой мир</param>
        /// <param name="game">Константы игры</param>
        /// <param name="self">Собственный маг</param>
        /// <param name="move">Управление магом</param>
        /// <param name="vc">Объект-визуализатор</param>
        public void goToVisual(Point2D point, World world, Game game, Wizard self, Move move, VisualClient vc)
        {
            List<LinePoint> trace = new List<LinePoint>();
            trace = getTrace(point, world, game, self);

            //отрисовываем все отрезки пути до hotZone
            int iCount = 0;
            foreach(LinePoint LP in trace)
            {
                double myX1;
                double myY1;
                if (iCount < 1)
                {
                    myX1 = self.X;
                    myY1 = self.Y;
                }else
                {
                    myX1 = trace.ElementAt(iCount - 1).X;
                    myY1 = trace.ElementAt(iCount - 1).Y;
                }
                iCount++;
                double myX2 = LP.X;
                double myY2 =LP.Y;
                vc.Line(myX1, myY1, myX2, myY2, 0.0f, 1.0f, 0.0f);
            }

            double angle = self.GetAngleTo(trace[0].X, trace[0].Y);
            double px = trace[0].X;
            double py = trace[0].Y;
            
            if (trace.Count > 1)
            {
                if (isCrash(self) == true)
                {
                    CrashedMove(move, self, game);
                } else
                {
                    double resX = px - trace[1].Y;
                    double resY = py - trace[1].X;
                    double res = Math.Sqrt(Math.Pow(resX, 2) + Math.Pow(resY, 2));

                    if (self.GetDistanceTo(trace[0].X, trace[0].Y) < 60)
                    {
                        i = 1;
                    }
                    if (i == 1)
                    {
                        angle = self.GetAngleTo(trace[1].X, trace[1].Y);
                        if (self.GetDistanceTo(trace[0].X, trace[0].Y) >= res / 2)
                        {
                            i = 0;
                        }
                    } else
                    {
                        angle = self.GetAngleTo(trace[0].X, trace[0].Y);
                    }

                    move.Turn = angle;

                    if (Math.Abs(angle) < game.StaffSector / 4.0D)
                    {
                        move.Speed = game.WizardForwardSpeed;
                    }
                }
            } else
            {
                move.Turn = angle;

                if (Math.Abs(angle) < game.StaffSector / 4.0D)
                {
                    move.Speed = game.WizardForwardSpeed;
                }
            }
        }

        private int steps = 0;
        private double oldX = 0;
        private double oldY = 0;
        public int ago = 30;
        private int deltaPorog = 70;  //подобрать
        private int stepsPorog = 80;  //подобрать
        public bool isCrash(Wizard self)
        {
            double deltaX = self.X - oldX;
            double deltaY = self.Y - oldY;
            double delta = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (delta < deltaPorog)
            {
                steps++;
            }
            else
            {
                steps = 0;
                oldX = self.X;
                oldY = self.Y;
            }
            
            if (steps > stepsPorog || ago != 0)
            {
                if (ago == 0)
                {
                    ago = 30;
                }
                ago = ago - 1;
                return true;

            }
            else
            {
                return false;
            }
        }

        public void CrashedMove(Move move, Wizard self,Game game)
        {
                move.Speed = - game.WizardForwardSpeed;
        }


        public  LinePoint getLastPointLine(World world, Game game, Wizard self)
        {
            List<LinePoint> llp = new List<LinePoint>();
            llp = getPointMap(world);
            Point2D point = new Point2D(10000, 10000);
            LinePoint lp = new LinePoint();
            int ind = -1;
            if (getTrace(point, world, game, self)[0].index > 0)
            {
                ind = getTrace(point, world, game, self)[0].index - 1;
            }
            else
            {
                ind = getTrace(point, world, game, self)[0].index;
            }

            int ln = getTrace(point, world, game, self)[0].line;

            
                for(int i=0;i<llp.Count;i++)
                {
                    if((llp[i].line==ln)&&(llp[i].index == ind))
                {
                    lp = llp[i];
                }
                }


            return lp;
        }
        
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

