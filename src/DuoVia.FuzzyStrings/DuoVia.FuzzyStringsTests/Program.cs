using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuoVia.FuzzyStrings;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace fuzzytest
{
    class Program
    {
        static void Main(string[] args)
        {
            NameMatching();
            AddressMatching();
            Console.ReadLine();
        }

        private static void NameMatching()
        {
            //name matching
            string input = "Jensn";
            string[] surnames = new string[] { 
                "Adams",
                "Benson",
                "Geralds",
                "Johannson",
                "Johnson",
                "Jensen",
                "Jordon",
                "Madsen",
                "Stratford",
                "Wilkins"
                };

            Console.WriteLine("Dice Coefficient for Jensn:");
            foreach (var name in surnames)
            {
                double dice = input.DiceCoefficient(name);
                Console.WriteLine("\t{0} against {1}", dice.ToString("###,###.00000"), name);
            }

            Console.WriteLine();
            Console.WriteLine("Levenshtein Edit Distance for Jensn:");
            foreach (var name in surnames)
            {
                int leven = input.LevenshteinDistance(name);
                Console.WriteLine("\t{0} against {1}", leven, name);
            }

            Console.WriteLine();
            Console.WriteLine("Longest Common Subsequence for Jensn:");
            foreach (var name in surnames)
            {
                var lcs = input.LongestCommonSubsequence(name);
                Console.WriteLine("\t{0}, {1} against {2}", lcs.Item2.ToString("###,###.00000"), lcs.Item1, name);
            }

            Console.WriteLine();
            string mp = input.ToDoubleMetaphone();
            Console.WriteLine("Double Metaphone for Jensn: {0}", mp);
            foreach (var name in surnames)
            {
                string nameMp = name.ToDoubleMetaphone();
                Console.WriteLine("\t{0} metaphone for {1}", nameMp, name);
            }

            Console.WriteLine();
            Console.WriteLine("FuzzyEquals and FuzzyMatch for Jensn: {0}", mp);
            foreach (var name in surnames)
            {
                bool isEqual = input.FuzzyEquals(name);
                double coefficient = input.FuzzyMatch(name);
                Console.WriteLine("\tFuzzyEquals is {0}, FuzzyMatch {1} against {2}", isEqual, coefficient, name);
            }
        }

        private static void AddressMatching()
        {
            //name matching
            string input = "2130 South Fort Union Blvd.";
            string[] surnames = new string[] { 
                "2689 East Milkin Ave.",
                "85 Morrison",
                "2350 North Main",
                "567 West Center Street",
                "2130 Fort Union Boulevard",
                "2310 S. Ft. Union Blvd.",
                "98 West Fort Union",
                "Rural Route 2 Box 29",
                "PO Box 3487",
                "3 Harvard Square"
                };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Dice Coefficient for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                double dice = input.DiceCoefficient(name);
                Console.WriteLine("\t{0} against {1}", dice.ToString("###,###.00000"), name);
            }

            Console.WriteLine();
            Console.WriteLine("Levenshtein Edit Distance for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                int leven = input.LevenshteinDistance(name);
                Console.WriteLine("\t{0} against {1}", leven, name);
            }

            Console.WriteLine();
            Console.WriteLine("Longest Common Subsequence for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                var lcs = input.LongestCommonSubsequence(name);
                Console.WriteLine("\t{0}, {1} against {2}", lcs.Item2.ToString("###,###.00000"), lcs.Item1, name);
            }

            Console.WriteLine();
            string mp = input.ToDoubleMetaphone();
            Console.WriteLine("Double Metaphone for 2130 South Fort Union Blvd.: {0}", mp);
            foreach (var name in surnames)
            {
                string nameMp = name.ToDoubleMetaphone();
                Console.WriteLine("\t{0} metaphone for {1}", nameMp, name);
            }

            Console.WriteLine();
            Console.WriteLine("FuzzyEquals and FuzzyMatch for 2130 South Fort Union Blvd.: {0}", mp);
            foreach (var name in surnames)
            {
                bool isEqual = input.FuzzyEquals(name);
                double coefficient = input.FuzzyMatch(name);
                Console.WriteLine("\tFuzzyEquals is {0}, FuzzyMatch {1} against {2}", isEqual, coefficient, name);
            }
        }
    }
}
