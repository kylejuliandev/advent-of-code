using Advent2024.Day5;

Console.WriteLine("Enter your Puzzle input. Press CTRL+Z when complete");

//using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
//var consoleInput = sr.ReadToEnd();
//var input = consoleInput.Trim();

var rules =
    """
    47|53
    97|13
    97|61
    97|47
    75|29
    61|13
    75|53
    29|13
    97|29
    53|29
    61|53
    97|53
    61|29
    47|13
    75|47
    97|75
    47|61
    75|61
    47|29
    75|13
    53|13
    """;

var updates =
    """
    75,47,61,53,29
    97,61,53,29,13
    75,29,13
    75,97,47,61,53
    61,13,29
    97,13,75,29,47
    """;

//var rules = File.ReadAllText("Day5/PageRules.txt");
//var updates = File.ReadAllText("Day5/Updates.txt");

var puzzle = new Puzzle();
var result = puzzle.GetSumOfMiddleNumbers(rules, updates);

Console.WriteLine("Program result: {0}", result);