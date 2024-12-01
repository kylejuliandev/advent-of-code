namespace Advent2023.Day1;

public class Trebuchet
{
    private readonly string[] _lines;

    public Trebuchet(string[] lines)
    {
        _lines = lines;
    }

    public int Calibrate()
    {
        var sum = 0;

        foreach (var line in _lines)
        {
            List<char> digits = [];
            var charsLookupCountFromLastDigit = 1;

            for (var i = 0; i < line.Length; i++)
            {
                var character = line[i];

                if (char.IsDigit(character))
                {
                    digits.Add(character);
                    charsLookupCountFromLastDigit = 1;
                }
                else
                {
                    if (charsLookupCountFromLastDigit > 2)
                    {
                        var endIndex = i + 1;
                        var startIndex = Math.Clamp(endIndex - charsLookupCountFromLastDigit, 0, int.MaxValue);
                        if (startIndex > 1) startIndex--;

                        var word = line[startIndex..endIndex];
                        var match = WordMatchesDigit(word);
                        if (match.HasValue)
                        {
                            digits.Add(match.Value);
                            charsLookupCountFromLastDigit = 1;
                        }
                    }
                }

                charsLookupCountFromLastDigit++;
            }

            var lineValues = $"{digits[0]}{digits[^1]}";
            var value = int.Parse(lineValues);

            sum += value;
        }

        return sum;
    }

    private static char? WordMatchesDigit(string word) => word switch
    {
        string a when a.Contains("one") => '1',
        string a when a.Contains("two") => '2',
        string a when a.Contains("three") => '3',
        string a when a.Contains("four") => '4',
        string a when a.Contains("five") => '5',
        string a when a.Contains("six") => '6',
        string a when a.Contains("seven") => '7',
        string a when a.Contains("eight") => '8',
        string a when a.Contains("nine") => '9',
        _ => null
    };
}