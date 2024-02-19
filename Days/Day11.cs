using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day11 : BaseDay
{
    public override string PartOne()
    {
        var grid = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day11.txt");

        var emptyRows = Enumerable.Range(0, grid.Length)
            .Where(r => grid[r].All(ch => ch == '.'))
            .ToList();

        var emptyCols = Enumerable.Range(0, grid[0].Length)
            .Where(c => grid.All(row => row[c] == '.'))
            .ToList();

        var points = new List<(int, int)>();
        for (var r = 0; r < grid.Length; r++)
        {
            for (var c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == '#')
                {
                    points.Add((r, c));
                }
            }
        }

        var combinations = points.SelectMany((p, i) => points.Skip(i + 1), (p, q) => (p, q));

        var total = combinations.Sum(pair =>
        {
            long sum = Math.Abs(pair.p.Item1 - pair.q.Item1) +
                       Math.Abs(pair.p.Item2 - pair.q.Item2);

            sum += emptyRows.Intersect(Enumerable.Range(Math.Min(pair.p.Item1, pair.q.Item1),
                Math.Abs(pair.p.Item1 - pair.q.Item1) + 1)).Count();
            sum += emptyCols.Intersect(Enumerable.Range(Math.Min(pair.p.Item2, pair.q.Item2),
                Math.Abs(pair.p.Item2 - pair.q.Item2) + 1)).Count();

            return sum;
        });

        return total.ToString();
    }

    public override string PartTwo()
    {
        var grid = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day11.txt");

        var emptyRows = Enumerable.Range(0, grid.Length)
            .Where(r => grid[r].All(ch => ch == '.'))
            .ToList();

        var emptyCols = Enumerable.Range(0, grid[0].Length)
            .Where(c => grid.All(row => row[c] == '.'))
            .ToList();

        var points = new List<(int, int)>();
        for (var r = 0; r < grid.Length; r++)
        {
            for (var c = 0; c < grid[0].Length; c++)
            {
                if (grid[r][c] == '#')
                {
                    points.Add((r, c));
                }
            }
        }

        var combinations = points.SelectMany((p, i) => points.Skip(i + 1), (p, q) => (p, q));

        var total = combinations.Sum(pair =>
        {
            long sum = Math.Abs(pair.p.Item1 - pair.q.Item1) +
                       Math.Abs(pair.p.Item2 - pair.q.Item2);

            sum += 999999L * emptyRows.Intersect(Enumerable.Range(Math.Min(pair.p.Item1, pair.q.Item1),
                Math.Abs(pair.p.Item1 - pair.q.Item1) + 1)).Count();
            sum += 999999L * emptyCols.Intersect(Enumerable.Range(Math.Min(pair.p.Item2, pair.q.Item2),
                Math.Abs(pair.p.Item2 - pair.q.Item2) + 1)).Count();

            return sum;
        });

        return total.ToString();
    }
}