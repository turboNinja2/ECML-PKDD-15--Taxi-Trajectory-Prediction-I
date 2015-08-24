using System;
using System.Collections.Generic;
using System.Globalization;

namespace Taxi
{
    /// <summary>
    /// Points with a mass : \f$(x,y,m)\f$.
    /// </summary>
    public class WeightedPoint
    {
        #region Private vars
        private double _x;
        private double _y;
        private double _weight;
        #endregion

        /// <summary>
        /// Returns \f$x\f$
        /// </summary>
        public double X
        {
            get { return _x; }
        }

        /// <summary>
        /// Returns \f$y\f$
        /// </summary>
        public double Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Returns \f$m\f$
        /// </summary>
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        #region Constructors
        /// <summary>
        /// Constructs a point with a mass \f$(x,y,m)\f$.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="weight"></param>
        public WeightedPoint(double x, double y, double weight)
        {
            _x = x;
            _y = y;
            _weight = weight;
        }
        /// <summary>
        /// Creates a null weighted point.
        /// </summary>
        public WeightedPoint()
        {
            _x = 0;
            _y = 0;
            _weight = 0;
        }
        /// <summary>
        /// (Deep) copy constructor of a weighted point.
        /// </summary>
        /// <param name="wp">Weighted point to copy.</param>
        public WeightedPoint(WeightedPoint wp)
        {
            _x = wp.X;
            _y = wp.Y;
            _weight = wp.Weight;
        }
        /// <summary>
        /// From string constructor of a weighted point.
        /// </summary>
        /// <param name="line">String "x_y"</param>
        public WeightedPoint(string line)
        {
            string[] res = line.Split('_');
            _x = Convert.ToDouble(res[0], CultureInfo.GetCultureInfo("en-US"));
            _y = Convert.ToDouble(res[1], CultureInfo.GetCultureInfo("en-US"));
        }

        #endregion

        /// <summary>
        /// Performs \f$(x_1+x_2,y_1+y_2,m_1+m_2)\f$.
        /// </summary>
        /// <param name="p2">The point to add.</param>
        /// <returns>(x_1+x_2,y_1+y_2,m_1+m_2).</returns>
        public WeightedPoint Add(WeightedPoint p2)
        {
            return new WeightedPoint(_x + p2.X , _y + p2.Y , _weight + p2.Weight);
        }

        /// <summary>
        /// Performs \f$(x_1/\lambda,y_1/\lambda,m_1/\lambda)\f$
        /// </summary>
        /// <param name="lambda">The division coefficient.</param>
        /// <returns>The scaled weighted point.</returns>
        public WeightedPoint Divide(double lambda)
        {
            return new WeightedPoint(_x / lambda, _y/ lambda, _weight / lambda);
        }

        /// <summary>
        /// Performs \f$(\lambda x_1,\lambda y_1,\lambda m_1)\f$.
        /// </summary>
        /// <param name="lambda">The multiplication coefficient.</param>
        /// <returns>The scaled weighted point.</returns>
        public WeightedPoint Multiply(double lambda)
        {
            return new WeightedPoint(_x * lambda, _y * lambda, _weight * lambda);
        }

        /// <summary>
        /// Given a list of WeightedPoints returns.
        /// </summary>
        /// <param name="weightedPoints">Weighted points to average.</param>
        /// <returns>The barycenter of the input points.</returns>
        public static WeightedPoint Barycenter(IList<WeightedPoint> weightedPoints)
        {
            WeightedPoint res = new WeightedPoint();
            double counter = 0;
            foreach (WeightedPoint pt in weightedPoints)
            {
                res = res.Add(pt.Multiply(pt.Weight));
                counter += pt.Weight;
            }
            res = res.Divide(counter);
            return res;
        }

        /// <summary>
        /// Represents the position of the weighted point as a string.
        /// </summary>
        /// <returns>"x_y"</returns>
        public new string ToString()
        {
            return Convert.ToString(_y, CultureInfo.GetCultureInfo("en-US")) + "," + Convert.ToString(_x, CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
