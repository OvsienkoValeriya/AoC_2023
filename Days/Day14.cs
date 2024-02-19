using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day14: BaseDay
{
    public override string PartOne()
    {
        var retval = 0;

        var lines = File.ReadAllLines("inputs/day14.txt");
        var transposed = TransposeArray(lines);

        var newArr = new List<string>();
        foreach(var row in transposed)
        {
            var r = string.Join('#', row.Split('#')
                .Select(group => new string(group.OrderByDescending(c => c).ToArray())));
            newArr.Add(r);
        }

        var untransposed = ReverseTransposeArray(newArr.ToArray()).Reverse().ToArray();
        for(var i = 0; i < untransposed.Length; i++)
        {
            var zeroCounter = 0;
            foreach(var ch in untransposed[i])
            {
                if(ch == 'O')
                {
                    zeroCounter++;
                }
            }

            retval += zeroCounter * (i+1);
      
        }
    
        return retval.ToString();
    }

    public override string PartTwo()
    {
        var grid = File.ReadAllLines("inputs/day14.txt");
        var seen = new Dictionary<string, int>();
        var loads = new List<int> { 0 };

        for (var c = 1; c < 1000000000; c++)
        {
            grid = TiltNorth(grid);
            grid = TiltWest(grid);
            grid = TiltSouth(grid);
            grid = TiltEast(grid);

            var pl = grid.Select((row, y) => (grid.Length - y) * row.Count(c => c == 'O')).Sum();
            loads.Add(pl);

            var state = string.Concat(grid.Select(row => new string(row.ToArray())));;
            if (seen.TryGetValue(state, out int seenC))
            {
                var lam = c - seenC;
                var mu = seenC;

                var times = mu + (1000000000 - mu) % lam;
                return loads[times].ToString();
            }

            seen[state] = c;
        }

    
        return "";
    }
    
    static string[] TiltNorth(string[] platform)
  {
      var charPlatform = platform.Select(row => row.ToCharArray()).ToArray();

      for (var x = 0; x < charPlatform[0].Length; x++)
      {
          var dy = 0;
          for (var y = 0; y < charPlatform.Length; y++)
          {
              var c = charPlatform[y][x];
              if (c == '.')
              {
                  dy++;
              }
              else if (c == '#')
              {
                  dy = 0;
              }
              else if (c == 'O')
              {
                  charPlatform[y][x] = '.';
                  charPlatform[y - dy][x] = 'O';
              }
          }
      }

      return charPlatform.Select(row => new string(row)).ToArray();
  }

  static string[] TiltSouth(string[] platform)
  {
      var charPlatform = platform.Select(row => row.ToCharArray()).ToArray();

      for (var x = 0; x < charPlatform[0].Length; x++)
      {
          var dy = 0;
          for (var y = charPlatform.Length - 1; y >= 0; y--)
          {
              var c = charPlatform[y][x];
              if (c == '.')
              {
                  dy++;
              }
              else if (c == '#')
              {
                  dy = 0;
              }
              else if (c == 'O')
              {
                  charPlatform[y][x] = '.';
                  charPlatform[y + dy][x] = 'O';
              }
          }
      }

      return charPlatform.Select(row => new string(row)).ToArray();
  }

  static string[] TiltWest(string[] platform)
  {
      char[][] charPlatform = platform.Select(row => row.ToCharArray()).ToArray();

      for (int y = 0; y < charPlatform.Length; y++)
      {
          int dx = 0;
          for (int x = 0; x < charPlatform[0].Length; x++)
          {
              char c = charPlatform[y][x];
              if (c == '.')
              {
                  dx++;
              }
              else if (c == '#')
              {
                  dx = 0;
              }
              else if (c == 'O')
              {
                  charPlatform[y][x] = '.';
                  charPlatform[y][x - dx] = 'O';
              }
          }
      }

      return charPlatform.Select(row => new string(row)).ToArray();
  }

  private string[] TiltEast(string[] platform)
  {
      var charPlatform = platform.Select(row => row.ToCharArray()).ToArray();

      for (var y = 0; y < charPlatform.Length; y++)
      {
          var dx = 0;
          for (var x = charPlatform[0].Length - 1; x >= 0; x--)
          {
              var c = charPlatform[y][x];
              if (c == '.')
              {
                  dx++;
              }
              else if (c == '#')
              {
                  dx = 0;
              }
              else if (c == 'O')
              {
                  charPlatform[y][x] = '.';
                  charPlatform[y][x + dx] = 'O';
              }
          }
      }

      return charPlatform.Select(row => new string(row)).ToArray();
  }
  
  private string[] TransposeArray(string[] array)
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
  
  private string[] ReverseTransposeArray(string[] array)
  {
      var rows = array.Length;
      var cols = array[0].Length;

      var reverseTransposedArray = new string[rows];

      for (var i = 0; i < rows; i++)
      {
          var row = new char[cols];
          for (var j = 0; j < cols; j++)
          {
              row[j] = array[j][i];
          }
          reverseTransposedArray[i] = new string(row);
      }

      return reverseTransposedArray;
  }
}