using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Assignment5WordStats
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filepath = @"..\resources\practice.txt";
            string filepath_write = @"..\..\stats.txt";
            
            using (StreamReader streamreader = new StreamReader(filepath))
            {
                string fileText = streamreader.ReadToEnd();
                // Count number of words in the file

                var count = fileText.Split(' ').Count();

                // Get the average length
                var averageLen = fileText.Split(' ').Average(n => n.Length);

                // Dictionary storing <word, wordLength>
                Dictionary<string, int> wordAndLength = new Dictionary<string, int>();

                // Remove the special characters from the file, in order to get letter pairs
                string longText = Extensions.RemoveSpecialCharacters(fileText);
                var wordHold = fileText.Split(new char[] { ' ', '-', '.', '\\', '/', ':', ',', '(', ')', '"', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < wordHold.Length; x++)
                {
                    if (!wordAndLength.ContainsKey(wordHold[x]))
                    {
                        wordAndLength.Add(wordHold[x], wordHold[x].Length);
                    }
                }
                // sort wordAndLength dictionary by length
                var sortedLengthDict = from value in wordAndLength orderby value.Value descending select value;

                List<string> distinctPairs = new List<string>();
                for (char c = 'a'; c <= 'z'; c++)
                {
                    for (char j = 'a'; j <= 'z'; j++)
                    {
                        char[] tmpArr = { c, j };
                        string search = new string(tmpArr);
                        distinctPairs.Add(search);
                    }
                }
                string[] matchArray = distinctPairs.ToArray();
                Dictionary<string, double> pairsAndOccurences = new Dictionary<string, double>();

                for (int i = 0; i < distinctPairs.Count; i++)
                {
                    int tmptest = Regex.Matches(longText, matchArray[i]).Cast<Match>().Count();
                    pairsAndOccurences.Add(matchArray[i], tmptest);
                }

                pairsAndOccurences = pairsAndOccurences.Where(p => p.Value != 0).ToDictionary(p => p.Key, p => p.Value);
                List<int> foo = new List<int>();
                foreach (var item in wordHold)
                {
                    foo.Add(item.Length);
                }
                using (StreamWriter writer = new StreamWriter(filepath_write))
                {
                    Console.SetOut(writer);
                    Console.WriteLine("The number of words in this file is: {0}", wordHold.Count());
                    printLogicPairs.PairHistogram(pairsAndOccurences);
                    printLogicLen.GoHistogram(foo);

                    Console.WriteLine("The average length is {0}", Math.Round(averageLen, 2));

                    
                }
                
            }
        }

       
    }
}
