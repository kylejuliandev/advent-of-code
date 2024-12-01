using Advent2024.Day1;

using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
var consoleInput = sr.ReadToEnd();
var input = consoleInput.Trim().Split(Environment.NewLine);

var puzzle1 = new Puzzle1();
var distance = puzzle1.GetDistance(input);

Console.WriteLine("Distance: {0}", distance);

var puzzle2 = new Puzzle2();
var score = puzzle2.GetSimilarityScore(input);

Console.WriteLine("Similarity {0}", score);