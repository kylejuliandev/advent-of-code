namespace Advent2023.Day5;

public class Almanac
{
    private readonly string _almanac;
    private static readonly string[] separator = ["\r\n", "\n"];

    public Almanac(string almanac)
    {
        _almanac = almanac;
    }

    public long GetLowestLocationToPlant()
    {
        var almanacLines = _almanac.Split(separator, StringSplitOptions.None);

        var seeds = GetSeeds(almanacLines[0]);
        var seedToSoilMap = GetFirstAlmanacMap(almanacLines);

        var nextMap = seedToSoilMap;
        var mappedDestinationNumbers = seeds;

        do
        {
            var tempNextMappedDestinationNumbers = new List<long>();
            var map = nextMap.GetMap();

            foreach (var mappedDestinationNumber in mappedDestinationNumbers)
            {
                var mappedSource = Array.Find(map, am => am.Source <= mappedDestinationNumber && mappedDestinationNumber <= am.Source + am.Range);
                if (mappedSource.IsInitialized)
                {
                    var diff = Math.Abs(mappedSource.Source - mappedDestinationNumber);
                    var dest = mappedSource.Destination + diff;

                    tempNextMappedDestinationNumbers.Add(dest);
                }
                else
                {
                    tempNextMappedDestinationNumbers.Add(mappedDestinationNumber);
                }
            }

            mappedDestinationNumbers = [.. tempNextMappedDestinationNumbers];
            nextMap = nextMap.GetNextAlmanacMap();
        } while (nextMap is not null);

        return mappedDestinationNumbers.Min();
    }

    public long GetLowestLocationWithSeedRangesToPlant()
    {
        var almanacLines = _almanac.Split(separator, StringSplitOptions.None);

        var seeds = GetSeedsRangePairs(almanacLines[0]);
        var lastMap = GetLastAlmanacMap(almanacLines);

        var earliestStartMap = lastMap.GetMap();
        var earliestStart = earliestStartMap.MinBy(p => p.Source);

        var maxLastLocation = earliestStart.Destination + earliestStart.Range;

        //for (var locationNumber = 0; locationNumber < maxLastLocation; locationNumber++)
        for (var locationNumber = 15880236; locationNumber < 15880237; locationNumber++)
        {
            Console.WriteLine(locationNumber);

            var prevMap = lastMap;
            long earliestDestination = locationNumber;

            do
            {
                Console.WriteLine();
                Console.WriteLine(prevMap.Type);
                Console.Write("{0}", earliestDestination);
                var map = prevMap.GetMap();
                var mappedDestination = Array.Find(map, p => p.IsDestinationAMatch(earliestDestination));
                Console.WriteLine("\t{0}", mappedDestination);

                if (mappedDestination.IsInitialized)
                {
                    var diff = Math.Abs(mappedDestination.Destination - earliestDestination);
                    var sourceToLookUpOnPrevMap = mappedDestination.Source + diff;

                    earliestDestination = sourceToLookUpOnPrevMap;
                }

                prevMap = prevMap.GetPrevAlmanacMap();
            } while (prevMap is not null);

            Console.WriteLine();
            Console.WriteLine("seed");
            Console.WriteLine(earliestDestination);
            Console.WriteLine();

            if (Array.Exists(seeds, s => s.IsMatch(earliestDestination)))
                return locationNumber;
        }

        return 0;
    }

