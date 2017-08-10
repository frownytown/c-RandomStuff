using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5WordStats
{
    public static class printLogicLen
    {
        public static SortedDictionary<int, int> GoHistogram(this IEnumerable<int> nums)
        {
            // contains the number of occurences of each size
            var histoDict = new SortedDictionary<int, double>();
            var dict = new SortedDictionary<int, int>();
            foreach(var n in nums)
            {
                if (!dict.ContainsKey(n))
                {
                    dict[n] = 1;
                }
                else
                {
                    dict[n] += 1;
                }
            }
            float valuesSum = dict.Values.Sum();
            Console.WriteLine("\t Word Length Chart");
            Console.WriteLine("----------------------------------");
            foreach (var x in dict)
            {
                double histoFreq = (x.Value / valuesSum);
                histoDict.Add(x.Key, histoFreq);
                double preFrequency = (x.Value / valuesSum) * 100;
                double frequency = Math.Round(preFrequency, 2);
                Console.WriteLine("The length of [{0}] occurs {1} times, or % {2} of the time ", x.Key, x.Value, frequency);
            }
            
            
            
            return dict;
        }
    }
    public static class printLogicPairs
    {
        public static SortedDictionary<string, double> PairHistogram(this Dictionary<string, double> pairs)
        {
            // check the frequency of occurences for the pairs of letters
            var pairDict = new SortedDictionary<string, double>();
          
            double sumPairs = pairs.Values.Sum();
            Console.WriteLine("\t Word Occurence Chart");
            Console.WriteLine("----------------------------------");
            foreach (var j in pairs)
            {
                double prePair = (j.Value / sumPairs) * 100;
                double pairFreq = Math.Round(prePair, 2);
                pairDict.Add(j.Key, pairFreq);
                Console.WriteLine("Letters [{0}] occur {1} times, or {2} % of the time ", j.Key, j.Value, pairFreq);
            }
            var mostPair = pairDict.OrderByDescending(kvp => kvp.Value).First().Key;
            
            Console.WriteLine("The highest percentage pair is: [{0}] percentage: % {1} of the time ", mostPair, pairDict[mostPair]);
            return pairDict;
            
        }
    }
}

