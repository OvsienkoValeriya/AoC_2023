using System;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day13 : BaseDay
{
    public override string PartOne()
    {
        var retval = 0;
        foreach (var block in File.ReadAllText("inputs/day13.txt").Split("\n\n")) 
        {
            var grid = block.Split('\n');
            var row = FindMirrorRow(grid);
            retval += row * 100;

            var col = FindMirrorRow(TransposeArray(grid));
            retval += col;
        }
   
        return retval.ToString();
    }

    public override string PartTwo()
    {
        var retval = 0;
        foreach (var block in File.ReadAllText("inputs/day13.txt").Split("\n\n")) 
        {
            var grid = block.Split('\n');
            var row = FindMirrorRowWithSmudge(grid);
            retval += row * 100;

            var col = FindMirrorRowWithSmudge(TransposeArray(grid));
            retval += col;
        }

        return retval.ToString();
    }
    
    private static int FindMirrorRow(string[] grid)
    {
        for (var r = 1; r < grid.Length; r++)
        {
            var above = grid.Take(r).Reverse().ToArray();
            var below = grid.Skip(r).ToArray();

            above = above.Take(below.Length).ToArray();
            below = below.Take(above.Length).ToArray();

            if (above.SequenceEqual(below))
            {
                return r;
            }
        }

        return 0;
    }
    private static int FindMirrorRowWithSmudge(string[] grid)
    {
        for (var r = 1; r < grid.Length; r++)
        {
            var above = grid.Take(r).Reverse().ToArray();
            var below = grid.Skip(r).ToArray();

            var differences = 0;

            for (var i = 0; i < Math.Min(above.Length, below.Length); i++)
            {
                var rowAbove = above[i];
                var rowBelow = below[i];

                for (var j = 0; j < Math.Min(rowAbove.Length, rowBelow.Length); j++)
                {
                    var charAbove = rowAbove[j];
                    var charBelow = rowBelow[j];

                    differences += (charAbove == charBelow) ? 0 : 1;
                }
            }

            if (differences == 1)
            {
                return r;
            }
        }

        return 0;
    }

    private static string[] TransposeArray(string[] array)
    {
        var rows = array.Length;
        var cols = array[0].Length;

        var transposedArray = new string[cols];

        for (var i = 0; i < cols; i++)
        {
            var column = new char[rows];
            for (var j = 0; j < rows; j++)
            {
                column[j] = array[j][i];
            }
            transposedArray[i] = new string(column);
        }

        return transposedArray;
    }
}