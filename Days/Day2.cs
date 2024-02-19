using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days
{
    public class Day2 : BaseDay
    {
        public override string PartOne()
        {
            Func<string[], int, int> partOneCompute = (sets, gameNum) =>
            {
                var validGames = new List<int>();
                foreach (var s in sets)
                {
                    var isValidGame = IsGameValid(s, 12, 13, 14);
                    if (isValidGame)
                    {
                        validGames.Add(gameNum);
                    }
                }

                return validGames.Count == sets.Length ? gameNum : 0;
            };

            return Compute("/Users/valeria/RiderProjects/AoC_2023/inputs/day2.txt", partOneCompute).ToString();
        }

        public override string PartTwo()
        {
            Func<string[], int, int> partTwoCompute = (sets, gameNum) =>
            {
                var maxColorCount = new Dictionary<string, int>();
                foreach (var s in sets)
                {
                    UpdateColorCounts(s, maxColorCount);
                }

                return maxColorCount["red"] * maxColorCount["green"] * maxColorCount["blue"];
            };
            return Compute("/Users/valeria/RiderProjects/AoC_2023/inputs/day2.txt", partTwoCompute).ToString();
        }

        private int Compute(string path, Func<string[], int, int> compute)
        {
            var lines = File.ReadAllLines(path);
            var pattern = @"Game (\d+): (.*$)";
            var retval = 0;
            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern);
                var gameNum = int.Parse(match.Groups[1].Value);
                var data = match.Groups[2].Value;
                var sets = data.Split(new[] { ';' });
                retval += compute(sets, gameNum);
            }

            return retval;
        }

        private bool IsGameValid(string s, int redMax, int greenMax, int blueMax)
        {
            var redCount = GetColorCount(s, "red");
            var greenCount = GetColorCount(s, "green");
            var blueCount = GetColorCount(s, "blue");
            return (redCount <= redMax && greenCount <= greenMax && blueCount <= blueMax);
        }

        private int GetColorCount(string setData, string color)
        {
            var pattern = $@"(\d+) {color}";
            var match = Regex.Match(setData, pattern);
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }

        private void UpdateColorCounts(string s, IDictionary<string, int> maxColorCount)
        {
            var colors = new[] { "red", "green", "blue" };
            foreach (var color in colors)
            {
                var currentCount = GetColorCount(s, color);
                if (!maxColorCount.TryAdd(color, currentCount))
                {
                    if (maxColorCount[color] < currentCount)
                    {
                        maxColorCount[color] = currentCount;
                    }
                }
            }
        }
    }
}