using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Taxi
{
    public class StreamingLearning
    {
        public delegate double DistanceFunction(WeightedPoint p1, WeightedPoint p2);

        Dictionary<string, StreamingCloud> _vector = new Dictionary<string, StreamingCloud>();
        List<Dictionary<string, StreamingCloud>> _vectorsCV = new List<Dictionary<string, StreamingCloud>>();

        /// <summary>
        /// Yields the lines from a file as predictors and final point (if train set)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="train"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private IEnumerable<Tuple<WeightedPoint, List<string>>> yieldLines(string filePath, bool train, LearningParameters learningParameters)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = ""; //no header in feature files ;)
                while ((line = reader.ReadLine()) != null)
                {
                    List<string> arrayLine = line.Split(',').ToList();
                    WeightedPoint bv = new WeightedPoint(0, 0, 0);

                    if (train)
                    {
                        string[] bidimVector = arrayLine[0].Split('_');
                        bv = new WeightedPoint(Convert.ToDouble(bidimVector[0], CultureInfo.GetCultureInfo("en-US")),
                            Convert.ToDouble(bidimVector[1], CultureInfo.GetCultureInfo("en-US")), 1);
                        arrayLine.RemoveAt(0);
                    }

                    arrayLine = arrayLine.CartesianProduct(learningParameters.Keyword);

                    yield return new Tuple<WeightedPoint, List<string>>(bv, arrayLine);
                }
            }
        }

        #region Cross validation

        /// <summary>
        /// Learns over the number of folds 
        /// (If you don't have much RAM, note that a number of dictionnary equals to the number of folds will be created)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nFolds"></param>
        public void CrossLearning(string filePath, int nFolds, LearningParameters learningParameters)
        {
            // Initialization
            _vectorsCV = new List<Dictionary<string, StreamingCloud>>(nFolds);
            for (int i = 0; i < nFolds; i++)
                _vectorsCV.Add(new Dictionary<string, StreamingCloud>());

            // Learning
            int currentLine = 0;
            foreach (Tuple<WeightedPoint, List<string>> line in yieldLines(filePath, true, learningParameters))
            {
                currentLine++;
                for (int i = 0; i < nFolds; i++)
                    if (currentLine % nFolds != i) // do not learn over the i-th field
                        foreach (string predictor in line.Item2)
                            _vectorsCV[i].TryAdd(predictor, line.Item1);
            }
        }

        /// <summary>
        /// Given predictions parameters, return the error per fold
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="predictionsParameters"></param>
        /// <param name="nFolds"></param>
        /// <returns></returns>
        public double[] CVScore(string filePath, LearningParameters predictionsParameters, int nFolds, LearningParameters learningParameters, DistanceFunction distance)
        {
            double[] perFoldError = new double[nFolds];
            int currentLine = 0;
            foreach (Tuple<WeightedPoint, List<string>> line in yieldLines(filePath, true, learningParameters))
            {
                currentLine++;
                for (int i = 0; i < nFolds; i++)
                    if (currentLine % nFolds == i) // predict over the i-th field
                        perFoldError[i] += distance(line.Item1, predictPositionFromLine(_vectorsCV[i], line.Item2, predictionsParameters));

            }
            for (int i = 0; i < nFolds; i++)
                perFoldError[i] = perFoldError[i] / currentLine * 5;
            return perFoldError;
        }


        #endregion

        #region Global learning

        /// <summary>
        /// Learns the barycenters and the dispersion of the clouds from the data.
        /// </summary>
        /// <param name="filePath">A file generated thanks to feature creation</param>
        public void Learn(string filePath, LearningParameters learningParameters)
        {
            foreach (Tuple<WeightedPoint, List<string>> line in yieldLines(filePath, true, learningParameters))
                foreach (string predictor in line.Item2)
                    _vector.TryAdd(predictor, line.Item1);

            string[] learned = _vector.Select(kvp => kvp.Key + "," + kvp.Value.ToString()).ToArray();

            File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Learned.txt", learned);

        }

        public List<WeightedPoint> Predict(string filePath, string outFilePath, LearningParameters learningParameters)
        {
            List<WeightedPoint> res = new List<WeightedPoint>();
            foreach (Tuple<WeightedPoint, List<string>> line in yieldLines(filePath, false, learningParameters))
                res.Add(predictPositionFromLine(_vector, line.Item2, learningParameters));
            return res;
        }

        #endregion

        public void TrainPredictAndWrite(string trainFilePath, string testFilePath, string outFilePath, string sampleSubmission,
            LearningParameters learningParameters)
        {
            Learn(trainFilePath, learningParameters);
            List<WeightedPoint> predicted = Predict(testFilePath, outFilePath, learningParameters);

            using (StreamReader reader = new StreamReader(sampleSubmission))
            using (StreamWriter writer = new StreamWriter(outFilePath))
            {
                string line = reader.ReadLine();
                writer.Write(line); // copy header

                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string idLine = line.Split(',')[0];
                    string res = idLine + ',' + predicted[i].ToString();
                    i++;
                    writer.Write(Environment.NewLine + res);
                }
            }
        }

        #region Privates

        private WeightedPoint predictPositionFromLine(Dictionary<string, StreamingCloud> clouds,
            List<string> predictors, LearningParameters lp)
        {
            List<WeightedPoint> bvs = new List<WeightedPoint>();
            foreach (string predictor in predictors)
                if (clouds.ContainsKey(predictor) && clouds[predictor].Size > lp.MinOccurences && clouds[predictor].Size < lp.MaxOccurences)
                {
                    StreamingCloud cloud = clouds[predictor];
                    WeightedPoint wp = new WeightedPoint(cloud.Barycenter);
                    wp.Weight =
                        Math.Pow(cloud.Size, lp.SizeExponent) / Math.Pow(cloud.Dispersion, lp.DispersionExponent);
                    bvs.Add(wp);
                }
            if (bvs.Count == 0) // could not find any predictor respecting the learning conditions
                return new WeightedPoint(-8.611317, 41.146504, 1);

            return WeightedPoint.Barycenter(bvs);
        }

        #endregion
    }
}
