using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Taxi
{
    public class StreamingCloud
    {
        int _nbPoints = 0;
        WeightedPoint _barycenter = null;
        double _dispersion = -1;

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
                _dispersion = (_dispersion * (_nbPoints - 1) + WeightedPoint.EuclideDistance(_barycenter, point)) / _nbPoints;
            }
        }

        public WeightedPoint Barycenter
        {
            get
            {
                return _barycenter;
            }
        }

        public double Dispersion
        {
            get
            {
                return _dispersion;
            }
        }

        public int Size
        {
            get
            {
                return _nbPoints;
            }
        }

        public override string ToString()
        {
            return _barycenter.ToString() + "," + 
                Convert.ToString(_dispersion, CultureInfo.GetCultureInfo("en-US")) + "," + 
                Convert.ToString(_nbPoints,  CultureInfo.GetCultureInfo("en-US"));
        }




    }
}
