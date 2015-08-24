using System;
using System.Collections.Generic;
using System.IO;

namespace Taxi
{
    /// <summary>
    /// Turns the data into a "libsvm" representation.
    /// </summary>
    public static class FeatureWriter
    {
        /// <summary>
        /// Given a cover of the map, writes a "libsvm" file associated to the cover. It also writes other features : last recorded element...
        /// </summary>
        /// <param name="ballSet">The cover of the map</param>
        /// <param name="ballsName">The name of the cover</param>
        /// <param name="filePath">The (cleaned) train file path</param>
        /// <param name="train">Should the file include the label</param>
        /// <param name="finalPosition">Shall we work with the final position or the total time ?</param>
        public static void FromNeighborhood(Cover ballSet, string ballsName, string filePath, bool train, bool finalPosition = true)
        {
            Random rnd = new Random(1);

            string comment = finalPosition ? "POS" : "TIME";

            using (StreamReader reader = new StreamReader(filePath)) // LRE is for the latest (19/5)
            using (StreamWriter writer = new StreamWriter(filePath.Split('.')[0] + "_" + ballsName + "_" + comment + "_SPE." + filePath.Split('.')[1]))
            {
                string line = "",
                    lastElement = "";
                string[] header = reader.ReadLine().Replace("\"", string.Empty).Split(',');
                while ((line = reader.ReadLine()) != null)
                {
                    Polyline pl = new Polyline(line);
                    lastElement = pl.LastElementString();
                    int totalLength = pl.Length;

                    line = line.Replace("\"", string.Empty);
                    string restOfLine = "";
                    string[] lineArray = line.Split(',');


                    if (train) // replaces the polyline by its first elements
                    {
                        pl = new Polyline(line, Math.Min(rnd.Next(10000), rnd.Next(10000)) * 1f/ 10000);
                        if (pl.Length == 0) continue;
                    }

                    List<string> res = ballSet.WhoContains(pl);
                    string last = String.Join(",", res);

                    string lastRecordedElement = pl.LastElementString(2); // gets the last position
                    last = "LRE_" + lastRecordedElement + ',' + last; // from version 1.2

                    string firstRecordedElement = pl.FirstElementString(2); // gets the last position
                    last = "FRE_" + firstRecordedElement + ',' + last; // from version 1.2

                    for (int i = 0; i < header.Length; i++)
                    {
                        if (header[i].Contains("TIMESTAMP"))
                        {
                            DateTime dt = JulianTimeStampToDateTime(Convert.ToInt32(lineArray[i]));
                            restOfLine += ',' + header[i] + "_" + dt.DayOfWeek + ',' + "HOUR_" + dt.Hour;
                            continue;
                        }

                        // drops
                        if (!(header[i].Contains("TRIP_ID") || header[i].Contains("POLYLINE") || header[i].Contains("DAY_TYPE") || header[i].Contains("MISSING_DATA")))
                            restOfLine += ',' + header[i] + '_' + (lineArray[i] == string.Empty ? "NA" : lineArray[i]);
                    }

                    restOfLine += ",DIR_" + pl.GetDirection(); // version 2
                    restOfLine += ",SPEED_" + Convert.ToString(Math.Floor(pl.Speed()/15*360)); // version 3

                    if (train)
                        if (finalPosition)
                            last = lastElement + ',' + last;
                        else
                            last = totalLength + "_0" + ',' + last;

                    writer.WriteLine(last + restOfLine);
                }
            }
        }

        /// <summary>
        /// Turns a Julian timespan to a DateTime
        /// </summary>
        /// <param name="jts">The Julian timespan representation</param>
        /// <returns>DateTime representation of the input</returns>
        public static DateTime JulianTimeStampToDateTime(int jts)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return epoch.AddSeconds(jts);
        }
    }
}
