using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day18: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day18.txt");
        var commands = new List<string>();
        foreach(var line in lines)
        {
            var split = line.Split(" ");
            var command = split[0]+" "+split[1];
            commands.Add(command);
        }

        var points = new List<(long, long)>(){(0,0)};

        var b = 0;
        foreach(var command in commands)
        {
            var dir = command.Split(" ")[0];
            var steps = int.Parse(command.Split(" ")[1]);
            b+=steps;

            var (r, c) = points[points.Count-1]; 
      
            if (dir == "R")
            {
                points.Add((r + 0 * steps, c + 1 * steps));
            }
            else if (dir == "D")
            {
                points.Add((r + 1 * steps, c + 0 * steps));
            }
            else if (dir == "L")
            {
                points.Add((r + 0 * steps, c -1 * steps));
            }
            else if (dir == "U")
            {
                points.Add((r -1 * steps, c + 0 * steps));
            }
        
        }

        var calc = Math.Abs(CalculatePolygonArea(points));
        var retval = calc - b / 2 + 1;
    
        return (retval+b).ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day18.txt");
        var commands = new List<string>();
        foreach(var line in lines)
        {
            var split = line.Split(" ");
            var command = split[2].Substring(2, split[2].Length - 3);
            commands.Add(command);
        }
        var points = new List<(long, long)>(){(0,0)};

        long b = 0;
        foreach(var command in commands)
        {
            var ch = command[command.Length-1];
            var dir = int.Parse(ch.ToString());
            var hex = command.Substring(0, command.Length - 1);
            var steps = Convert.ToInt64(hex, 16);
            b+=steps;

            var (r, c) = points[points.Count-1]; 

            if (dir == 0)
            {
                points.Add((r + 0 * steps, c + 1 * steps));
            }
            else if (dir == 1)
            {
                points.Add((r + 1 * steps, c + 0 * steps));
            }
            else if (dir == 2)
            {
                points.Add((r + 0 * steps, c -1 * steps));
            }
            else if (dir == 3)
            {
                points.Add((r -1 * steps, c + 0 * steps));
            }

        }

        long calc = Math.Abs(CalculatePolygonArea(points));
        long retval = calc - b / 2 + 1;

        return (retval+b).ToString();
    }
    private long CalculatePolygonArea(List<(long, long)> points)
    {
        var n = points.Count;
        long area = 0;

        for (var i = 0; i < n; i++)
        {
            var j = (i + 1) % n;
            area += (points[i].Item1 * points[j].Item2 - points[j].Item1 * points[i].Item2);
        }

        return area / 2;
    }
}