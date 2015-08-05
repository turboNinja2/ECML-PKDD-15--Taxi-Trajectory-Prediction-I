namespace Taxi
{
    public struct Ball
    {
        private double _x;
        private double _y;
        private double _radius;

        public Ball(double x, double y, double radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public double X
        {
            get { return _x; }
        }
        public double Y
        {
            get { return _y; }
        }
        public double Radius
        {
            get { return _radius; }
        }
    }
}
