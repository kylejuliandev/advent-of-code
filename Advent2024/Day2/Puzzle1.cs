namespace Advent2024.Day2;

public class Puzzle1
{
    public int GetNumberOfSafeReports(string[] input)
    {
        var reports = GetReports(input);

        var numberOfSafeReports = 0;
        for (var reportIndex = 0; reportIndex < reports.Count; reportIndex++)
        {
            var report = reports[reportIndex];
            var reportIsSafe = CheckIfReportIsSafe(report);

            if (reportIsSafe)
            {
                numberOfSafeReports++;
            }
        }

        return numberOfSafeReports;
    }

    public int GetNumberOfSafeReportsWithDampeners(string[] input)
    {
        var reports = GetReports(input);

        var numberOfSafeReports = 0;
        for (var reportIndex = 0; reportIndex < reports.Count; reportIndex++)
        {
            var report = reports[reportIndex];
            var reportIsSafe = CheckIfReportIsSafeWithDampeners(report);

            if (reportIsSafe)
            {
                numberOfSafeReports++;
            }
        }

        return numberOfSafeReports;
    }

    private static bool CheckIfReportIsSafe(List<int> report)
    {
        var reportIsSafe = false;
        var increasing = report[0] < report[1];

        for (int i = 0; i < report.Count - 1; i++)
        {
            var level = report[i];
            var nextLevel = report[i + 1];

            if (Math.Abs(level - nextLevel) <= 3)
            {
                if (increasing)
                {
                    if (level < nextLevel)
                    {
                        reportIsSafe = true;
                    }
                    else
                    {
                        reportIsSafe = false;
                        break;
                    }

                }
                else
                {
                    if (level > nextLevel)
                    {
                        reportIsSafe = true;
                    }
                    else
                    {
                        reportIsSafe = false;
                        break;
                    }
                }
            }
            else
            {
                reportIsSafe = false;
                break;
            }
        }

        return reportIsSafe;
    }

    private static bool CheckIfReportIsSafeWithDampeners(List<int> report, bool increasing)
    {
        var reportIsSafe = false;

        for (int i = 0; i < report.Count - 1; i++)
        {
            var level = report[i];
            var nextLevel = report[i + 1];

            if (Math.Abs(level - nextLevel) <= 3)
            {
                if (increasing)
                {
                    if (level < nextLevel)
                    {
                        reportIsSafe = true;
                    }
                    else
                    {
                        reportIsSafe = false;
                        break;
                    }

                }
                else
                {
                    if (level > nextLevel)
                    {
                        reportIsSafe = true;
                    }
                    else
                    {
                        reportIsSafe = false;
                        break;
                    }
                }
            }
            else
            {
                reportIsSafe = false;
                break;
            }
        }

        return reportIsSafe;
    }

    private static bool CheckIfReportIsSafeWithDampeners(List<int> report)
    {
        var reportIsSafe = CheckIfReportIsSafe(report);
        if (reportIsSafe) return true;

        var increasing = report[0] < report[^1];

        for (int i = 0; i < report.Count - 1; i++)
        {
            var level = report[i];
            var nextLevel = report[i + 1];
            List<int> reportWithoutCurrent = [.. report[..i], .. report[(i + 1)..]];

            if (Math.Abs(level - nextLevel) <= 3)
            {
                if (increasing)
                {
                    if (level < nextLevel)
                    {
                        if (i > 1)
                        {
                            var prevLevel = report[i - 1];
                            reportIsSafe = prevLevel < level && Math.Abs(level - prevLevel) <= 3;

                            if (!reportIsSafe) break;
                        }
                        else
                        {
                            reportIsSafe = true;
                        }
                    }
                    else
                    {
                        reportIsSafe = CheckIfReportIsSafeWithDampeners(reportWithoutCurrent, increasing);
                        if (reportIsSafe)
                            break;
                    }

                }
                else
                {
                    if (level > nextLevel)
                    {
                        if (i > 1)
                        {
                            var prevLevel = report[i - 1];
                            reportIsSafe = prevLevel > level && Math.Abs(level - prevLevel) <= 3;

                            if (!reportIsSafe) break;
                        }
                        else
                        {
                            reportIsSafe = true;
                        }
                    }
                    else
                    {
                        reportIsSafe = CheckIfReportIsSafeWithDampeners(reportWithoutCurrent, increasing);
                        if (reportIsSafe)
                            break;
                    }
                }
            }
            else
            {
                reportIsSafe = CheckIfReportIsSafeWithDampeners(reportWithoutCurrent, increasing);
                if (reportIsSafe)
                    break;
            }
        }

        if (!reportIsSafe)
        {
            List<int> reportWithoutFirst = [.. report[1..]];
            reportIsSafe = CheckIfReportIsSafe(reportWithoutFirst);
        }
        if (!reportIsSafe)
        {
            List<int> reportWithoutLast = [.. report[..^1]];
            reportIsSafe = CheckIfReportIsSafe(reportWithoutLast);
        }

        return reportIsSafe;
    }

    private static List<List<int>> GetReports(string[] input)
    {
        var left = new List<List<int>>();

        foreach (var line in input)
        {
            var levelChars = line.Split(' ');
            var levels = new List<int>();

            foreach (var levelChar in levelChars)
            {
                var level = int.Parse(levelChar);
                levels.Add(level);
            }

            left.Add(levels);
        }

        return left;
    }
}
