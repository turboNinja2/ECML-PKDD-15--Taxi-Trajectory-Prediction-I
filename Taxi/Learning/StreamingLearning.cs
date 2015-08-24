using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Taxi
{
    /// <summary>
    /// Learning methods.
    /// </summary>
    public class StreamingLearning
    {
        /// <summary>
        /// The distance function.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between p1 and p2</returns>
        public delegate double DistanceFunction(WeightedPoint p1, WeightedPoint p2);

        private Dictionary<string, StreamingCloud> _vector = new Dictionary<string, StreamingCloud>();
        private List<Dictionary<string, StreamingCloud>> _vectorsCV = new List<Dictionary<string, StreamingCloud>>();

        /// <summary>
        /// Learns over each fold.
        /// </summary>
        /// <param name="filePath">The training file path (after feature extraction).</param>
        /// <param name="nFolds">The number of folds.</param>
        /// <param name="learningParameters">The learning parameters.</param>
        public void CrossLearning(string filePath, int nFolds, LearningParameters learningParameters)
        {
            _vectorsCV = new List<Dictionary<string, StreamingCloud>>(nFolds);
            for (int i = 0; i < nFolds; i++)
                _vectorsCV.Add(new Dictionary<string, StreamingCloud>());

            int currentLine = 0;
            foreach (Tuple<WeightedPoint, List<string>> line in YieldLines(filePath, true, learningParameters))
            {
                currentLine++;
                for (int i = 0; i < nFolds; i++)
                    if (currentLine % nFolds != i) // do not learn over the i-th field
                        foreach (string predictor in line.Item2)
                            _vectorsCV[i].TryAdd(predictor, line.Item1);
            }
        }

        /// <summary>
        /// Evaluates the cross validation error for each fold.
        /// </summary>
        /// <param name="filePath">The training file path (after feature extraction).</param>
        /// <param name="learningParameters">The learning parameters.</param>
        /// <param name="nFolds">The number of folds.</param>
        /// <param name="distance">The distance function to evaluate the scores.</param>
        /// <returns>An array, each element containing the error over the fold.</returns>
        public double[] CrossValidationScore(string filePath, LearningParameters learningParameters, int nFolds, DistanceFunction distance)
        {
            double[] perFoldError = new double[nFolds];
            int currentLine = 0;
            foreach (Tuple<WeightedPoint, List<string>> line in YieldLines(filePath, true, learningParameters))
            {
                currentLine++;
                for (int i = 0; i < nFolds; i++)
                    if (currentLine % nFolds == i) // predict over the i-th field
                        perFoldError[i] += distance(line.Item1, PredictFromLine(_vectorsCV[i], line.Item2, learningParameters));

            }
            for (int i = 0; i < nFolds; i++)
                perFoldError[i] = perFoldError[i] / currentLine * 5;
            return perFoldError;
        }

        /// <summary>
        /// Trains the model.
        /// </summary>
        /// <param name="filePath">The training file path (after feature extraction).</param>
        /// <param name="learningParameters">The learning parameters.</param>
        public void Train(string filePath, LearningParameters learningParameters)
        {
            foreach (Tuple<WeightedPoint, List<string>> line in YieldLines(filePath, true, learningParameters))
                foreach (string predictor in line.Item2)
                    _vector.TryAdd(predictor, line.Item1);

            string[] learned = _vector.Select(kvp => kvp.Key + "," + kvp.Value.ToString()).ToArray();

            File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Learned.txt", learned);
        }

        /// <summary>
        /// Trains the model and generate the predictions.
        /// </summary>
        /// <param name="trainFilePath">The training file path (after feature extraction).</param>
        /// <param name="testFilePath">The testing file path (after feature extraction).</param>
        /// <param name="outFilePath">The predicted values file path.</param>
        /// <param name="sampleSubmission">The sample submission file path (as provided by Kaggle).</param>
        /// <param name="learningParameters">The learning parameters.</param>
        public void TrainPredictAndWrite(string trainFilePath, string testFilePath, string outFilePath, string sampleSubmission,
            LearningParameters learningParameters)
        {
            Train(trainFilePath, learningParameters);
            List<WeightedPoint> predicted = Predict(testFilePath, learningParameters);

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

        private List<WeightedPoint> Predict(string filePath, LearningParameters learningParameters)
        {
            List<WeightedPoint> res = new List<WeightedPoint>();
            foreach (Tuple<WeightedPoint, List<string>> line in YieldLines(filePath, false, learningParameters))
                res.Add(PredictFromLine(_vector, line.Item2, learningParameters));
            return res;
        }

        private WeightedPoint PredictFromLine(Dictionary<string, StreamingCloud> clouds,
            List<string> predictors, LearningParameters predictionParameters)
        {
            List<WeightedPoint> bvs = new List<WeightedPoint>();
            foreach (string predictor in predictors)
                if (clouds.ContainsKey(predictor) && clouds[predictor].Size > predictionParameters.MinOccurences && clouds[predictor].Size < predictionParameters.MaxOccurences)
                {
                    StreamingCloud cloud = clouds[predictor];
                    WeightedPoint wp = new WeightedPoint(cloud.Barycenter);
                    wp.Weight =
                        Math.Pow(cloud.Size, predictionParameters.SizeExponent) / Math.Pow(cloud.Dispersion, predictionParameters.DispersionExponent);
                    bvs.Add(wp);
                }
            if (bvs.Count == 0) // could not find any predictor respecting the learning conditions
                return new WeightedPoint(-8.611317, 41.146504, 1);

            return WeightedPoint.Barycenter(bvs);
        }

        /// <summary>
        /// Yields the lines from a file as predictors and final point (if train set)
        /// </summary>
        /// <param name="filePath">The training file path (after feature extraction).</param>
        /// <param name="train">Set to true for train files, to return the final points as labels.</param>
        /// <param name="learningParameters">The learning parameters.</param>
        /// <returns></returns>
        private IEnumerable<Tuple<WeightedPoint, List<string>>> YieldLines(string filePath, bool train, LearningParameters learningParameters)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = ""; //No header in feature files
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

        #endregion
    }
}
