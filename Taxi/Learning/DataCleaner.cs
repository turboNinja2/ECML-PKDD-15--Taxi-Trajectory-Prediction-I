using System;
using System.IO;

namespace Taxi
{
    public static class DataCleaner
    {
        /// <summary>
        /// Drops some lines if :
        /// - The highest speed is too high
        /// - There are missing data
        /// - Holidays
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Tuple<int, int> CleanMissingData(string filePath)
        {
            int totalLines = 0,
                finalLines = 0;
            using (StreamReader reader = new StreamReader(filePath))
            using (StreamWriter writer = new StreamWriter(filePath.Split('.')[0] + "_clean." + filePath.Split('.')[1]))
            {
                string line = "";
                writer.WriteLine(reader.ReadLine()); // header
                while ((line = reader.ReadLine()) != null)
                {
                    totalLines++;
                    string[] arr = line.Split(',');
                    if (arr[7] == "\"True\"") // drop missing data
                        continue;
                    if (arr[6] != "\"A\"") // drop holidays (do not appear in test data)
                        continue;
                    if (arr[8].Length < 8) //  drops too short trajectories
                        continue;

                    Polyline dat = new Polyline(line);
                    if (dat.MaxSpeed() > 0.02) // drops suspicious polylines
                        continue;
                    finalLines++;

                    writer.WriteLine(line);
                }
            }
            return new Tuple<int, int>(totalLines, finalLines);
        }
    }
}
