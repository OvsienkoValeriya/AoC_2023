using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day12 : BaseDay
{
    public override string PartOne()
    {
        long retval = 0;

        var lines = File.ReadLines("inputs/day12.txt");

        foreach (var line in lines)
        {
            var pattern = line.Split(" ")[0];
            var comb = line.Split(" ")[1].Split(",").Select(str => int.Parse(str.Trim())).ToArray();
            retval += CombinationPossibleCount(pattern, comb);
        }

        return retval.ToString();
    }

    public override string PartTwo()
    {
        long retval = 0;

        var lines = File.ReadLines("inputs/day12.txt");

        foreach (var line in lines)
        {
            var pattern = line.Split(" ")[0];
            pattern = string.Join("?", Enumerable.Repeat(pattern, 5));
            var comb = line.Split(" ")[1].Split(",").Select(str => int.Parse(str.Trim())).ToArray();
            var combs = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                combs.AddRange(comb);
            }
            retval += CombinationPossibleCount(pattern, combs.ToArray());
        
        }

        return retval.ToString();
    }

   private long CombinationPossibleCount(string pattern, int[] groups)
  {
      var arrangements = new long[pattern.Length + 1, groups.Length + 1];
      arrangements[0, 0] = 1;

      for (var patternLength = 1; patternLength <= pattern.Length; patternLength++)
      {
          var patternIndex = patternLength - 1;
          for (var groupCount = 0; groupCount <= groups.Length; groupCount++)
          {
              var character = pattern[patternIndex];
              if (character == '.' || character == '?')
              {
                  arrangements[patternLength, groupCount] += arrangements[patternLength - 1, groupCount];
              }

              if (character == '#' || character == '?')
              {
                  if (groupCount == 0)
                  {
                      continue;
                  }

                  var groupSize = groups[groupCount - 1];
                  if (patternLength < groupSize)
                  {
                      continue;
                  }

                  var canPlaceGroup = true;
                  for (var endIndex = patternIndex; endIndex >= patternIndex - groupSize + 1; endIndex--)
                  {
                      if (pattern[endIndex] == '.')
                      {
                          canPlaceGroup = false;
                          break;
                      }
                  }

                  if (patternIndex - groupSize >= 0 && pattern[patternIndex - groupSize] == '#')
                  {
                      canPlaceGroup = false;
                  }

                  if (canPlaceGroup)
                  {
                      if (patternLength == groupSize)
                      {
                          if (groupCount == 1)
                          {
                              arrangements[patternLength, groupCount] += 1;
                          }
                      }
                      else
                      {
                          arrangements[patternLength, groupCount] +=
                              arrangements[patternLength - groupSize - 1, groupCount - 1];
                      }
                  }
              }
          }
      }

      return arrangements[pattern.Length, groups.Length];

    }
}