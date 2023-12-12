using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day12 : BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("inputs/day12.txt");
        var retval = 0;
        foreach (var line in lines)
        {
            var pattern = line.Split(" ")[0];
            var comb = line.Split(" ")[1].Split(",").Select(str => int.Parse(str.Trim())).ToArray();
            retval += CombinationPossibleCount(pattern, comb);
            //Console.WriteLine($"{pattern} - {CombinationPossibleCount(pattern, comb)}");
        }

        return retval.ToString();
    }

    public override string PartTwo()
    {
        throw new System.NotImplementedException();
    }

    static int CombinationPossibleCount(string cfg, int[] nums)
    {
        if (string.IsNullOrEmpty(cfg))
        {
            return nums.Length == 0 ? 1 : 0;
        }

        if (nums.Length == 0)
        {
            return cfg.Contains("#") ? 0 : 1;
        }

        int result = 0;
        //Console.WriteLine($"{cfg} - result = {result}");

        if (cfg[0] == '.' || cfg[0] == '?')
        {
            if (cfg.Length > 1)
            {
                result += CombinationPossibleCount(cfg.Substring(1), nums);
            }
        }

        if (cfg[0] == '#' || cfg[0] == '?')
        {
            if (nums[0] <= cfg.Length && !cfg.Substring(0, nums[0]).Contains('.') &&
                (nums[0] == cfg.Length || cfg[nums[0]] != '#'))
            {
                if (cfg.Length > nums[0] + 1)
                {
                    result += CombinationPossibleCount(cfg.Substring(nums[0] + 1), nums[1..]);
                }
            }
        }

        return result;
    }
}