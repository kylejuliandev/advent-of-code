namespace Advent2023.Day2;

public class Cubes
{
    private readonly string[] _gamesInput;

    public Cubes(string[] gamesInput)
    {
        _gamesInput = gamesInput;
    }

    public int GetGameCount()
    {
        var bag = new ElfBag(redCubes: 12, greenCubes: 13, blueCubes: 14);

        var gameIdCount = 0;
        foreach (var gameLine in _gamesInput)
        {
            var (isPossible, gameId) = bag.IsPartOnePossible(gameLine);

            if (isPossible) gameIdCount += gameId;
        }

        return gameIdCount;
    }

    public int GetSumOfPowers()
    {
        var bag = new ElfBag(redCubes: 12, greenCubes: 13, blueCubes: 14);

        var sumOfSetPowers = 0;
        foreach (var gameLine in _gamesInput)
        {
            var power = bag.GetGameSetPower(gameLine);

            sumOfSetPowers += power;
        }

        return sumOfSetPowers;
    }
}

public class ElfBag(int redCubes, int greenCubes, int blueCubes)
{
    private readonly int _redCubes = redCubes;
    private readonly int _greenCubes = greenCubes;
    private readonly int _blueCubes = blueCubes;

    public (bool IsPossible, int GameId) IsPartOnePossible(string gameLine)
    {
        var game = Game.FromGameLine(gameLine);

        foreach (var set in game.Sets)
        {
            if (set.Red > _redCubes) return (false, game.GameId);
            if (set.Green > _greenCubes) return (false, game.GameId);
            if (set.Blue > _blueCubes) return (false, game.GameId);
        }

        Console.WriteLine("Game {0}: Is Possible", game.GameId);

        return (true, game.GameId);
    }

    public int GetGameSetPower(string gameLine)
    {
        var game = Game.FromGameLine(gameLine);

        var highestRed = 0;
        var highestGreen = 0;
        var highestBlue = 0;

        foreach (var set in game.Sets)
        {
            if (set.Red > highestRed) highestRed = set.Red;
            if (set.Green > highestGreen) highestGreen = set.Green;
            if (set.Blue > highestBlue) highestBlue = set.Blue;
        }

        Console.WriteLine("Game {0}: Is Possible with {1} reds, {2} greens, {3} blues",
            game.GameId, highestRed, highestGreen, highestBlue);

        return highestRed * highestGreen * highestBlue;
    }
}

public readonly struct Game(int gameId, Set[] sets)
{
    public int GameId { get; } = gameId;
    public Set[] Sets { get; } = sets;

    public static Game FromGameLine(string gameLine)
    {
        var gameSplit = gameLine.Split(':');
        var firstSpace = gameSplit[0].IndexOf(' ') + 1;

        var gameId = int.Parse(gameSplit[0][firstSpace..]);
        var sets = new List<Set>();

        var setSplits = gameSplit[1].Split(';');

        foreach (var setSplit in setSplits)
        {
            var numRed = 0;
            var numGreen = 0;
            var numBlue = 0;

            var setColours = setSplit.Split(',');
            foreach (var colourSet in setColours)
            {
                var colours = colourSet.Trim().Split(' ');
                var colourCount = int.Parse(colours[0]);
                var colourId = colours[1].Trim();

                if (colourId == "red")
                    numRed += colourCount;

                else if (colourId == "green")
                    numGreen += colourCount;

                else if (colourId == "blue")
                    numBlue += colourCount;

                else
                    throw new Exception("Unexpected colour!");
            }

            sets.Add(new Set(red: numRed, green: numGreen, blue: numBlue));
        }

        return new Game(gameId, sets.ToArray());
    }
}

public readonly struct Set(int red, int green, int blue)
{
    public int Red { get; } = red;
    public int Green { get; } = green;
    public int Blue { get; } = blue;

    public override string ToString() => $"{Red} red, {Green} green, {Blue} blue";
}
