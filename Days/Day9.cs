using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day9: BaseDay
{
    public override string PartOne()
    {
        var retval = 0;

        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day9.txt");
        foreach(var line in lines)
        {
            var nums = line.Split().Select(int.Parse).ToArray();
            retval += Extrapolate(nums);
        }
    
        return retval.ToString();
    }

    public override string PartTwo()
    {
        var retval = 0;

        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day9.txt");
        foreach(var line in lines)
        {
            var nums = line.Split().Select(int.Parse).ToArray();
            retval += ExtrapolatePrevious(nums);
        }

        return retval.ToString();
    }
    
    private int Extrapolate(int[] array)
    {
        if (array.All(x => x == 0))
            return 0;

        var deltas = new int[array.Length - 1];
        for (var i = 0; i < deltas.Length; i++)
            deltas[i] = array[i + 1] - array[i];

        var diff = Extrapolate(deltas);
        return array[array.Length-1] + diff;
    }
    
    private int ExtrapolatePrevious(int[] array)
    {
        if (array.All(x => x == 0))
            return 0;

        var deltas = new int[array.Length - 1];
        for (var i = 0; i < deltas.Length; i++)
            deltas[i] = array[i + 1] - array[i];

        var diff = ExtrapolatePrevious(deltas);
        return array[0] - diff;
    }
}