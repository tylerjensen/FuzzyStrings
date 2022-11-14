using System;
using Xunit;
using Xunit.Abstractions;
using DuoVia.FuzzyStrings;

namespace DuoVia.FuzzyStringsTests
{
    public class FuzzyTests
    {
        private readonly ITestOutputHelper output;
        public FuzzyTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void FuzzyMatchTests(string input, string match)
        {
            var result = input.FuzzyMatch(match);
            Assert.True(result > 0.0);
            output.WriteLine($"FuzzyMatch of \"{match}\" against \"{input}\" was {result}.");
        }

        [Theory]
        [InlineData("wwww", "w")]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        [InlineData("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "ee")]
        [InlineData("aaaaaaaaa", "aaaaaaaaa")]
        public void DiceCoefficientTests(string input, string match)
        {
            var result = input.DiceCoefficient(match);
            var reversedResult = match.DiceCoefficient(input);
            var inputBiGrams = input.ToBiGrams(false);
            var matchBiGrams = match.ToBiGrams(false);
            var biGramResult = inputBiGrams.DiceCoefficient(matchBiGrams);
            var reversedBiGramResult = matchBiGrams.DiceCoefficient(inputBiGrams);
            output.WriteLine($"DiceCoefficient of \"{match}\" against \"{input}\" was {result} (reversed was {reversedResult}), biGramResult was {biGramResult} (reversed was {reversedBiGramResult}).");

            Assert.True(Math.Abs(result - reversedResult) < double.Epsilon);
            Assert.True(Math.Abs(biGramResult - reversedBiGramResult) < double.Epsilon);
            Assert.True(Math.Abs(result - biGramResult) < double.Epsilon);
            Assert.True(result >= 0.0);
            Assert.True(result <= 1.0);
            Assert.True(biGramResult >= 0.0);
            Assert.True(biGramResult <= 1.0);
            if (input == match)
            {
                Assert.True(Math.Abs(result - 1.0) < double.Epsilon);
                Assert.True(Math.Abs(biGramResult - 1.0) < double.Epsilon);
            }
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void LevenshteinDistanceTests(string input, string match)
        {
            var result = input.LevenshteinDistance(match);
            Assert.True(result > 0);
            output.WriteLine($"LevenshteinDistance of \"{match}\" against \"{input}\" was {result}.");
        }

        [Theory]
        [InlineData("kitten", "sitting", 3)]
        [InlineData("78135", "75130", 2)]
        [InlineData("78135x", "75130x", 2)]
        public void LevenshteinDistancePreciseTests(string input, string match, int expectedResult)
        {
            var result = input.LevenshteinDistance(match);
            output.WriteLine($"LevenshteinDistance of \"{match}\" against \"{input}\" was {result}, expecting {expectedResult}.");
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("test", "w")]
        [InlineData("test", "W")]
        [InlineData("test", "w ")]
        [InlineData("test", "W ")]
        [InlineData("test", " w")]
        [InlineData("test", " W")]
        [InlineData("test", " w ")]
        [InlineData("test", " W ")]
        [InlineData("Jensn", "Adams")]
        [InlineData("Jensn", "Benson")]
        [InlineData("Jensn", "Geralds")]
        [InlineData("Jensn", "Johannson")]
        [InlineData("Jensn", "Johnson")]
        [InlineData("Jensn", "Jensen")]
        [InlineData("Jensn", "Jordon")]
        [InlineData("Jensn", "Madsen")]
        [InlineData("Jensn", "Stratford")]
        [InlineData("Jensn", "Wilkins")]
        [InlineData("2130 South Fort Union Blvd.", "2689 East Milkin Ave.")]
        [InlineData("2130 South Fort Union Blvd.", "85 Morrison")]
        [InlineData("2130 South Fort Union Blvd.", "2350 North Main")]
        [InlineData("2130 South Fort Union Blvd.", "567 West Center Street")]
        [InlineData("2130 South Fort Union Blvd.", "2130 Fort Union Boulevard")]
        [InlineData("2130 South Fort Union Blvd.", "2310 S. Ft. Union Blvd.")]
        [InlineData("2130 South Fort Union Blvd.", "98 West Fort Union")]
        [InlineData("2130 South Fort Union Blvd.", "Rural Route 2 Box 29")]
        [InlineData("2130 South Fort Union Blvd.", "PO Box 3487")]
        [InlineData("2130 South Fort Union Blvd.", "3 Harvard Square")]
        public void LongestCommonSubsequenceTests(string input, string match)
        {
            var result = input.LongestCommonSubsequence(match);
            Assert.True(result.Item2 >= 0.0);
            output.WriteLine($"LongestCommonSubsequence of \"{match}\" against \"{input}\" was \"{result.Item1}\", {result.Item2}.");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("Adams")]
        [InlineData("Benson")]
        [InlineData("Geralds")]
        [InlineData("Johannson")]
        [InlineData("Johnson")]
        [InlineData("Jensen")]
        [InlineData("Jordon")]
        [InlineData("Madsen")]
        [InlineData("Stratford")]
        [InlineData("Wilkins")]
        [InlineData("2689 East Milkin Ave.")]
        [InlineData("85 Morrison")]
        [InlineData("2350 North Main")]
        [InlineData("567 West Center Street")]
        [InlineData("2130 Fort Union Boulevard")]
        [InlineData("2310 S. Ft. Union Blvd.")]
        [InlineData("98 West Fort Union")]
        [InlineData("Rural Route 2 Box 29")]
        [InlineData("PO Box 3487")]
        [InlineData("3 Harvard Square")]
        public void ToDoubleMetaphoneTests(string input)
        {
            var result = input.ToDoubleMetaphone();
            Assert.NotNull(result);
            output.WriteLine($"ToDoubleMetaphone of \"{input}\" was {result}.");
        }
    }
}
