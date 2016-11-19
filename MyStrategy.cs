using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;

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
        VisualClient vc = new VisualClient("localhost", 13579);


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

            vc.BeginPost(); //�������� ��������� ������ ���� �������� �����-�������
            if (self.IsMaster)
            {
                //�������� ����� setKommand � �������-���������

                //���� ���������, ����� �� �� �������� ������� ����, ������� ����
            }

            hotZone = myStrat.getHotZone(world, game, self);

            if(hotZone!=null)
            {
                vc.Line(self.X, self.Y, hotZone.getX(), hotZone.getY(), 1.0f, 0.0f, 0.0f);
                
            }

            vc.Circle(self.X, self.Y, HOT_ZONE_POROG, 1.0f, 1.0f, 0.0f);
            vc.Circle(self.X, self.Y, ENEMY_POROG, 1.0f, 0.0f, 1.0f);
            List<LinePoint> MapPoint = myTracer.getPointMap(world);
            foreach(LinePoint LP in MapPoint)
            {
                vc.FillCircle(LP.X, LP.Y, 5, 0.0f, 1.0f, 0.0f);
            }

            double nearestTargetDistance = myTactic.getNearestTargetDistance(world, self);
            if (nearestTargetDistance>ENEMY_POROG && self.GetDistanceTo(hotZone.getX(), hotZone.getY()) > HOT_ZONE_POROG)
            {
                //myTracer.goTo(hotZone, world, game, self, move);
                myTracer.goToVisual(hotZone, world, game, self, move, vc);
                vc.Text(self.X, self.Y + 50, "TRACER", 0.0f, 0.0f, 1.0f);
            } else
            {
                myTactic.getTacticMove(world, game, self, move);
                vc.Text(self.X, self.Y + 50, "TACTIC", 0.0f, 0.0f, 1.0f);
            }

            //�������� ����� getHotZone � �������-���������


            vc.EndPost(); //����������� ���������
        }
    }
}