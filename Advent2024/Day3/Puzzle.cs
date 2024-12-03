using System.Text.RegularExpressions;

namespace Advent2024.Day3;

public partial class Puzzle
{
    [GeneratedRegex("mul\\((\\d{1,3})\\,(\\d{1,3})\\)")]
    private static partial Regex MatchMuls();

    [GeneratedRegex("(?<dont>don't\\(\\))|(?<do>do\\(\\))|(?<mul>mul\\((?<left>\\d{1,3})\\,(?<right>\\d{1,3})\\))")]
    private static partial Regex MatchMulsConditional();

    public long GetProgramResult(string input)
    {
        var matches = MatchMuls().Matches(input);

        long sum = 0;
        foreach (Match match in matches)
        {
            var left = int.Parse(match.Groups[1].Value);
            var right = int.Parse(match.Groups[2].Value);

            var result = left * right;

            sum += result;
        }

        return sum;
    }

    public long GetProgramResultWithConditions(string input)
    {
        var matches = MatchMulsConditional().Matches(input);

        long sum = 0;
        var carryOutExecution = true;
        foreach (Match match in matches)
        {
            var groups = match.Groups;
            var dont = groups["dont"];
            var @do = groups["do"];
            var mul = groups["mul"];

            if (dont.Success)
            {
                if (@do.Success)
                {
                    if (dont.Index > @do.Index) carryOutExecution = false;
                    else carryOutExecution = true;
                }
                else
                {
                    carryOutExecution = false;
                }
            }
            else
            {
                if (@do.Success)
                {
                    carryOutExecution = true;
                }
            }

            long result = 0;
            if (mul.Success)
            {
                var left = int.Parse(groups["left"].Value);
                var right = int.Parse(groups["right"].Value);

                result = left * right;
            }

            if (carryOutExecution)
                sum += result;
        }

        return sum;
    }
}
