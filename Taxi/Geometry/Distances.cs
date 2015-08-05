using System;

namespace Taxi
{
    public static class Distances
    {
        private const double REarth = 6371f;
        private const double _pi180 = Math.PI / 180f;

        /// <summary>
        /// Returns the Haversine distance between two weighted points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double Haversine(WeightedPoint p1, WeightedPoint p2)
        {
            double lat = Math.Abs(p1.X - p2.X) * _pi180,
                lon = Math.Abs(p1.Y - p2.Y) * _pi180;

            double lat1 = p1.X * _pi180,
                lat2 = p2.X * _pi180;

            double a = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(lon / 2) * Math.Sin(lon / 2);

            double Hdist = REarth * 2f * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Hdist;
        }

        public static double Euclide(WeightedPoint bv1, WeightedPoint bv2)
        {
            return Math.Sqrt((bv1.X - bv2.X) * (bv1.X - bv2.X) + (bv2.Y - bv1.Y) * (bv2.Y - bv1.Y));
        }
    }
}
