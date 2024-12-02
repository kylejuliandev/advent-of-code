using Advent2024.Day2;

namespace Advent2024.Tests.Day2;

public class Tests
{
    Puzzle1 _puzzle;

    [SetUp]
    public void SetUp()
    {
        _puzzle = new Puzzle1();
    }

    [TestCase("1 5 6 7 8", ExpectedResult = false)]
    public bool SafeReportsWithoutDampeners(string input)
    {
        return _puzzle.GetNumberOfSafeReports([input]) == 1;
    }

    [TestCase("7 6 4 2 1", ExpectedResult = true)]
    [TestCase("1 2 7 8 9", ExpectedResult = false)]
    [TestCase("9 7 6 2 1", ExpectedResult = false)]
    [TestCase("1 3 2 4 5", ExpectedResult = true)]
    [TestCase("8 6 4 4 1", ExpectedResult = true)]
    [TestCase("1 3 6 7 9", ExpectedResult = true)]
    [TestCase("3 5 4 3 2", ExpectedResult = true)]
    [TestCase("6 9 10 12 15 16", ExpectedResult = true)]
    [TestCase("11 16 20 22 23 26 27 31", ExpectedResult = false)]
    [TestCase("4 6 8 6 6", ExpectedResult = false)]
    [TestCase("33 40 45 46 46", ExpectedResult = false)]
    [TestCase("79 79 78 79 79", ExpectedResult = false)]
    [TestCase("48 46 47 49 51 54 56", ExpectedResult = true)]
    [TestCase("1 1 2 3 4 5", ExpectedResult = true)]
    [TestCase("1 2 3 4 5 5", ExpectedResult = true)]
    [TestCase("5 1 2 3 4 5", ExpectedResult = true)]
    [TestCase("1 4 3 2 1", ExpectedResult = true)]
    [TestCase("1 6 7 8 9", ExpectedResult = true)]
    [TestCase("1 2 3 4 3", ExpectedResult = true)]
    [TestCase("9 8 7 6 7", ExpectedResult = true)]
    [TestCase("7 10 8 10 11", ExpectedResult = true)]
    [TestCase("29 28 27 25 26 25 22 20", ExpectedResult = true)]
    [TestCase("8 9 10 11", ExpectedResult = true)]
    [TestCase("31 34 32 30 28 27 24 22", ExpectedResult = true)]
    [TestCase("75 77 72 70 69", ExpectedResult = true)]
    public bool SafeReportsWithDampeners(string input)
    {
        return _puzzle.GetNumberOfSafeReportsWithDampeners([input]) == 1;
    }
}
