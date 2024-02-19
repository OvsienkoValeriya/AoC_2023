using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day8: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllText("inputs/day8.txt").Split("\n");

        var instructions = lines[0].Trim();
        var data = new Dictionary<string, (string, string)>();
        for (var i = 2; i < lines.Length; i++)
        {
            var parts = lines[i].Split('=');
            var key = parts[0].Trim();
            var values = parts[1].Trim(' ', '(', ')').Split(',').Select(v => v.Trim()).ToArray();
            data[key] = (values[0], values[1]);
        }

        var curr = "AAA";
        var stepsCount = 0;
    
        while (curr != "ZZZ")
        {
            stepsCount++;
            curr = instructions[0] == 'L' ? data[curr].Item1 : data[curr].Item2;
            instructions = instructions.Substring(1) + instructions[0];
        }

       
        return stepsCount.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllText("inputs/day8.txt").Split("\n");

        var instructions = lines[0].Trim();
        var data = new Dictionary<string, (string, string)>();
        for (var i = 2; i < lines.Length; i++)
        {
            var parts = lines[i].Split('=');
            var key = parts[0].Trim();
            var values = parts[1].Trim(' ', '(', ')').Split(',').Select(v => v.Trim()).ToArray();
            data[key] = (values[0], values[1]);
        }

        var steps = 0;
        var cycleSizes = new List<ulong>();

        var startingNodes = data.Keys.Where(k => k.EndsWith('A')).ToList();
        foreach (var startingNode in startingNodes)
        {
            steps = 0;
            var currentNode = startingNode;
            var currentInstruction = 0;
            while (!currentNode.EndsWith('Z'))
            {
                var instruction = instructions[currentInstruction];
                steps++;

                var (left, right) = data[currentNode];
                currentNode = instruction == 'L' ? left : right;

                currentInstruction++;
                currentInstruction %= instructions.Length;
            }

            cycleSizes.Add((ulong)steps);
        }

        var lcm = cycleSizes[0];
        for (var i = 1; i < cycleSizes.Count; i++)
        {
            lcm = (lcm * cycleSizes[i]) / GCD(lcm, cycleSizes[i]);
        }

        return lcm.ToString();
    }
    
    private ulong GCD(ulong a, ulong b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}