using System.Collections.Generic;

namespace Taxi
{
    public static class Extension
    {
        public static void TryAdd(this Dictionary<string, StreamingCloud> vector, string key, WeightedPoint weightedPoint)
        {
            if (vector.ContainsKey(key)) { vector[key].Add(weightedPoint); }
            else
            {
                StreamingCloud sc = new StreamingCloud();
                sc.Add(weightedPoint);
                vector.Add(key, sc);
            }
        }
    }
}
