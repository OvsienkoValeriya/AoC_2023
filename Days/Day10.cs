using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day10: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("inputs/day10.txt");
        var startingRow = 0;
        var startingCol = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S')
                {
                    startingRow = i;
                    startingCol = j;
                    break;
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        var queue = new Queue<(int, int)>();
            var loop = new HashSet<(int, int)>{(startingRow, startingCol)};
            queue.Enqueue((startingRow, startingCol));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var row = current.Item1;
                var col = current.Item2;
                var ch = lines[row][col];

                if (row > 0 && "S|JL".Contains(ch) && "|7F".Contains(lines[row - 1][col]) && !loop.Contains((row - 1, col)))
                {
                    loop.Add((row - 1, col));
                    queue.Enqueue((row - 1, col));
                }

                if (row < lines.Length - 1 && "S|7F".Contains(ch) && "|JL".Contains(lines[row + 1][col]) && !loop.Contains((row + 1, col)))
                {
                    loop.Add((row + 1, col));
                    queue.Enqueue((row + 1, col));
                }

                if (col > 0 && "S-J7".Contains(ch) && "-LF".Contains(lines[row][col - 1]) && !loop.Contains((row, col - 1)))
                {
                    loop.Add((row, col - 1));
                    queue.Enqueue((row, col - 1));
                }

                if (col < lines[row].Length - 1 && "S-LF".Contains(ch) && "-J7".Contains(lines[row][col + 1]) && !loop.Contains((row, col + 1)))
                {
                    loop.Add((row, col + 1));
                    queue.Enqueue((row, col + 1));
                }
            }
        

        return (loop.Count / 2).ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("inputs/day10.txt");
        var startingRow = 0;
        var startingCol = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S')
                {
                    startingRow = i;
                    startingCol = j;
                    break;
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        var queue = new Queue<(int, int)>();
        var loop = new HashSet<(int, int)>{(startingRow, startingCol)};
        var maybe_s = new HashSet<char> { '|', '-', 'J', 'L', '7', 'F' };
        queue.Enqueue((startingRow, startingCol));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var row = current.Item1;
            var col = current.Item2;
            var ch = lines[row][col];

            if (row > 0 && "S|JL".Contains(ch) && "|7F".Contains(lines[row - 1][col]) && !loop.Contains((row - 1, col)))
            {
                loop.Add((row - 1, col));
                queue.Enqueue((row - 1, col));
                if (ch == 'S')
                    maybe_s.IntersectWith(new[] { '|', 'J', 'L' });
            }

            if (row < lines.Length - 1 && "S|7F".Contains(ch) && "|JL".Contains(lines[row + 1][col]) &&
                !loop.Contains((row + 1, col)))
            {
                loop.Add((row + 1, col));
                queue.Enqueue((row + 1, col));
                if (ch == 'S')
                    maybe_s.IntersectWith(new[] { '|', '7', 'F' });
            }

            if (col > 0 && "S-J7".Contains(ch) && "-LF".Contains(lines[row][col - 1]) && !loop.Contains((row, col - 1)))
            {
                loop.Add((row, col - 1));
                queue.Enqueue((row, col - 1));
                if (ch == 'S')
                    maybe_s.IntersectWith(new[] { '-', 'L', 'F' });
            }

            if (col < lines[row].Length - 1 && "S-LF".Contains(ch) && "-J7".Contains(lines[row][col + 1]) &&
                !loop.Contains((row, col + 1)))
            {
                loop.Add((row, col + 1));
                queue.Enqueue((row, col + 1));
                if (ch == 'S')
                    maybe_s.IntersectWith(new[] { '-', 'L', 'F' });
            }
        }
        
        var newS = maybe_s.Single();
        lines = lines.Select(row => row.Replace('S', newS)).ToArray();
        lines = lines.Select((row, r) => string.Concat(row.Select((ch, c) => loop.Contains((r, c)) ? ch : '.'))).ToArray();

        var outside = new HashSet<(int, int)>();

        for (var r = 0; r < lines.Length; r++)
        {
            bool within = false;
            bool? up = null;

            for (var c = 0; c < lines[r].Length; c++)
            {
                var ch = lines[r][c];

                if (ch == '|')
                {
                    up = null;
                    within = !within;
                }
                else if (ch == '-')
                {
                    if (up != null)
                        up = true;
                }
                else if (ch == 'L' || ch == 'F')
                {
                    up = null;
                    up = (ch == 'L');
                }
                else if (ch == '7' || ch == 'J')
                {
                    if (up != null && ch != (up.Value ? 'J' : '7'))
                        within = !within;
                    up = null;
                }
                
                if (!within)
                {
                    outside.Add((r, c));
                }
            }
        }

        var result = lines.Length * lines[0].Length - outside.Union(loop).Count();
        

        return result.ToString();
    }
    
    
}