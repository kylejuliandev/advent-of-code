namespace Advent2023.Day3;

public class Engine
{
    private readonly string _schematic;

    private static readonly string[] _separator = ["\r\n", "\n"];

    public Engine(string schematic)
    {
        _schematic = schematic;
    }

    public int GetPartNumberSum()
    {
        var schematicRows = _schematic.Split(_separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var numberOfRows = schematicRows.Length;
        var numberOfCols = schematicRows[0].Length;
        
        var partNumbers = new List<int>();

        for (var rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            var row = schematicRows[rowIndex];
            var numberOfCharactersInPartNumber = 0;
            for (var colIndex = 0; colIndex < numberOfCols; colIndex++)
            {
                var col = row[colIndex];

                if (char.IsDigit(col))
                {
                    if (colIndex + 1 < numberOfCols)
                    {
                        numberOfCharactersInPartNumber++;
                        continue;
                    }
                }

                if (numberOfCharactersInPartNumber > 0)
                {
                    var startIndexOfPartNumber = colIndex - numberOfCharactersInPartNumber;
                    var endIndexOfPartNumber = colIndex;

                    if (colIndex + 1 == numberOfCols && char.IsDigit(col))
                    {
                        // Include the last digit in the part number, otherwise ignore it
                        endIndexOfPartNumber++;
                    }

                    var number = int.Parse(row[startIndexOfPartNumber..endIndexOfPartNumber]);

                    if (col != '.' && !char.IsDigit(col)) partNumbers.Add(number);

                    if (startIndexOfPartNumber - 1 > 0)
                        if (row[startIndexOfPartNumber - 1] != '.' && !char.IsDigit(row[startIndexOfPartNumber - 1])) partNumbers.Add(number);

                    // Row below checks

                    var nextRow = rowIndex + 1;
                    if (nextRow < numberOfRows)
                    {
                        for (var i = 1; i <= numberOfCharactersInPartNumber; i++)
                        {
                            var nextRowCol = colIndex - i;
                            var nextRowColValue = schematicRows[nextRow][nextRowCol];
                            if (nextRowColValue != '.' && !char.IsDigit(nextRowColValue)) partNumbers.Add(number);
                        }

                        // Diagonal checks

                        var startOfPartNumber = colIndex - numberOfCharactersInPartNumber;
                        var leftDiagonal = startOfPartNumber - 1;
                        if (leftDiagonal > 0)
                            if (schematicRows[nextRow][leftDiagonal] != '.' && !char.IsDigit(schematicRows[nextRow][leftDiagonal])) partNumbers.Add(number);

                        var rightDiagonal = colIndex;
                        if (rightDiagonal < numberOfCols)
                            if (schematicRows[nextRow][rightDiagonal] != '.' && !char.IsDigit(schematicRows[nextRow][rightDiagonal])) partNumbers.Add(number);
                    }

                    // Row above checks

                    var prevRow = rowIndex - 1;
                    if (prevRow > 0)
                    {
                        for (var i = 1; i <= numberOfCharactersInPartNumber; i++)
                        {
                            var prevRowCol = colIndex - i;
                            var prevRowColValue = schematicRows[prevRow][prevRowCol];
                            if (prevRowColValue != '.' && !char.IsDigit(prevRowColValue)) partNumbers.Add(number);
                        }

                        // Diagonal checks

                        var startOfPartNumber = colIndex - numberOfCharactersInPartNumber;
                        var leftDiagonal = startOfPartNumber - 1;
                        if (leftDiagonal > 0)
                            if (schematicRows[prevRow][leftDiagonal] != '.' && !char.IsDigit(schematicRows[prevRow][leftDiagonal])) partNumbers.Add(number);

                        var rightDiagonal = colIndex;
                        if (rightDiagonal < numberOfCols)
                            if (schematicRows[prevRow][rightDiagonal] != '.' && !char.IsDigit(schematicRows[prevRow][rightDiagonal])) partNumbers.Add(number);
                    }

                    numberOfCharactersInPartNumber = 0;

                    //Console.WriteLine("row {0}, col {1}, part number {2}, sum {3}", rowIndex, colIndex, number, partNumbers.Sum());
                }
            }
        }

        return partNumbers.Sum();
    }

    public int GetGearRatioSum()
    {
        var schematicRows = _schematic.Split(_separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var numberOfRows = schematicRows.Length;
        var numberOfCols = schematicRows[0].Length;

        var gearRatios = new List<int>();

        for (var rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
        {
            var row = schematicRows[rowIndex];

            for (var colIndex = 0; colIndex < numberOfCols; colIndex++)
            {

                var adjacentPartNumbers = new List<int>();
                var col = row[colIndex];

                if (col != '*') continue;

                // Row above
                var prevRowIndex = rowIndex - 1;
                if (prevRowIndex >= 0)
                {
                    var prevRow = schematicRows[prevRowIndex];
                    if (char.IsDigit(prevRow[colIndex]))
                    {
                        if (colIndex + 1 > numberOfCols) int.Parse(prevRow[colIndex..colIndex]);

                        var sliceStart = colIndex;
                        var sliceEnd = colIndex + 1;
                        for (var i = colIndex + 1; i < numberOfCols; i++)
                        {
                            var prevRowNextCol = prevRow[i];
                            if (char.IsDigit(prevRowNextCol)) sliceEnd++;
                            else break;
                        }

                        for (var i = colIndex - 1; 0 < i; i--)
                        {
                            var prevRowPrevCol = prevRow[i];
                            if (char.IsDigit(prevRowPrevCol)) sliceStart--;
                            else break;
                        }

                        var num = int.Parse(prevRow[sliceStart..sliceEnd]);
                        adjacentPartNumbers.Add(num);
                    }
                    else
                    {
                        // Right Diagonal
                        if (colIndex + 1 < numberOfCols)
                        {
                            var prevRowDiagonalRightIndex = colIndex + 1;
                            var prevRowDiagonalRight = prevRow[prevRowDiagonalRightIndex];
                            if (char.IsDigit(prevRowDiagonalRight))
                            {
                                var sliceStart = colIndex + 1;
                                var sliceEnd = colIndex + 1;
                                for (var i = colIndex + 1; i < numberOfCols; i++)
                                {
                                    var nextRowNextCol = prevRow[i];
                                    if (char.IsDigit(nextRowNextCol)) sliceEnd++;
                                    else break;
                                }

                                var num = int.Parse(prevRow[sliceStart..sliceEnd]);
                                adjacentPartNumbers.Add(num);
                            }
                        }

                        // Left Diagonal
                        if (colIndex - 1 >= 0)
                        {
                            var prevRowDiagonalLeftIndex = colIndex - 1;
                            var prevRowDiagonalLeft = prevRow[prevRowDiagonalLeftIndex];
                            if (char.IsDigit(prevRowDiagonalLeft))
                            {
                                var digitCount = 0;
                                var sliceEnd = colIndex;
                                for (var i = colIndex - 1; -1 < i; i--)
                                {
                                    var nextRowNextCol = prevRow[i];
                                    if (char.IsDigit(nextRowNextCol)) digitCount++;
                                    else break;
                                }

                                var sliceStart = sliceEnd - digitCount;
                                var num = int.Parse(prevRow[sliceStart..sliceEnd]);
                                adjacentPartNumbers.Add(num);
                            }
                        }
                    }
                }

                // Row below
                var nextRowIndex = rowIndex + 1;
                if (nextRowIndex < numberOfRows)
                {
                    var nextRow = schematicRows[nextRowIndex];
                    if (char.IsDigit(nextRow[colIndex]))
                    {
                        if (colIndex + 1 > numberOfCols) int.Parse(nextRow[colIndex..colIndex]);

                        var sliceStart = colIndex;
                        var sliceEnd = colIndex + 1;
                        for (var i = colIndex + 1; i < numberOfCols; i++)
                        {
                            var nextRowNextCol = nextRow[i];
                            if (char.IsDigit(nextRowNextCol)) sliceEnd++;
                            else break;
                        }

                        for (var i = colIndex - 1; 0 < i; i--)
                        {
                            var nextRowPrevCol = nextRow[i];
                            if (char.IsDigit(nextRowPrevCol)) sliceStart--;
                            else break;
                        }

                        var num = int.Parse(nextRow[sliceStart..sliceEnd]);
                        adjacentPartNumbers.Add(num);
                    }
                    else
                    {
                        // Right Diagonal
                        if (colIndex + 1 < numberOfCols)
                        {
                            var nextRowDiagonalRightIndex = colIndex + 1;
                            var nextRowDiagonalRight = nextRow[nextRowDiagonalRightIndex];
                            if (char.IsDigit(nextRowDiagonalRight))
                            {
                                var sliceStart = colIndex + 1;
                                var sliceEnd = colIndex + 1;
                                for (var i = colIndex + 1; i < numberOfCols; i++)
                                {
                                    var nextRowNextCol = nextRow[i];
                                    if (char.IsDigit(nextRowNextCol)) sliceEnd++;
                                    else break;
                                }

                                var num = int.Parse(nextRow[sliceStart..sliceEnd]);
                                adjacentPartNumbers.Add(num);
                            }
                        }

                        // Left Diagonal
                        if (colIndex - 1 > 0)
                        {
                            var nextRowDiagonalLeftIndex = colIndex - 1;
                            var nextRowDiagonalLeft = nextRow[nextRowDiagonalLeftIndex];
                            if (char.IsDigit(nextRowDiagonalLeft))
                            {
                                var digitCount = 0;
                                var sliceEnd = colIndex;
                                for (var i = colIndex - 1; -1 < i; i--)
                                {
                                    var nextRowNextCol = nextRow[i];
                                    if (char.IsDigit(nextRowNextCol)) digitCount++;
                                    else break;
                                }

                                var sliceStart = sliceEnd - digitCount;
                                var num = int.Parse(nextRow[sliceStart..sliceEnd]);
                                adjacentPartNumbers.Add(num);
                            }
                        }
                    }
                }

                // Right
                if (colIndex + 1 < numberOfCols)
                {
                    var nextRightIndex = colIndex + 1;
                    var nextRight = row[nextRightIndex];
                    if (char.IsDigit(nextRight))
                    {
                        var sliceStart = colIndex + 1;
                        var sliceEnd = colIndex + 1;
                        for (var i = colIndex + 1; i < numberOfCols; i++)
                        {
                            var nextRowNextCol = row[i];
                            if (char.IsDigit(nextRowNextCol)) sliceEnd++;
                            else break;
                        }

                        var num = int.Parse(row[sliceStart..sliceEnd]);
                        adjacentPartNumbers.Add(num);
                    }
                }

                // Left
                if (colIndex - 1 > 0)
                {
                    var nextLeftIndex = colIndex - 1;
                    var nextLeft = row[nextLeftIndex];
                    if (char.IsDigit(nextLeft))
                    {
                        var sliceStart = colIndex;
                        var sliceEnd = colIndex;
                        for (var i = colIndex - 1; 0 < i; i--)
                        {
                            var nextRowNextCol = row[i];
                            if (char.IsDigit(nextRowNextCol)) sliceStart--;
                            else break;
                        }

                        var num = int.Parse(row[sliceStart..sliceEnd]);
                        adjacentPartNumbers.Add(num);
                    }
                }

                if (adjacentPartNumbers.Count == 2)
                {
                    var partNum1 = adjacentPartNumbers[0];
                    var partNum2 = adjacentPartNumbers[1];
                    gearRatios.Add(partNum1 * partNum2);

                    Console.WriteLine("row {0}, adjacent part numbers {1} and {2}", rowIndex, partNum1, partNum2);
                }
            }
        }

        return gearRatios.Sum();
    }
}