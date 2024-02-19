using System;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day6 : BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day6.txt");

        var timeLine = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        var distanceLine = lines[1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        var time = timeLine.Skip(1).Select(int.Parse).ToList();
        var distance = distanceLine.Skip(1).Select(int.Parse).ToList();

        var retval = 1;
        for (var i = 0; i < time.Count; i++)
        {
            var dist = distance[i];
            var winningRacesCount = 0;
            for (var j = 0; j < time[i] + 1; j++)
            {
                var possibleTime = (time[i] - j) * j;
                if (possibleTime > dist)
                {
                    winningRacesCount += 1;
                }
            }

            retval *= winningRacesCount;
        }

        return retval.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day6.txt");

        var time = long.Parse(string.Join("", lines[0].Where(char.IsDigit)));
        var distance = long.Parse(string.Join("", lines[1].Where(char.IsDigit)));

        var winningRacesCount = 0;
        for (var j = 0; j < time + 1; j++)
        {
            var possibleTime = (time - j) * j;
            if (possibleTime > distance)
            {
                winningRacesCount += 1;
            }
        }

        return winningRacesCount.ToString();
    }
}