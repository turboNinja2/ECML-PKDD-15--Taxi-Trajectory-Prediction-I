using System.Collections.Generic;

namespace Taxi
{
    /// <summary>
    /// Simple dictionary extension for Dictionary string StreamingCloud.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Extension of the add default method. If the dictionary does not contains the clound, a single point cloud is added 
        /// and the associated value is the point. If the dictionary contains the key, the values are added and the 
        /// key is unchanged.
        /// </summary>
        /// <param name="clouds">The clouds of points</param>
        /// <param name="key">The key of a cloud</param>
        /// <param name="weightedPoint">The point to add to the cloud</param>
        public static void TryAdd(this Dictionary<string, StreamingCloud> clouds, string key, WeightedPoint weightedPoint)
        {
            if (clouds.ContainsKey(key)) { clouds[key].Add(weightedPoint); }
            else
            {
                StreamingCloud sc = new StreamingCloud();
                sc.Add(weightedPoint);
                clouds.Add(key, sc);
            }
        }
    }
}
