using System;
using System.Globalization;

namespace Taxi
{
    public class LearningParameters
    {
        public int MinOccurences;
        public int MaxOccurences;
        public double DispersionExponent;
        public double SizeExponent;
        public string Keyword;

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
