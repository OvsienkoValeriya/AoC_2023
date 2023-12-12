using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day5 : BaseDay
{
    public override string PartOne()
    {
        var orig = new List<long>();

        using (var sr = new StreamReader("inputs/day5_1.txt"))
        {
            var fileContent = sr.ReadToEnd().Trim();
            var blocks = fileContent.Split("\n\n");

            var maps = blocks.Select(block => block.Split(':')[1].Trim().Split('\n')).ToArray();

            if (maps[0].Length == 1)
            {
                orig = maps[0][0].Split(' ').Select(long.Parse).ToList();
            }

            var result = orig.Select(val => Follow(val, maps)).Min();
            return result.Item1.ToString();
        }
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("inputs/day5.txt");
        var inputs = lines[0].Split(':')[1].Split().Skip(1).ToArray();
        var seeds = new List<(long, long)>();

        for (var i = 0; i < inputs.Length; i += 2)
        {
            var start = long.Parse(inputs[i]);
            var length = long.Parse(inputs[i + 1]);
            seeds.Add((start, start + length));
        }

        var fileContent = File.ReadAllText("inputs/day5.txt");
        var blocks = fileContent.Split("\n\n");
        var maps = blocks.Select(block => block.Split(':')[1].Trim().Split('\n')).ToArray();

        foreach (var block in maps)
        {
            var ranges = new List<List<long>>();
            foreach (var line in block)
            {
                var range = line.Split().Select(long.Parse).ToList();
                ranges.Add(range);
                //Console.WriteLine($"range = {string.Join(",", range)}");
            }

            var newSeeds = new List<(long, long)>();

            while (seeds.Count > 0)
            {
                (long s, long e) = seeds[seeds.Count - 1];
                seeds.RemoveAt(seeds.Count - 1);
                bool added = false;
                foreach (var range in ranges)
                {
                    var a = range[0];
                    var b = range[1];
                    var c = range[2];
                    var os = Math.Max(s, b);
                    var oe = Math.Min(e, b + c);

                    if (os < oe)
                    {
                        newSeeds.Add((os - b + a, oe - b + a));

                        if (os > s)
                        {
                            seeds.Add((s, os));
                        }

                        if (e > oe)
                        {
                            seeds.Add((oe, e));
                        }

                        added = true;
                        break;
                    }
                }

                if (!added)
                {
                    newSeeds.Add((s, e));
                }
            }

            seeds = newSeeds;
            var result = seeds.Min(seed => seed.Item1);
        }

        return seeds.Min(seed => seed.Item1).ToString();
    }

    private static Tuple<long, List<int>> Follow(long val, IEnumerable<string[]> blocks)
    {
        var path = new List<int>();

        foreach (var block in blocks.Skip(1))
        {
            for (int i = 0; i < block.Length; i++)
            {
                var rg = block[i].Split().Select(long.Parse).ToArray();
                long dest = rg[0], src = rg[1], length = rg[2];

                if (src <= val && val < src + length)
                {
                    val = dest + (val - src);
                    path.Add(i);
                    break;
                }
            }
        }

        return Tuple.Create(val, path);
    }
}