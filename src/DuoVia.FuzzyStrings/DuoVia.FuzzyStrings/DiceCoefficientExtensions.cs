/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Derived from http://www.codeguru.com/vb/gen/vb_misc/algorithms/article.php/c13137__1/Fuzzy-Matching-Demo-in-Access.htm
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuoVia.FuzzyStrings
{
	public static class DiceCoefficientExtensions
	{
		/// <summary>
		/// Dice Coefficient based on bigrams. <br />
		/// A good value would be 0.33 or above, a value under 0.2 is not a good match, from 0.2 to 0.33 is iffy.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="comparedTo"></param>
		/// <returns></returns>
		public static double DiceCoefficient(this string input, string comparedTo)
		{
			if (input == comparedTo)
				return 1.0d;

			if (input.Length < 2 || comparedTo.Length < 2)
				return 0.0d;

			var biGrams = input.ToBiGrams(false);
			var compareToBiGrams = comparedTo.ToBiGrams(false);
			return DiceCoefficient(biGrams, compareToBiGrams);
		}

		/// <summary>
		/// Dice Coefficient used to compare nGrams arrays produced in advance.
		/// </summary>
		/// <param name="nGrams"></param>
		/// <param name="compareToNGrams"></param>
		/// <returns></returns>
		public static double DiceCoefficient(this string[] nGrams, string[] compareToNGrams)
		{
			var nGramMap = new Dictionary<string, int>(nGrams.Length);
			var compareToNGramMap = new Dictionary<string, int>(compareToNGrams.Length);
			var nGramSet = new HashSet<string>();
			var compareToNGramSet = new HashSet<string>();
			foreach (var nGram in nGrams)
			{
				if (nGramSet.Add(nGram))
					nGramMap[nGram] = 1;
				else
					nGramMap[nGram]++;
			}
			foreach (var nGram in compareToNGrams)
			{
				if (compareToNGramSet.Add(nGram))
					compareToNGramMap[nGram] = 1;
				else
					compareToNGramMap[nGram]++;
			}
			nGramSet.IntersectWith(compareToNGramSet);
			if (nGramSet.Count == 0)
				return 0.0d;

			var matches = 0;
			foreach (var nGram in nGramSet)
				matches += Math.Min(nGramMap[nGram], compareToNGramMap[nGram]);
			
			double totalBigrams = nGrams.Length + compareToNGrams.Length;
			return (2 * matches) / totalBigrams;
		}

		public static string[] ToBiGrams(this string input, bool usePadding = true)
		{
			if (usePadding)
			{
				// nLength == 2
				//   from Jackson, return %j ja ac ck ks so on n#
				//   from Main, return %m ma ai in n#
				input = SinglePercent + input + SinglePound;
			}
			if (input.Length < 2)
				return new string[0];
			
			return ToNGrams(input, 2);
		}

		public static string[] ToTriGrams(this string input)
		{
			// nLength == 3
			//   from Jackson, return &&j &ja jac ack cks kso son on# n##
			//   from Main, return &&m &ma mai ain in# n##
			input = DoubleAmpersand + input + DoublePound;
			return ToNGrams(input, 3);
		}

		private static string[] ToNGrams(string input, int nLength)
		{
			int itemsCount = input.Length - 1;
			string[] ngrams = new string[input.Length - 1];
			for (int i = 0; i < itemsCount; i++)
				ngrams[i] = input.Substring(i, nLength);
			return ngrams;
		}

		private const string SinglePercent = "%";
		private const string SinglePound = "#";
		private const string DoubleAmpersand = "&&";
		private const string DoublePound = "##";
	}
}
