using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Taxi
{
    public class Polyline
    {
        private double[] _polylineX;
        private double[] _polylineY;

        #region Constructors

        public Polyline()
        {

        }

        public Polyline(string line, double partToKeep = 1.1f)
        {
            StringSplitOptions sso = new StringSplitOptions();
            string[] arr = line.Split(new string[] { "[[" }, sso);
            string pl = arr[1];
            pl = pl.Replace("[", "");
            pl = pl.Replace("]", "");
            pl = pl.Replace("\"", "");
            List<string> elts = pl.Split(',').ToList();

            if (partToKeep < 1f)
            {
                int finalSize = (int)Math.Floor(elts.Count * partToKeep / 2 + 1) * 2;
                List<string> elts2 = new List<string>();
                for (int i = 0; i < finalSize; i++)
                    elts2.Add(elts[i]);
                elts = elts2;
            }

            _polylineX = new double[elts.Count / 2];
            _polylineY = new double[elts.Count / 2];

            for (int i = 0; i < _polylineX.Length; i++)
            {
                _polylineX[i] = Convert.ToDouble(elts[i * 2], CultureInfo.GetCultureInfo("en-US"));
                _polylineY[i] = Convert.ToDouble(elts[i * 2 + 1], CultureInfo.GetCultureInfo("en-US"));
            }
        }

        #endregion

        #region Accessors

        public int Length
        {
            get { return _polylineX.Length; }
        }

        public string GetDirection()
        {
            string res = "";
            res += (_polylineX.First() > _polylineX.Last()) ? "W" : "E";
            res += (_polylineY.First() > _polylineY.Last()) ? "N" : "S";
            return res;
        }

        #endregion

        public double MaxSpeed()
        {
            double maxSpeed = 0;
            for (int i = 1; i < _polylineX.Length; i++)
                maxSpeed = Math.Max(Math.Abs(_polylineX[i] - _polylineX[i - 1]) + Math.Abs(_polylineY[i] - _polylineY[i - 1]), maxSpeed);
            return maxSpeed;
        }

        /// <summary>
        /// True if the polyline goes through the ball.
        /// </summary>
        /// <param name="b">The ball</param>
        /// <returns></returns>
        public bool Crosses(Ball b)
        {
            bool res = false;
            for (int i = 0; i < _polylineX.Length; i++)
                res = res || ((_polylineX[i] - b.X) * (_polylineX[i] - b.X) + (_polylineY[i] - b.Y) * (_polylineY[i] - b.Y) < b.Radius * b.Radius);
            return res;
        }

        /// <summary>
        /// Returns (as a string) the last point of a polyline
        /// </summary>
        /// <returns></returns>
        public string LastElementString()
        {
            return (Convert.ToString(_polylineX.Last(), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(_polylineY.Last(), CultureInfo.GetCultureInfo("en-US")));
        }


        public string LastElementString(int precision)
        {
            // * 2 starts from new version (improve resolution of the last point)
            return (Convert.ToString(Math.Round(_polylineX.Last() * 2, precision), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(Math.Round(_polylineY.Last() * 2, precision), CultureInfo.GetCultureInfo("en-US")));
        }
        public string FirstElementString(int precision)
        {
            return (Convert.ToString(Math.Round(_polylineX.First(), precision), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(Math.Round(_polylineY.First(), precision), CultureInfo.GetCultureInfo("en-US")));
        }

        public double Speed()
        {
            return Distances.Haversine(
                new WeightedPoint(_polylineX.First(), _polylineY.First(), 1),
                new WeightedPoint(_polylineX.Last(), _polylineY.Last(), 1)) / Length;
        }

    }
}
