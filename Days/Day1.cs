using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;
public class Day1: BaseDay
{
    public override string PartOne()
    {
        var lines = FetchInput();
        var sum = 0;
        foreach (var line in lines)
        {
            sum += GetStringValue(line);
        }
        return sum.ToString();
    }

    public override string PartTwo()
    {
        var lines = FetchInput();
        var sum = 0;
        foreach (var line in lines)
        {
            var digits = new List<int>();

            for (var j = 0; j <= line.Length - 1; j++)
            {
                var substring = line.Substring(j);
                
                if (Char.IsDigit(line[j]))
                {
                    digits.Add(Int32.Parse(line[j].ToString()));
                }
                else
                {
                    var number = ParseWordToNumber(substring);
                    if (number.HasValue)
                    {
                        digits.Add(number.Value);
                        j += number.Value.ToString().Length - 1;
                    }
                }
            }

            var lineSum = Int32.Parse(digits[0] + digits[^1].ToString());
            sum += lineSum;
        }
        return sum.ToString();
    }

    private static string[] FetchInput()
    {
        return File.ReadAllLines("inputs/day1.txt");
    }

    private static int GetStringValue(string line)
    {
        var integer = new StringBuilder();

        for (var i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                integer.Append(line[i]);
                break;
            }
        }

        for (var j = line.Length - 1; j >= 0; j--)
        {
            if (Char.IsDigit(line[j]))
            {
                integer.Append(line[j]);
                break;
            }
        }

        return Int32.Parse(integer.ToString());
    }

    private static int? ParseWordToNumber(string word)
    {
        var numbersDictionary = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };

        foreach (var entry in numbersDictionary)
        {
            if (word.StartsWith(entry.Key))
            {
                return entry.Value;
            }
        }

        return null;
    }
}