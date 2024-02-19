using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;
public class Day1: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("inputs/day1.txt");
        var sum = 0;
        foreach (var line in lines)
        {
            sum += GetStringValue(line);
        }
        return sum.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("inputs/day1.txt");
        var sum = 0;
        foreach (var line in lines)
        {
            var digits = new List<int>();

            for (var j = 0; j <= line.Length - 1; j++)
            {
                var substring = line.Substring(j);
                
                if (char.IsDigit(line[j]))
                {
                    digits.Add(int.Parse(line[j].ToString()));
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

            var lineSum = int.Parse(digits[0] + digits[^1].ToString());
            sum += lineSum;
        }
        return sum.ToString();
    }
    
    private static int GetStringValue(string line)
    {
        var integer = new StringBuilder();

        foreach (var ch in line.Where(ch => char.IsDigit(ch)))
        {
            integer.Append(ch);
            break;
        }

        for (var j = line.Length - 1; j >= 0; j--)
        {
            if (char.IsDigit(line[j]))
            {
                integer.Append(line[j]);
                break;
            }
        }

        return int.Parse(integer.ToString());
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

        foreach (var kv in numbersDictionary.Where(entry => word.StartsWith(entry.Key)))
        {
            return kv.Value;
        }

        return null;
    }
}