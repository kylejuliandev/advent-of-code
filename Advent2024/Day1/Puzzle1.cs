using System.Text.RegularExpressions;

namespace Advent2024.Day1;

public partial class Puzzle1
{
    [GeneratedRegex("\\s")]
    private static partial Regex MatchWhitespace();

    public int GetDistance(string[] input)
    {
        var locIds = GetLocationIds(input);

        var left = locIds.Left;
        var right = locIds.Right;

        left.Sort();
        right.Sort();

        var total = 0;
        for (var i = 0; i < left.Count; i++)
        {
            var leftItem = left[i];
            var rightItem = right[i];

            var distance = Math.Abs(leftItem - rightItem);
            total += distance;
        }

        return total;
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
