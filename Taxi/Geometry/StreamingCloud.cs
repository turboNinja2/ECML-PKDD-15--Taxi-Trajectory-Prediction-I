using System;
using System.Globalization;

namespace Taxi
{
    /// <summary>
    /// Represents a cloud of points. Note that the points themselves are not stored.
    /// </summary>
    public class StreamingCloud
    {
        private int _nbPoints = 0;
        private WeightedPoint _barycenter = null;
        private double _dispersion = -1;

        public WeightedPoint Barycenter
        {
            get { return _barycenter; }
        }

        public double Dispersion
        {
            get { return _dispersion; }
        }

        public int Size
        {
            get { return _nbPoints; }
        }

        public void Add(WeightedPoint point)
        {
            _nbPoints++;
            if (_barycenter == null)
            {
                _barycenter = new WeightedPoint(point.X, point.Y, 1);
                _dispersion = 0;
            }
            else
            {
                _barycenter = _barycenter.Multiply(_nbPoints - 1);
                _barycenter = _barycenter.Add(point);
                _barycenter = _barycenter.Divide(_nbPoints);
                _dispersion = (_dispersion * (_nbPoints - 1) + Distances.Euclide(_barycenter, point)) / _nbPoints;
            }
        }

        public override string ToString()
        {
            return _barycenter.ToString() + "," +
                Convert.ToString(_dispersion, CultureInfo.GetCultureInfo("en-US")) + "," +
                Convert.ToString(_nbPoints, CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
