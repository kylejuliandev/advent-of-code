namespace Advent2023.Day6;

public class Boat
{
    private readonly string _raceData;
    private static readonly string[] _separator = ["\n", "\r\n"];

    public Boat(string raceData)
    {
        _raceData = raceData;
    }

    public long GetNumberOfWaysToWin()
    {
        var races = GetRaces();

        var racePossibleWins = 0;
        foreach (var race in races)
        {
            // y = (a - x) x
            var a = race.Time;
            var numberOfWaysToWin = 0;
            for (var x = 0; x < a; x++)
            {
                var y = (a - x) * x;
                if (y > race.Distance) numberOfWaysToWin++;
            }

            if (racePossibleWins == 0) racePossibleWins = numberOfWaysToWin;
            else racePossibleWins *= numberOfWaysToWin;
        }

        return racePossibleWins;
    }

    public long Part2_GetNumberOfWaysToWinOneRace()
    {
        var race = GetRace();

        // y = (a - x) x
        var a = race.Time;
        var numberOfWaysToWin = 0;
        for (var x = 0; x < a; x++)
        {
            var y = (a - x) * x;
            if (y > race.Distance) numberOfWaysToWin++;
        }

        return numberOfWaysToWin;
    }

    private Race[] GetRaces()
    {
        var raceLines = _raceData.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
        var raceTimesLine = raceLines[0];
        var raceDistancesLine = raceLines[1];

        var raceTimes = raceTimesLine[6..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var raceDistances = raceDistancesLine[9..].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var numberOfRaces = raceTimes.Length;
        var races = new Race[numberOfRaces];
        for (var raceIndex = 0; raceIndex < numberOfRaces; raceIndex++)
        {
            var time = long.Parse(raceTimes[raceIndex]);
            var distance = long.Parse(raceDistances[raceIndex]);
            races[raceIndex] = new Race(time, distance);
        }

        return races;
    }

    private Race GetRace()
    {
        var raceLines = _raceData.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
        var raceTimesLine = raceLines[0];
        var raceDistancesLine = raceLines[1];

        var raceTimes = raceTimesLine[6..].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var raceDistances = raceDistancesLine[9..].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var raceTime = long.Parse(string.Concat(raceTimes));
        var raceDistance = long.Parse(string.Concat(raceDistances));
        var race = new Race(raceTime, raceDistance);

        return race;
    }

    readonly struct Race(long time, long distance)
    {
        public long Time { get; } = time;

        public long Distance { get; } = distance;
    }
}