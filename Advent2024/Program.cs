using Advent2024.Day2;

Console.WriteLine("Enter your Puzzle input. Press CTRL+Z when complete");

using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
var consoleInput = sr.ReadToEnd();
var input = consoleInput.Trim().Split(Environment.NewLine);

var puzzle1 = new Puzzle1();
var numSafeReports = puzzle1.GetNumberOfSafeReportsWithDampeners(input);

Console.WriteLine("Number of safe reports: {0}", numSafeReports);