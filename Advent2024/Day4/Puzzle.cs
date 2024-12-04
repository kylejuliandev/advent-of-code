namespace Advent2024.Day4;

public class Puzzle
{
    static readonly string _word = "XMAS";
    static readonly string _wordInReverse = "SAMX";
    static readonly int _wordLength = _word.Length;

    public int NumberOfXmasWords(string input)
    {
        var grid = GetGrid(input);

        var count = GetCount(grid);

        return count;
    }

    public int NumberOfX_MASWords(string input)
    {
        var grid = GetGrid(input);

        var count = GetXmasCount(grid);

        return count;
    }

    private static int GetCount(char[,] grid)
    {
        var count = 0;
        
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (var rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (var colIndex = 0; colIndex < cols; colIndex++)
            {
                // Vertical
                if (rowIndex + _wordLength <= rows)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 1, 0, _word, _wordInReverse);
                    if (match) count++;
                }

                // Horizontal
                if (colIndex + _wordLength <= cols)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 0, 1, _word, _wordInReverse);
                    if (match) count++;
                }

                // Right diagonal
                if (rowIndex + _wordLength <= rows && colIndex + _wordLength <= cols)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 1, 1, _word, _wordInReverse);
                    if (match) count++;
                }

                // Left diagonal
                if (rowIndex + _wordLength <= rows && colIndex - (_wordLength - 1) >= 0)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 1, -1, _word, _wordInReverse);
                    if (match) count++;
                }
            }
        }

        return count;
    }

    private static int GetXmasCount(char[,] grid)
    {
        var count = 0;

        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        for (int rowIndex = 1; rowIndex < rows - 1; rowIndex++)
        {
            for (int colIndex = 1; colIndex < cols - 1; colIndex++)
            {
                if (grid[rowIndex, colIndex] == 'A')
                {
                    char topLeft = grid[rowIndex - 1, colIndex - 1];
                    char topRight = grid[rowIndex - 1, colIndex + 1];
                    char bottomLeft = grid[rowIndex + 1, colIndex - 1];
                    char bottomRight = grid[rowIndex + 1, colIndex + 1];

                    string pattern1 = $"{topLeft}A{bottomRight}";
                    string pattern2 = $"{topRight}A{bottomLeft}";

                    if ((pattern1 == "MAS" || pattern1 == "SAM") &&
                        (pattern2 == "MAS" || pattern2 == "SAM"))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private static bool IsMatch(char[,] grid, int row, int col, int dRow, int dCol, string word, string wordInReverse)
    {
        char[] chars = new char[word.Length];
        var coords = new List<(int, int)>();

        for (int i = 0; i < word.Length; i++)
        {
            int newRow = row + i * dRow;
            int newCol = col + i * dCol;

            chars[i] = grid[newRow, newCol];

            coords.Add((newRow, newCol));
        }

        var currentWord = new string(chars);
        var match = currentWord == word || currentWord == wordInReverse;

        if (match)
        {
            for (var i = 0; i < coords.Count; i++)
            {
                var coordinate = coords[i];
                var @char = chars[i];
                Console.Write("{0} [{1}]", coordinate, @char);
            }
            Console.WriteLine();
        }

        return match;
    }

    private static char[,] GetGrid(string input)
    {
        var lines = input.Split(Environment.NewLine);

        var rows = lines.Length;
        var cols = lines[0].Trim().Length;

        var grid = new char[rows, cols];
        
        for (var i = 0; i < rows; i++)
        {
            var line = lines[i].Trim();

            for (var j = 0; cols > j; j++)
            {
                grid[i, j] = line[j];
            }
        }

        return grid;
    }
}
