using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day3: BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day3.txt");
        var retval = 0;

        for(var i = 0; i < lines.Length; i++)
        {
            var d = "";
      
            var isNeeded = false;
            for(var j = 0; j < lines[i].Length; j++)
            {
                if(char.IsDigit(lines[i][j]))
                {
                    d += lines[i][j];
          
                    isNeeded = isNeeded || CheckNeighbours(lines, i, j);
                }
                else if(d != "")
                {
                    if(isNeeded)
                    {
                        retval += int.Parse(d);
                    }
                    d = "";
                    isNeeded = false;
                }        
            }
            if(d != "" && isNeeded)
            {
                retval += int.Parse(d);
            }
        }
        return retval.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day3.txt");
            var retval = 0;
            var coords = new Dictionary<(int, int), MyNumber>();
            
            for(var i = 0; i < lines.Length; i++)
            {
              var d = "";
              var myNumber = new MyNumber();
              for(var j = 0; j < lines[i].Length; j++)
              {        
                if(char.IsDigit(lines[i][j]))
                {
                  d+=lines[i][j];
                  coords[(i, j)] = myNumber;
                  myNumber.Coords.Add((i, j));
                }
                else if(d != "")
                {
                  myNumber.Value = int.Parse(d);
                  d = "";
                  myNumber = new MyNumber();
                }
              }
              if(d != "")
              {
                myNumber.Value = int.Parse(d);
              }
            }
        
            (int, int)[] directions = {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1),           (0, 1),
                (1, -1), (1, 0), (1, 1)
            };
            
            for(var i = 0; i < lines.Length; i++)
            {
              for(var j = 0; j < lines[i].Length; j++)
              {
                var c = lines[i][j]; 
        
                if (c == '*')
                {
                    
                    var nums = new HashSet<MyNumber>();
                    foreach ((var di, var dj) in directions)
                    {
                        var nx = j + dj;
                        var ny = i + di;
                        
                        if (coords.TryGetValue((ny, nx), out MyNumber v))
                        {
                            nums.Add(v);
                        }
                        
                    }
                    if (nums.Count == 2)
                    {
                        var a = nums.ToList()[0].Value;
                        var b = nums.ToList()[1].Value;
                        retval += a * b;
                    }
                }
        
              }
            }
            
            return retval.ToString();
    }
    
    private bool CheckNeighbours(string[] lines, int i, int j)
    {
        return (j < lines[i].Length-1 && lines[i][j+1] != '.' && !char.IsDigit(lines[i][j+1]) ||
                j> 0 && lines[i][j-1] != '.' && !char.IsDigit(lines[i][j-1])||
                i> 0 && lines[i-1][j] != '.' && !char.IsDigit(lines[i-1][j]) ||
                i < lines.Length-1 && lines[i+1][j] != '.' && !char.IsDigit(lines[i+1][j]) ||
                j< lines[i].Length-1 && i> 0 && lines[i-1][j+1]!= '.' && !char.IsDigit(lines[i-1][j+1])||
                i < lines.Length-1 && j< lines[i].Length-1 && lines[i+1][j+1]!= '.' && !char.IsDigit(lines[i+1][j+1]) ||
                i > 0 && j > 0 && lines[i-1][j-1]!= '.' && !char.IsDigit(lines[i-1][j-1]) ||
                i < lines.Length-1 && j > 0 && lines[i+1][j-1]!= '.' && !char.IsDigit(lines[i+1][j-1]));
    }
}

public class MyNumber
{
    public List<(int, int)> Coords {get;set;} = new List<(int, int)>();

    public int Value {get;set;}
  
} 