using System.Text.RegularExpressions;

namespace Advent2024.Day1;

public partial class Puzzle2
{
    [GeneratedRegex("\\s")]
    private static partial Regex MatchWhitespace();

    public int GetSimilarityScore(string[] input)
    {
        var locIds = GetLocationIds(input);

        var left = locIds.Left;
        var right = locIds.Right;

        var score = 0;

        for (int i = 0; i < left.Count; i++)
        {
            var leftItem = left[i];

            var countOfMatches = right.Count(r => leftItem == r);

            var similarity = leftItem * countOfMatches;

            score += similarity;
        }

        return score;
    }


    private static (List<int> Left, List<int> Right) GetLocationIds(string[] input)
    {
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in input)
        {
            var locations = MatchWhitespace().Split(line);

            var leftLocation = locations[0];
            var rightLocation = locations[^1];

            left.Add(int.Parse(leftLocation));
            right.Add(int.Parse(rightLocation));
        }

        return (left, right);
    }
}
