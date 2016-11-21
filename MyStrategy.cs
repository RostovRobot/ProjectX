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

        //������� ����� ���������!!!!!
        VisualClient vc;

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

            double nearestTargetDistance = myTactic.getNearestTargetDistance(world, self);
            if (nearestTargetDistance>ENEMY_POROG && self.GetDistanceTo(hotZone.getX(), hotZone.getY()) > HOT_ZONE_POROG)
            {
                if (vc == null)
                {
                    myTracer.goTo(hotZone, world, game, self, move);
                } else
                {
                    myTracer.goToVisual(hotZone, world, game, self, move, vc);

                    vc.Text(self.X, self.Y + 50, "TRACER", 0.0f, 0.0f, 1.0f);
                }
            } else
            {
                myTactic.getTacticMove(world, game, self, move);
                if(vc!=null) vc.Text(self.X, self.Y + 50, "TACTIC", 0.0f, 0.0f, 1.0f);
            }

            //�������� ����� getHotZone � �������-���������

            if(vc!=null) vc.EndPost(); //����������� ���������
        }
    }
}