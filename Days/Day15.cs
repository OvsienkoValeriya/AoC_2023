using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day15: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllText("day15.txt").Split(",");
        var retval = 0;
        foreach(var line in lines)
        {
            var curr = 0;
            foreach(var ch in line)
            {
                var code = Convert.ToByte(ch);
                curr+=code;
                curr*=17;
                curr%=256;
            }
            retval+=curr;
        }
        return retval.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllText("day15.txt").Split(",");
        var focal_lengths = new Dictionary<string, int>();
        var boxes = new List<string>[256];
        foreach(var line in lines)
        {      
            if (line.Contains("-"))
            {
                var label = line.Substring(0, line.Length - 1);
                var index = CalculateHash(label);

                if (boxes[index] != null && boxes[index].Contains(label))
                {
                    boxes[index].Remove(label);
                }
            }
            else
            {
                var parts = line.Split('=');
                var label = parts[0];
                var length = int.Parse(parts[1]);

                var index = CalculateHash(label);

                if (boxes[index] == null)
                {
                    boxes[index] = new List<string>();
                }

                if (!boxes[index].Contains(label))
                {
                    boxes[index].Add(label);
                }

                focal_lengths[label] = length;
            }
        }
    
        var retval = 0;
        for (var boxNumber = 0; boxNumber < boxes.Length; boxNumber++)
        {
            if (boxes[boxNumber] != null)
            {
                for (var lensSlot = 0; lensSlot < boxes[boxNumber].Count; lensSlot++)
                {
                    var label = boxes[boxNumber][lensSlot];
                    retval += (boxNumber + 1) * (lensSlot + 1) * focal_lengths[label];
                }
            }
        }

        return retval.ToString();
    }
    
    private static int CalculateHash(string line)
    {
        var curr = 0;
        foreach(var ch in line)
        {
            var code = Convert.ToByte(ch);
            curr+=code;
            curr*=17;
            curr%=256;
        }

        return curr;
    }
}