duovia-fuzzystrings
==========

DuoVia.FuzzyStrings
----------
### A Collection of Fuzzy String Algorithms for .NET.

> This is partially derived from multiple open sources. See individual algorithm classes for attribution.
>
> A developer may wish to take advantage of one or more of the algorithms included in this libray or on the contrived string comparison extension methods like this:
'''C#
bool isEqual = input.FuzzyEquals(name);
double coefficient = input.FuzzyMatch(name);
'''
>
Included Algorithms
-------------------

### Dice Coefficient based on bigrams
> A good value would be 0.33 or above, a value under 0.2 is not a good match, from 0.2 to 0.33 is iffy.

### Levenshtein Distance algorithm with transposition
> A value of 1 or 2 is okay, 3 is iffy and greater than 4 is a poor match

### Longest Common Subsequence
> A good value is greater than 0.33.

### DoubleMetaphone
> Get a 4 character "soundex"
> 
> The author hopes you will use and help improve this library.
