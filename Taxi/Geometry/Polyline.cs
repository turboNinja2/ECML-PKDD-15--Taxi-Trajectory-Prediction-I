using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Taxi
{
    /// <summary>
    /// Represents a polyline : \f$(x_1,y_1),(x_2,y_2),...,(x_n,y_n)\f$
    /// Various features are available.
    /// </summary>
    public class Polyline
    {
        private double[] _polylineX;
        private double[] _polylineY;

        #region Accessors

        /// <summary>
        /// Length of the polyline.
        /// </summary>
        public int Length
        {
            get { return _polylineX.Length; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Builds a polyline from a string. Optional : keep a subpart of the complete polyline.
        /// </summary>
        /// <param name="line">The input string, under the format [[x_1,y_1],...,[x_n,y_n]].</param>
        /// <param name="partToKeep">The percentage of the first part of the string to keep.</param>
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

        /// <summary>
        ///  Feature extraction. Direction of the polyline : N,S,E,W.
        /// </summary>
        /// <returns>The direction, as a string, of the polyline.</returns>
        public string GetDirection()
        {
            string res = "";
            res += (_polylineX.First() > _polylineX.Last()) ? "W" : "E";
            res += (_polylineY.First() > _polylineY.Last()) ? "N" : "S";
            return res;
        }

        /// <summary>
        /// Feature extraction.
        /// </summary>
        /// <returns>The maximum speed over the polyline.</returns>
        public double MaxSpeed()
        {
            double maxSpeed = 0;
            for (int i = 1; i < _polylineX.Length; i++)
                maxSpeed = Math.Max(Math.Abs(_polylineX[i] - _polylineX[i - 1]) + Math.Abs(_polylineY[i] - _polylineY[i - 1]), maxSpeed);
            return maxSpeed;
        }

        /// <summary>
        /// Feature extraction. True if the polyline goes through the ball.
        /// </summary>
        /// <param name="b">The ball.</param>
        /// <returns>True if the polyline crosses the ball.</returns>
        public bool Crosses(Ball b)
        {
            bool res = false;
            for (int i = 0; i < _polylineX.Length; i++)
                res = res || ((_polylineX[i] - b.X) * (_polylineX[i] - b.X) + (_polylineY[i] - b.Y) * (_polylineY[i] - b.Y) < b.Radius * b.Radius);
            return res;
        }

        /// <summary>
        /// Feature extraction. Returns (as a string) the last point of a polyline.
        /// </summary>
        /// <returns>The last point of a polyline</returns>
        public string LastElementString()
        {
            return (Convert.ToString(_polylineX.Last(), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(_polylineY.Last(), CultureInfo.GetCultureInfo("en-US")));
        }

        /// <summary>
        /// Feature extraction. Returns (as a string) the last point of a polyline.
        /// </summary>
        /// <returns>The (rounded) last point of a polyline.</returns>
        public string LastElementString(int precision)
        {
            return (Convert.ToString(Math.Round(_polylineX.Last() * 2, precision), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(Math.Round(_polylineY.Last() * 2, precision), CultureInfo.GetCultureInfo("en-US")));
        }

        /// <summary>
        /// Feature extraction. Returns (as a string) the first point of a polyline.
        /// </summary>
        /// <returns>The (rounded) first point of a polyline</returns>
        public string FirstElementString(int precision)
        {
            return (Convert.ToString(Math.Round(_polylineX.First(), precision), CultureInfo.GetCultureInfo("en-US")) + '_' +
                Convert.ToString(Math.Round(_polylineY.First(), precision), CultureInfo.GetCultureInfo("en-US")));
        }

        /// <summary>
        /// Feature extraction. Gets the average speed.
        /// </summary>
        /// <returns>The average speed of the trajectory.</returns>
        public double Speed()
        {
            return Distances.Haversine(
                new WeightedPoint(_polylineX.First(), _polylineY.First(), 1),
                new WeightedPoint(_polylineX.Last(), _polylineY.Last(), 1)) / Length;
        }
    }
}
