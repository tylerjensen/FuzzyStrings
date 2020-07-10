/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Derived from http://www.codeguru.com/vb/gen/vb_misc/algorithms/article.php/c13137__1/Fuzzy-Matching-Demo-in-Access.htm
 * and from http://www.berghel.net/publications/asm/asm.php 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuoVia.FuzzyStrings
{
	public static class LevenshteinDistanceExtensions
	{
		/// <summary>
		/// Levenshtein Distance algorithm with transposition. <br />
		/// A value of 1 or 2 is okay, 3 is iffy and greater than 4 is a poor match
		/// </summary>
		/// <param name="input"></param>
		/// <param name="comparedTo"></param>
		/// <param name="caseSensitive"></param>
		/// <returns></returns>
		public static int LevenshteinDistance(this string input, string comparedTo, bool caseSensitive = false)
		{
			if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(comparedTo)) return -1;
			if (!caseSensitive)
			{
				input = input.ToLower();
				comparedTo = comparedTo.ToLower();
			}

			int[,] matrix = new int[input.Length + 1, comparedTo.Length + 1];

			//initialize
			for (int i = 0; i <= matrix.GetUpperBound(0); i++) matrix[i, 0] = i;
			for (int i = 0; i <= matrix.GetUpperBound(1); i++) matrix[0, i] = i;

			//analyze
			for (int i = 1; i <= matrix.GetUpperBound(0); i++)
			{
				var si = input[i - 1];
				for (int j = 1; j <= matrix.GetUpperBound(1); j++)
				{
					var tj = comparedTo[j - 1];
					int cost = (si == tj) ? 0 : 1;

					var above = matrix[i - 1, j];
					var left = matrix[i, j - 1];
					var diag = matrix[i - 1, j - 1];
					var cell = FindMinimum(above + 1, left + 1, diag + cost);

					//transposition
					if (i > 1 && j > 1)
					{
						var trans = matrix[i - 2, j - 2] + 1;
						if (input[i - 2] != comparedTo[j - 1]) trans++;
						if (input[i - 1] != comparedTo[j - 2]) trans++;
						if (cell > trans) cell = trans;
					}
					matrix[i, j] = cell;
				}
			}
			return matrix[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
		}

		private static int FindMinimum(params int[] p)
		{
			if (null == p) return int.MinValue;
			int min = int.MaxValue;
			for (int i = 0; i < p.Length; i++)
			{
				if (min > p[i]) min = p[i];
			}
			return min;
		}
	}
}