    private static long[] GetSeeds(string seedsLine)
    {
        return seedsLine[6..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
    }

    private static AlmanacRangePair[] GetSeedsRangePairs(string seedsLine)
    {
        var seedChunks = seedsLine[6..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).Chunk(2);

        var seedDestinationNumbers = new List<AlmanacRangePair>();
        foreach (var seedChunk in seedChunks)
        {
            var start = seedChunk[0];
            var range = seedChunk[1];

            var seedPair = new AlmanacRangePair(start, range);
            Console.WriteLine(seedPair);
            seedDestinationNumbers.Add(seedPair);
        }

        Console.WriteLine();

        return [.. seedDestinationNumbers];
    }

    private static AlmanacMap GetFirstAlmanacMap(string[] almanacLines)
    {
        var seedToSoilMapIndex = Array.IndexOf(almanacLines, "seed-to-soil map:");
        var soilToFertilizerMapIndex = Array.IndexOf(almanacLines, "soil-to-fertilizer map:");
        var fertilizerToWaterMapIndex = Array.IndexOf(almanacLines, "fertilizer-to-water map:");
        var waterToLightMapIndex = Array.IndexOf(almanacLines, "water-to-light map:");
        var lightToTemperatureMapIndex = Array.IndexOf(almanacLines, "light-to-temperature map:");
        var temperatureToHumidityMapIndex = Array.IndexOf(almanacLines, "temperature-to-humidity map:");
        var humidityToLocationMapIndex = Array.IndexOf(almanacLines, "humidity-to-location map:");

        var seedToSoilMapLines = almanacLines[seedToSoilMapIndex..soilToFertilizerMapIndex];
        var soilToFertilizerMapLines = almanacLines[soilToFertilizerMapIndex..fertilizerToWaterMapIndex];
        var fertilizerToWaterMapLines = almanacLines[fertilizerToWaterMapIndex..waterToLightMapIndex];
        var waterToLightMapLines = almanacLines[waterToLightMapIndex..lightToTemperatureMapIndex];
        var lightToTemperatureMapLines = almanacLines[lightToTemperatureMapIndex..temperatureToHumidityMapIndex];
        var temperatureToHumidityMapLines = almanacLines[temperatureToHumidityMapIndex..humidityToLocationMapIndex];
        var humidityToLocationMapLines = almanacLines[humidityToLocationMapIndex..];

        var humidityToLocationMap = new AlmanacMap("humidity-to-location", humidityToLocationMapLines[1..], null);
        var temperatureToHumidityMap = new AlmanacMap("temperature-to-humidity", temperatureToHumidityMapLines[1..^1], humidityToLocationMap);
        var lightToTemperatureMap = new AlmanacMap("light-to-temperature", lightToTemperatureMapLines[1..^1], temperatureToHumidityMap);
        var waterToLightMap = new AlmanacMap("water-to-light", waterToLightMapLines[1..^1], lightToTemperatureMap);
        var fertilizerToWaterMap = new AlmanacMap("fertilizer-to-water", fertilizerToWaterMapLines[1..^1], waterToLightMap);
        var soilToFertilizerMap = new AlmanacMap("soil-to-fertilizer", soilToFertilizerMapLines[1..^1], fertilizerToWaterMap);
        var seedToSoilMap = new AlmanacMap("seed-to-soil", seedToSoilMapLines[1..^1], soilToFertilizerMap);

        return seedToSoilMap;
    }

    private static Part2AlmanacMap GetLastAlmanacMap(string[] almanacLines)
    {
        var seedToSoilMapIndex = Array.IndexOf(almanacLines, "seed-to-soil map:");
        var soilToFertilizerMapIndex = Array.IndexOf(almanacLines, "soil-to-fertilizer map:");
        var fertilizerToWaterMapIndex = Array.IndexOf(almanacLines, "fertilizer-to-water map:");
        var waterToLightMapIndex = Array.IndexOf(almanacLines, "water-to-light map:");
        var lightToTemperatureMapIndex = Array.IndexOf(almanacLines, "light-to-temperature map:");
        var temperatureToHumidityMapIndex = Array.IndexOf(almanacLines, "temperature-to-humidity map:");
        var humidityToLocationMapIndex = Array.IndexOf(almanacLines, "humidity-to-location map:");

        var seedToSoilMapLines = almanacLines[seedToSoilMapIndex..soilToFertilizerMapIndex];
        var soilToFertilizerMapLines = almanacLines[soilToFertilizerMapIndex..fertilizerToWaterMapIndex];
        var fertilizerToWaterMapLines = almanacLines[fertilizerToWaterMapIndex..waterToLightMapIndex];
        var waterToLightMapLines = almanacLines[waterToLightMapIndex..lightToTemperatureMapIndex];
        var lightToTemperatureMapLines = almanacLines[lightToTemperatureMapIndex..temperatureToHumidityMapIndex];
        var temperatureToHumidityMapLines = almanacLines[temperatureToHumidityMapIndex..humidityToLocationMapIndex];
        var humidityToLocationMapLines = almanacLines[humidityToLocationMapIndex..];

        var seedToSoilMap = new Part2AlmanacMap("seed-to-soil", seedToSoilMapLines[1..^1], null);
        var soilToFertilizerMap = new Part2AlmanacMap("soil-to-fertilizer", soilToFertilizerMapLines[1..^1], seedToSoilMap);
        var fertilizerToWaterMap = new Part2AlmanacMap("fertilizer-to-water", fertilizerToWaterMapLines[1..^1], soilToFertilizerMap);
        var waterToLightMap = new Part2AlmanacMap("water-to-light", waterToLightMapLines[1..^1], fertilizerToWaterMap);
        var lightToTemperatureMap = new Part2AlmanacMap("light-to-temperature", lightToTemperatureMapLines[1..^1], waterToLightMap);
        var temperatureToHumidityMap = new Part2AlmanacMap("temperature-to-humidity", temperatureToHumidityMapLines[1..^1], lightToTemperatureMap);
        var humidityToLocationMap = new Part2AlmanacMap("humidity-to-location", humidityToLocationMapLines[1..], temperatureToHumidityMap);

        return humidityToLocationMap;
    }

    readonly struct AlmanacRangePair(long start, long range)
    {
        public long Start { get; } = start;

        public long MaxStart { get; } = start + range;

        public long Range { get; } = range;

        public bool IsInitialized => Start != 0 && Range != 0;

        public override string ToString() => $"srt:{Start} end:{Start + Range} rng:{Range}";

        public bool IsMatch(long number) => Start <= number && number < MaxStart;
    }

    class AlmanacMap
    {
        private readonly string _type;
        private readonly string[] _lines;
        private readonly AlmanacMap? _nextMap;

        public AlmanacMap(string type, string[] lines, AlmanacMap? nextMap)
        {
            _type = type;
            _lines = lines;
            _nextMap = nextMap;
        }

        public string Type => _type;

        public AlmanacMap? GetNextAlmanacMap() => _nextMap;

        public AlmanacSourceToDestination[] GetMap()
        {
            var ranges = new List<AlmanacSourceToDestination>();

            foreach (var line in _lines)
            {
                var map = line.Split(' ').Select(long.Parse).ToArray();

                var destinationRangeStart = map[0];
                var sourceRangeStart = map[1];
                var range = map[2];

                ranges.Add(new AlmanacSourceToDestination(sourceRangeStart, destinationRangeStart, range));
            }

            return [.. ranges];
        }
    }

    class Part2AlmanacMap
    {
        private readonly string _type;
        private readonly string[] _lines;
        private readonly Part2AlmanacMap? _prevMap;

        public Part2AlmanacMap(string type, string[] lines, Part2AlmanacMap? prevMap)
        {
            _type = type;
            _lines = lines;
            _prevMap = prevMap;
        }

        public string Type => _type;

        public Part2AlmanacMap? GetPrevAlmanacMap() => _prevMap;

        public AlmanacSourceToDestination[] GetMap()
        {
            var ranges = new List<AlmanacSourceToDestination>();

            foreach (var line in _lines)
            {
                var map = line.Split(' ').Select(long.Parse).ToArray();

                var destinationRangeStart = map[0];
                var sourceRangeStart = map[1];
                var range = map[2];

                ranges.Add(new AlmanacSourceToDestination(sourceRangeStart, destinationRangeStart, range));
            }

            return [.. ranges];
        }
    }

    readonly struct AlmanacSourceToDestination(long source, long destination, long range)
    {
        public long Source { get; } = source;

        public long MaxSource { get; } = source + range;

        public long Destination { get; } = destination;

        public long MaxDestination { get; } = destination + range;

        public long Range { get; } = range;

        public bool IsInitialized => Range != 0;

        public bool IsDestinationAMatch(long destination) => destination >= Destination && destination <= MaxDestination;

        public override string ToString() => $"dst:{Destination} src:{Source} rng:{Range}";
    }
}