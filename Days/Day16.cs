using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day16: BaseDay
{
    public override string PartOne()
    {
        var grid = File.ReadAllLines("inputs/day16.txt");

        return Calculate(grid, 0, -1, 0, 1).ToString();
    }

    public override string PartTwo()
    {
        var grid = File.ReadAllLines("inputs/day16.txt");

        var maxVal = 0;

        for (var r = 0; r < grid.Length; r++)
        {
            maxVal = Math.Max(maxVal, Calculate(grid, r, -1, 0, 1));
            maxVal = Math.Max(maxVal, Calculate(grid, r, grid[0].Length, 0, -1));
        }

        for (var c = 0; c < grid[0].Length; c++)
        {
            maxVal = Math.Max(maxVal, Calculate(grid, -1, c, 1, 0));
            maxVal = Math.Max(maxVal, Calculate(grid, grid.Length, c, -1, 0));
        }

        return maxVal.ToString();
    }
    
    private int Calculate(string[] grid, int r, int c, int delR, int delC)
    {
        var a = new List<(int, int, int, int)> {(r, c, delR, delC)};
        var queue = new Queue<(int, int, int, int)>(a);
        var seen = new HashSet<(int, int, int, int)>();

        while(queue.Count > 0)
        {
            (var row, var col, var deltaR, var deltaC) = queue.Dequeue();
            row += deltaR;
            col += deltaC;

            if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length)
                continue;

            var curr = grid[row][col];
            if (curr == '.' || (curr == '-' && deltaC != 0) || (curr == '|' && deltaR != 0))
            {
                var state = (row, col, deltaR, deltaC);
                if (!seen.Contains(state))
                {
                    seen.Add(state);
                    queue.Enqueue(state);
                }
            }
            else if (curr == '/')
            {
                (deltaR, deltaC) = (-deltaC, -deltaR);
                var state = (row, col, deltaR, deltaC);
                if (!seen.Contains(state))
                {
                    seen.Add(state);
                    queue.Enqueue(state);
                }
            }
            else if (curr == '\\')
            {
                (deltaR, deltaC) = (deltaC, deltaR);
                var state = (row, col, deltaR, deltaC);
                if (!seen.Contains(state))
                {
                    seen.Add(state);
                    queue.Enqueue(state);
                }
            }
            else
            {
                foreach ((var newDr, var newDc) in curr == '|' ? new[] {(1, 0), (-1, 0)} : new[] {(0, 1), (0, -1)})
                {
                    var state = (row, col, newDr, newDc);
                    if (!seen.Contains(state))
                    {
                        seen.Add(state);
                        queue.Enqueue(state);
                    }
                }
            }

      
        }
    
        var coords = new HashSet<(int, int)>(seen.Select(tuple => (tuple.Item1, tuple.Item2)));

        return coords.Count;
    }
}