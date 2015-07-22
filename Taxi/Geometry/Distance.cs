using System;

namespace Taxi
{
    public static class Distances
    {
        private const double REarth = 6371f;
        private const double _pi180 = Math.PI / 180f;

        /// <summary>
        /// Returns the Haversine distance between two weighted points.
        /// Matches the following R script :
        /// 
        /// HaversineDistance=function(lat1,lon1,lat2,lon2)
        ///     REarth<-6371
        ///     lat<-abs(lat1-lat2)*pi/180
        ///     lon<-abs(lon1-lon2)*pi/180
        ///     lat1<-lat1*pi/180
        ///     lat2<-lat2*pi/180
        ///     a<-sin(lat/2)*sin(lat/2)+cos(lat1)*cos(lat2)*sin(lon/2)*sin(lon/2)
        ///     d<-2*atan2(sqrt(a),sqrt(1-a))
        ///     d<-REarth*d
        ///     return(d)
        ///     
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

        public static double Euclide(WeightedPoint p1, WeightedPoint p2)
        {
            return Math.Pow(Math.Log(1 + p1.X) - Math.Log(1 + p2.X),2);
        }


        public static double Euclide(double t1, double t2)
        {
            return (t1 - t2) * (t1 - t2);
        }
    }
}
