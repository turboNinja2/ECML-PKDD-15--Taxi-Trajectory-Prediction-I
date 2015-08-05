using System;
using System.Collections.Generic;
using System.Globalization;

namespace Taxi
{
    public class WeightedPoint
    {
        #region Private vars
        private double _x;
        private double _y;
        private double _weight;
        #endregion

        #region Constructors
        public WeightedPoint(double x, double y, double weight)
        {
            _x = x;
            _y = y;
            _weight = weight;
        }
        public WeightedPoint()
        {
            _x = 0;
            _y = 0;
            _weight = 0;
        }
        public WeightedPoint(WeightedPoint wp)
        {
            _x = wp.X;
            _y = wp.Y;
            _weight = wp.Weight;
        }


        public WeightedPoint(string line)
        {
            string[] res = line.Split('_');
            _x = Convert.ToDouble(res[0], CultureInfo.GetCultureInfo("en-US"));
            _y = Convert.ToDouble(res[1], CultureInfo.GetCultureInfo("en-US"));
        }

        #endregion

        public WeightedPoint Add(WeightedPoint p2)
        {
            return new WeightedPoint(_x + p2.X , _y + p2.Y , _weight + p2.Weight);
        }

        public WeightedPoint Divide(double elt)
        {
            return new WeightedPoint(_x / elt, _y/ elt, _weight / elt);

        }
        public WeightedPoint Multiply(double elt)
        {
            return new WeightedPoint(_x * elt, _y * elt, _weight * elt);
        }

        public static WeightedPoint Barycenter(IList<WeightedPoint> pts)
        {
            WeightedPoint res = new WeightedPoint();
            double counter = 0;
            foreach (WeightedPoint pt in pts)
            {
                res = res.Add(pt.Multiply(pt.Weight));
                counter += pt.Weight;
            }
            res = res.Divide(counter);
            return res;
        }

        public double X
        {
            get { return _x; }
        }
        public double Y
        {
            get { return _y; }
        }
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public string ToString()
        {
            return Convert.ToString(_y, CultureInfo.GetCultureInfo("en-US")) + "," + Convert.ToString(_x, CultureInfo.GetCultureInfo("en-US"));
        }

    }
}
