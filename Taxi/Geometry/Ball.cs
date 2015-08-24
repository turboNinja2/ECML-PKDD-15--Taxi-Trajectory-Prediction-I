namespace Taxi
{
    /// <summary>
    /// Representation of a ball : \f$(x,y)\f$ represents the center, \f$r\f$ the radius.
    /// </summary>
    public struct Ball
    {
        private double _x;
        private double _y;
        private double _radius;

        /// <summary>
        /// \f$x\f$ accessor
        /// </summary>
        public double X
        {
            get { return _x; }
        }

        /// <summary>
        /// \f$y\f$ accessor
        /// </summary>
        public double Y
        {
            get { return _y; }
        }
        
        /// <summary>
        /// \f$r\f$ accessor
        /// </summary>
        public double Radius
        {
            get { return _radius; }
        }

        /// <summary>
        /// Constructs a ball, centered in \f$(x,y)\f$ with radius \f$r\f$.
        /// </summary>
        /// <param name="x">\f$x\f$</param>
        /// <param name="y">\f$y\f$</param>
        /// <param name="radius">\f$r\f$</param>
        public Ball(double x, double y, double radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }
    }
}
