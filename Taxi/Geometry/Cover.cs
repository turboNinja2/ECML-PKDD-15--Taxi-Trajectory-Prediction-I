using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Taxi
{
    /// <summary>
    /// Represents a cover of the map.
    /// Basically, balls centers and radiuses.
    /// Balls are named after there center and radiuses.
    /// </summary>
    public class Cover
    {
        private List<Ball> _balls = new List<Ball>();
        private List<String> _names = new List<string>();

        /// <summary>
        /// Accessor to the names of the balls in the cover.
        /// </summary>
        public List<string> Names
        {
            get { return _names; }
        }

        /// <summary>
        /// Builds a cover from a .csv file.
        /// </summary>
        /// <param name="csvFilePath">.csv file containing the cover. Must contain a header.</param>
        public Cover(string csvFilePath)
        {
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string line = "";
                reader.ReadLine(); // header
                while ((line = reader.ReadLine()) != null)
                {
                    string[] res = line.Split(',');
                    double x = Convert.ToDouble(res[0], CultureInfo.GetCultureInfo("en-US"));
                    double y = Convert.ToDouble(res[1], CultureInfo.GetCultureInfo("en-US"));
                    double radius = Convert.ToDouble(res[2], CultureInfo.GetCultureInfo("en-US"));

                    Ball current = new Ball(x, y, radius);
                    _balls.Add(current);
                    _names.Add(line.Replace(',', '_'));
                }
            }
        }

        /// <summary>
        /// Returns the names of the balls containing the specified polyline
        /// </summary>
        /// <param name="polyline">The polyline to study</param>
        /// <returns>The names of the balls crossing the polyline</returns>
        public List<string> WhoContains(Polyline polyline)
        {
            bool[] res = new bool[_balls.Count];
            Parallel.For(0, _balls.Count, i => { res[i] = polyline.Crosses(_balls[i]); });

            List<string> resStr = new List<string>();
            for (int i = 0; i < _balls.Count; i++)
                if (res[i])
                    resStr.Add(_names[i]);

            return resStr;
        }
    }
}
