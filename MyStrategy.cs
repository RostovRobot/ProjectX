using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;
using System;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        const int HOT_ZONE_POROG = 80;
        const int ENEMY_POROG = 600;
        Strat myStrat = new Strat();
        Tracer myTracer = new Tracer();
        Tactic myTactic = new Tactic();
        Point2D hotZone = new Point2D();
        bool isTactic = false;

        //������� ����� ���������!!!!!
        VisualClient vc=null;

        public void setVisual()
        {
            try
            {
                vc = new VisualClient("localhost", 13579);
            }
            catch (Exception e)
            {

            }
        }

        public void Move(Wizard self, World world, Game game, Move move)
        {
            #region
            //1. �������������� ������������.
            //��� ����� ��������� ��� ���� ���������� (���� �� - ���������)
            //����� ��������� �� ��������� ����������
            //��� ����� ���������� ��������� �� ������ ������ 
            //(������ ��� �����������, ��� ������ ������, ���� ��������� � �.�.)
            //�� ����-�� ������������ ������������� ���������:
            //���������� ������ (� �� �������/��������)
            //��������� ��������� (� �� �������/��������)
            //��������������� �������
            //����������� �������
            //���������� �� ���� �����
            //������� ������� �������
            // � �.�.
            //������ ���������� (��� �����), ���� ��������� ������ ���� (� ������� �����, ���� �� - ���������)
            //������������� - �������� ������

            //2. ��������� �������� �� ����� �� �������������� ������������
            //����� ������� ���� ���������� � �������� ���������� (�� �������������� ����). 
            //�������� �������, ������ � ���������, ������������ ����������� ������� �� ����� ����������.
            //�������� �� ������-�� ������ ���������� (���� �� ������ ���������� ��������).

            //3. ����������� ������������
            //��������� ������ ���� � ������������ ����:
            //����� ������ ��� ����� ����������
            //����� �� ����� ����������
            //����������� ����������� �� ����� ���������
            //����� ���� �����
            //������ ������
            //������������ �� ����� ����������
            //�������� ������ ��������� � ����, ������ ��������
            //������� ������
            //� �.�.
            //����� ���������� ������������ ������ �������
            //����� ����� ���� ������� ������������ ������������ ���������� � ������ ������ ��
            //������������� - �������� ������

            //4. ������� ��������� ��������
            //���������� ��������� ����������
            //����� ��������� ��� �������� ���� ��������� (���� �� - ���������)
            //


            //5. ���������� ��������
            //��� ����� ���� ���������
            //��� ���������� ����
            //� �.�.
            #endregion

            if (world.TickIndex < 1) setVisual();

            if (vc != null)
            {
                vc.BeginPre();
                vc.FillRect(0, 0, 500, 500, 1.0f, 0.0f, 1.0f);
                vc.FillRect(3500, 3500, 4000, 4000, 1.0f, 0.0f, 1.0f);
                vc.FillRect(1650, 1650, 2350, 2350, 1.0f, 0.0f, 1.0f);
                vc.EndPre();
                vc.BeginPost(); //�������� ��������� ������ ���� �������� �����-�������
            }
            if (self.IsMaster)
            {
                //�������� ����� setKommand � �������-���������

                //���� ���������, ����� �� �� �������� ������� ����, ������� ����
            }

            hotZone = myStrat.getHotZone(world, game, self);
            myStrat.getHotZone2(world, game, self, vc);
            if (vc != null)
            {
                if (hotZone != null)
                {
                    vc.Line(self.X, self.Y, hotZone.getX(), hotZone.getY(), 1.0f, 0.0f, 0.0f);

                }

                vc.Circle(self.X, self.Y, HOT_ZONE_POROG, 1.0f, 1.0f, 0.0f);
                vc.Circle(self.X, self.Y, ENEMY_POROG, 1.0f, 0.0f, 1.0f);
                List<LinePoint> MapPoint = myTracer.getPointMap(world);
                foreach (LinePoint LP in MapPoint)
                {
                    vc.FillCircle(LP.X, LP.Y, 5, 0.0f, 1.0f, 0.0f);
                }
            }
            int CD = self.RemainingCooldownTicksByAction[2];
            double nearestTargetDistance = myTactic.getNearestTargetDistance(world, self);
            if (nearestTargetDistance<ENEMY_POROG*0.9D || (nearestTargetDistance < ENEMY_POROG && CD<10) || self.GetDistanceTo(hotZone.getX(), hotZone.getY()) < HOT_ZONE_POROG)
            {
                myTactic.getTacticMove(world, game, self, move);
                if (vc != null) vc.Text(self.X, self.Y + 50, "TACTIC", 0.0f, 0.0f, 1.0f);
            } else
            {
                if (vc == null)
                {
                    myTracer.goTo(hotZone, world, game, self, move);
                } else
                {
                    myTracer.goToVisual(hotZone, world, game, self, move, vc);

                    vc.Text(self.X, self.Y + 50, "TRACER", 0.0f, 0.0f, 1.0f);
                }
            }
            
            /*if(nearestTargetDistance < ENEMY_POROG-50 ||(isTactic && nearestTargetDistance < ENEMY_POROG) || self.GetDistanceTo(hotZone.getX(), hotZone.getY()) < HOT_ZONE_POROG)
            {
                myTactic.getTacticMove(world, game, self, move);
                if (vc != null) vc.Text(self.X, self.Y + 50, "TACTIC", 0.0f, 0.0f, 1.0f);
                isTactic = true;
            } else
            {
                if (vc == null)
                {
                    myTracer.goTo(hotZone, world, game, self, move);
                } else
                {
                    myTracer.goToVisual(hotZone, world, game, self, move, vc);

                    vc.Text(self.X, self.Y + 50, "TRACER", 0.0f, 0.0f, 1.0f);
                }
                isTactic = false;
            }*/

            //�������� ����� getHotZone � �������-���������

            if(vc!=null) vc.EndPost(); //����������� ���������
        }
    }
}