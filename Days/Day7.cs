using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day7 : BaseDay
{
    static List<char> labels = new List<char> { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

    static List<char> labelsTwo = new List<char> { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

    public override string PartOne()
    {
        var rawLines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day7.txt");

        var playsDict = new Dictionary<string, int>();

        var lines = rawLines.Select(line =>
        {
            var parts = line.Split();
            return (parts[0], int.Parse(parts[1]));
        }).ToList();


        lines = lines.OrderBy(x => DetermineHandType(x.Item1))
            .ThenBy(x => x.Item1, new CustomStringComparer(labels))
            .ToList();

        var ans = 0;
        for (var i = 0; i < lines.Count; i++)
        {
            ans += (i + 1) * lines[i].Item2;
        }

        return ans.ToString();
    }

    public override string PartTwo()
    {
        var rawLines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day7.txt");

        var playsDict = new Dictionary<string, int>();

        var lines = rawLines.Select(line =>
        {
            var parts = line.Split();
            return (parts[0], int.Parse(parts[1]));
        }).ToList();


        lines = lines.OrderBy(x => DetermineHandTypeWithJoker(x.Item1))
            .ThenBy(x => x.Item1, new CustomStringComparer(labelsTwo))
            .ToList();

        var retval = 0;
        for (var i = 0; i < lines.Count; i++)
        {
            retval += (i + 1) * lines[i].Item2;
        }

        return retval.ToString();
    }

    private static int DetermineHandType(string hand)
    {
        var counts = new Dictionary<char, int>();

        foreach (var card in hand)
        {
            if (counts.ContainsKey(card))
                counts[card]++;
            else
                counts[card] = 1;
        }

        var uniqueCount = counts.Count;

        if (uniqueCount == 5)
        {
            return 1;
        }
        else if (uniqueCount == 4)
        {
            return 2;
        }
        else if (uniqueCount == 3)
        {
            var maxCount = counts.Values.Max();
            if (maxCount == 3)
                return 4;
            else
                return 3;
        }
        else if (uniqueCount == 2)
        {
            var maxCount = counts.Values.Max();
            if (maxCount == 4)
                return 6;
            else
                return 5;
        }

        return 7;
    }

    private static int DetermineHandTypeWithJoker(string hand)
    {
        var counts = new Dictionary<char, int>();
        var jokers = 0;

        foreach (var card in hand)
        {
            if (card == 'J')
            {
                jokers++;
            }
            else
            {
                counts[card] = counts.ContainsKey(card) ? counts[card] + 1 : 1;
            }
        }

        var amounts = counts.Values.OrderByDescending(value => value).ToList();

        if (jokers >= 5 || amounts.First() + jokers >= 5)
        {
            return 7;
        }

        if (jokers >= 4 || amounts.First() + jokers >= 4)
        {
            return 6;
        }

        if (amounts.First() + jokers >= 3)
        {
            var remJokers = amounts.First() + jokers - 3;
            if (amounts.Count >= 2 && amounts.Skip(1).First() + remJokers >= 2 || remJokers >= 2)
            {
                return 5;
            }

            return 4;
        }

        if (amounts.First() + jokers >= 2)
        {
            var remJokers = amounts.First() + jokers - 2;
            if (amounts.Count >= 2 && amounts.Skip(1).First() + remJokers >= 2 || remJokers >= 2)
            {
                return 3;
            }

            return 2;
        }

        return 1;
    }
}

public class CustomStringComparer : IComparer<string>
{
    private readonly List<char> _labels;

    public CustomStringComparer(List<char> labels)
    {
        _labels = labels;
    }

    public int Compare(string x, string y)
    {
        for (var i = 0; i < Math.Min(x.Length, y.Length); i++)
        {
            var indexX = _labels.IndexOf(x[i]);
            var indexY = _labels.IndexOf(y[i]);

            if (indexX < indexY)
            {
                return -1;
            }
            else if (indexX > indexY)
            {
                return 1;
            }
        }

        return x.Length.CompareTo(y.Length);
    }
}