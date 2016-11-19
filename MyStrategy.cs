using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        const int TACTIC_POROG = 600;
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

            vc.Circle(self.X, self.Y, TACTIC_POROG, 1.0f, 0.0f, 1.0f);

            LivingUnit nearestTarget = myTactic.getNearlestTarget(myTactic.getTargets(world, self), self);
            double nearestTargetDistance=world.Height;
            if (nearestTarget != null)
            {
                nearestTargetDistance = self.GetDistanceTo(nearestTarget);
            }
            if (nearestTargetDistance>TACTIC_POROG && self.GetDistanceTo(hotZone.getX(), hotZone.getY()) > TACTIC_POROG)
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