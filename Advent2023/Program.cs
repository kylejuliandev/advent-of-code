using Advent2023.Day1;
using Advent2023.Day2;
using Advent2023.Day3;
using Advent2023.Day4;
using Advent2023.Day5;
using Advent2023.Day6;

// Day 1

var lines = File.ReadAllLines("Day1\\Day1Calibration.txt");

var trebuchet = new Trebuchet(lines);

var calibration = trebuchet.Calibrate();
Console.WriteLine("Calibration Value: {0}", calibration);


// Day 2

var gamesInput = File.ReadAllLines("Day2\\Day2Games.txt");

var cube = new Cubes(gamesInput);

var gameIdCount = cube.GetGameCount();
Console.WriteLine("Sum of possible Game IDs: {0}", gameIdCount);

var sumOfSetPowers = cube.GetSumOfPowers();
Console.WriteLine("Sum of possible Game Set Powers: {0}", sumOfSetPowers);


// Day 3
var schematic = File.ReadAllText("Day3\\Day3Schematic.txt");

var engine = new Engine(schematic);

var sum = engine.GetPartNumberSum();

var gearRatioSum = engine.GetGearRatioSum();

Console.WriteLine("Sum of part numbers: {0}", sum);
Console.WriteLine("Gear ratio of part numbers: {0}", gearRatioSum);


// Day 4
var scratchCards = File.ReadAllText("Day4\\Day4ScratchCards.txt");

var deck = new Deck(scratchCards);

var totalPoints = deck.GetTotalScratchCardPoints();

var totalScratchCards = deck.GetNumberOfScratchCards();

Console.WriteLine("Total points: {0}", totalPoints);
Console.WriteLine("Number of scratch cards: {0}", totalScratchCards);

// Day 5
var almanacConfig = File.ReadAllText("Day5\\Day5AlmanacMap.txt");

var almanac = new Almanac(almanacConfig);

var lowestLocation = almanac.GetLowestLocationToPlant();

// Actual   = 17729183
// Expected = 15880236
var lowestLocationFromPairs = almanac.GetLowestLocationWithSeedRangesToPlant();

Console.WriteLine("Lowest Location: {0}", lowestLocation);
Console.WriteLine("Lowest Location from Seed Pairs: {0}", lowestLocationFromPairs);

// Day 6
var raceData = File.ReadAllText("Day6\\Day6Races.txt");

var boat = new Boat(raceData);
var numWaysToWin = boat.GetNumberOfWaysToWin();

Console.WriteLine("Number of ways to win races: {0}", numWaysToWin);

var numWaysToWinBigRace = boat.Part2_GetNumberOfWaysToWinOneRace();

Console.WriteLine("Number of ways to win big race: {0}", numWaysToWinBigRace);