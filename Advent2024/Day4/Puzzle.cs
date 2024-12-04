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
                    var match = IsMatch(grid, rowIndex, colIndex, 1, 0);
                    if (match) count++;
                }

                // Horizontal
                if (colIndex + _wordLength <= cols)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 0, 1);
                    if (match) count++;
                }

                // Right diagonal
                if (rowIndex + _wordLength <= rows && colIndex + _wordLength <= cols)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 1, 1);
                    if (match) count++;
                }

                // Left diagonal
                if (rowIndex + _wordLength <= rows && colIndex - (_wordLength - 1) >= 0)
                {
                    var match = IsMatch(grid, rowIndex, colIndex, 1, -1);
                    if (match) count++;
                }
            }
        }

        return count;
    }

    private static bool IsMatch(char[,] grid, int row, int col, int dRow, int dCol)
    {
        char[] chars = new char[_word.Length];
        var coords = new List<(int, int)>();

        for (int i = 0; i < _word.Length; i++)
        {
            int newRow = row + i * dRow;
            int newCol = col + i * dCol;

            chars[i] = grid[newRow, newCol];

            coords.Add((newRow, newCol));
        }

        var word = new string(chars);
        var match = word == _word || word == _wordInReverse;

        if (match)
        {
            Console.WriteLine("{0},{1},{2},{3}",
                $"{coords[0]} {chars[0]}",
                $"{coords[1]} {chars[1]}",
                $"{coords[2]} {chars[2]}",
                $"{coords[3]} {chars[3]}");
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
