using Advent2024.Day4;

Console.WriteLine("Enter your Puzzle input. Press CTRL+Z when complete");

using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
var consoleInput = sr.ReadToEnd();
var input = consoleInput.Trim();

var puzzle = new Puzzle();
var result = puzzle.NumberOfXmasWords(input);

Console.WriteLine("Program result: {0}", result);