using System.Collections.Generic;

namespace Taxi
{
    public static class FeatureInteractions
    {
        public static List<string> CartesianProduct(this List<string> input, string keyword)
        {
            List<string> res = new List<string>();

            if (keyword == "method1")
                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        res.Add(input[i] + "_" + input[j]);

            if (keyword == "method2")
                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        if (!input[j].StartsWith("TAXI_ID") && !input[i].StartsWith("TAXI_ID"))
                            res.Add(input[i] + "_" + input[j]);

            if (keyword == "method3")
                for (int i = 0; i < input.Count; i++)
                    if (input[i].StartsWith("-8"))
                        for (int j = i; j < input.Count; j++)
                            if (!input[j].StartsWith("TAXI_ID"))
                                res.Add(input[i] + "_" + input[j]);

            if (keyword == "method4")
                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        if (!input[j].StartsWith("TAXI_ID") && !input[i].StartsWith("TAXI_ID") &&
                            !input[i].StartsWith("CALL_TYPE") && !input[j].StartsWith("CALL_TYPE"))
                            res.Add(input[i] + "_" + input[j]);

            if (keyword == "method5")
                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        if (!input[j].StartsWith("TAXI_ID") && !input[i].StartsWith("TAXI_ID") &&
                            !input[i].StartsWith("CALL_TYPE") && !input[j].StartsWith("CALL_TYPE") &&
                            !input[j].StartsWith("DIR"))
                            res.Add(input[i] + "_" + input[j]);

            if (keyword == "method6")
                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        if (!input[j].StartsWith("TAXI_ID") && !input[i].StartsWith("TAXI_ID") &&
                            !input[i].StartsWith("CALL_TYPE") && !input[j].StartsWith("CALL_TYPE") &&
                            !input[i].StartsWith("TIMESTAMP") && !input[j].StartsWith("TIMESTAMP"))
                            res.Add(input[i] + "_" + input[j]);


            if (keyword == "method7")
            {
                string factor = "";
                for (int i = 0; i < input.Count; i++)
                    if (input[i].StartsWith("DIR"))
                        factor = input[i];

                for (int i = 0; i < input.Count; i++)
                    for (int j = i; j < input.Count; j++)
                        if (!input[j].StartsWith("TAXI_ID") && !input[i].StartsWith("TAXI_ID") &&
                            !input[i].StartsWith("CALL_TYPE") && !input[j].StartsWith("CALL_TYPE") &&
                            !input[i].StartsWith("TIMESTAMP") && !input[j].StartsWith("TIMESTAMP"))
                            res.Add(input[i] + "_" + input[j] + "_" + factor);


            }


            return res;
        }

    }
}
