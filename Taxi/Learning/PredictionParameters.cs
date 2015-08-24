using System;
using System.Globalization;

namespace Taxi
{
    /// <summary>
    /// A simple class to store learning parameters
    /// </summary>
    public class LearningParameters
    {
        /// <summary>
        /// The smallest number of occurences of a feature to use it.
        /// </summary>
        public int MinOccurences;

        /// <summary>
        /// The largest number of occurences of a feature to use it.
        /// </summary>
        public int MaxOccurences;

        /// <summary>
        /// The exponent weighting the dispersion of the cloud.
        /// </summary>
        public double DispersionExponent;

        /// <summary>
        /// The exponent weighting the size of the cloud.
        /// </summary>
        public double SizeExponent;

        /// <summary>
        /// The method used to generate combination of features.
        /// </summary>
        public string Keyword;
        
        /// <summary>
        /// Constructs the object from its string representation.
        /// </summary>
        /// <param name="learningString">"Keyword_MinOccurences_MaxOccurences_SizeExponent_DispersionExponent"</param>
        public LearningParameters(string learningString)
        {
            string[] parameters = learningString.Split('_');
            Keyword = parameters[0];
            MinOccurences = Convert.ToInt32(parameters[1], CultureInfo.GetCultureInfo("en-US"));
            MaxOccurences = Convert.ToInt32(parameters[2], CultureInfo.GetCultureInfo("en-US"));
            SizeExponent = Convert.ToDouble(parameters[3], CultureInfo.GetCultureInfo("en-US"));
            DispersionExponent = Convert.ToDouble(parameters[4], CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
