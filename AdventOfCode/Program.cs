// See https://aka.ms/new-console-template for more information
using AdventOfCode.Day1;

Console.WriteLine("Hello, World!");

var puzzle1 = new Puzzle1();

var input = File.ReadAllLines("Day1\\puzzle1.txt");
var distance = puzzle1.GetDistance(input);

Console.WriteLine("Distance: {0}", distance);

var puzzle2 = new Puzzle2();

var score = puzzle2.GetSimilarityScore(input);

Console.WriteLine("Similarity {0}", score);

