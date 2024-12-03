using Advent2024.Day3;

namespace Advent2024.Tests.Day3;

internal class Tests
{
    Puzzle _puzzle;

    [SetUp]
    public void SetUp()
    {
        _puzzle = new Puzzle();
    }

    [TestCase("mul(44,46)", ExpectedResult = 2024)]
    [TestCase("mul(11,8)mul(8,5)", ExpectedResult = 128)]
    [TestCase("mul(123,4)", ExpectedResult = 492)]
    [TestCase("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", ExpectedResult = 161)]
    [TestCase("mul(0,0)", ExpectedResult = 0)]
    [TestCase("mul(1,1)", ExpectedResult = 1)]
    [TestCase("mul(999,999)", ExpectedResult = 998001)]
    public long GetProgramResult_ReturnsResult(string input)
    {
        return _puzzle.GetProgramResult(input);
    }

    [TestCase("mul(1000,1001)")]
    [TestCase("mul(-1,-3)")]
    [TestCase("mul ( 2 , 4 )")]
    [TestCase("mul(4*")]
    [TestCase("mul(6,9!")]
    [TestCase("?(12,34)")]
    [TestCase("mul[3,7]")]
    [TestCase("mul(32,64]")]
    [TestCase("mul(12;34)")]
    [TestCase("mul(12 34)")]
    [TestCase("mul()")]
    [TestCase("mul(1234,56)")]
    [TestCase("mul(12,34extra)")]
    [TestCase("mul(12,3456)")]
    [TestCase("mul(1,\n12)")]
    public void GetProgramResult_ReturnsZero(string input)
    {
        var result = _puzzle.GetProgramResult(input);

        Assert.That(result, Is.EqualTo(0));
    }

    [TestCase("mul(44,46)", ExpectedResult = 2024)]
    [TestCase("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", ExpectedResult = 48)]
    public long GetProgramResultWithConditions_ReturnsResult(string input)
    {
        return _puzzle.GetProgramResultWithConditions(input);
    }
}