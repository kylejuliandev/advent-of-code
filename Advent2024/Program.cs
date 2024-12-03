using Advent2024.Day3;

Console.WriteLine("Enter your Puzzle input. Press CTRL+Z when complete");

using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
var consoleInput = sr.ReadToEnd();
var input = consoleInput.Trim();

var puzzle1 = new Puzzle();
var result = puzzle1.GetProgramResultWithConditions(input);

Console.WriteLine("Program result: {0}", result);