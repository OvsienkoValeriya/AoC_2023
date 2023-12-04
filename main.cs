using System;
using System.IO; 
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class MyNumber
{
  public List<(int, int)> Coords {get;set;} = new List<(int, int)>();

  public int Value {get;set;}
  
} 

public static class Program {
  public static void Main (string[] args) {

    Console.WriteLine(Day4());
    Console.WriteLine(Day4_1());
  }
  public static int Day1()
  {
    var lines = File.ReadAllLines("inputs/day1.txt");
    var retval = 0;
    foreach(var line in lines)
    {
      var integer = "";
      line.ToCharArray();

      for(var i=0; i < line.Length; i++)
      {
        if(Char.IsDigit(line[i]))
        {
          integer+=line[i];
          break;
        }
      }
      for(var j = line.Length-1; j >=0; j--)
      {
        if(Char.IsDigit(line[j]))
        {
          integer+=line[j];
          break;
        }
      }
    
      retval+=Int32.Parse(integer);
    }
    return retval;
  }

  public static int Day1_2()
  {
    var lines = File.ReadAllLines("inputs/day1.txt");
    var retval = 0;
    foreach(var line in lines)
    {
      var arr = new List<int>();
      
      for(var j=0; j <= line.Length-1; j++) 
      {
        if(Char.IsDigit(line[j]))
        {
          var num = Int32.Parse(line[j].ToString());
          arr.Add(num);
        }
        else if(line.Substring(j).StartsWith("one")) 
        {
          arr.Add(1);
       } 
        else if(line.Substring(j).StartsWith("two"))
        {
          arr.Add(2);
        } 
        else if(line.Substring(j).StartsWith("three"))
        {
          arr.Add(3);
        } 
        else if(line.Substring(j).StartsWith("four"))
        {
          arr.Add(4);
        } 
        else if(line.Substring(j).StartsWith("five"))
        {
          arr.Add(5);
        } 
        else if(line.Substring(j).StartsWith("six"))
        {
          arr.Add(6);
        } 
        else if(line.Substring(j).StartsWith("seven")) 
        {
          arr.Add(7);
        } 
        else if(line.Substring(j).StartsWith( "eight")) 
        {
          arr.Add(8);
        } 
        else if(line.Substring(j).StartsWith("nine"))
        {
          arr.Add(9);
        }    
      }
      
      var linevar = Int32.Parse(arr[0].ToString()+arr[arr.Count-1].ToString());
      retval+=linevar;
  }
    return retval;
}

  public static int Day2()
  {
    var lines = File.ReadAllLines("inputs/day2.txt");
    var pattern = @"Game (\d+): (.*$)";
    var retval = 0;
    foreach(var line in lines)
    {
      var match = Regex.Match(line, pattern);

      var gameNum = int.Parse(match.Groups[1].Value);
      
      var data = match.Groups[2].Value;
      var sets = data.Split(new[] { ';' });
      
      var validGames = new List<int>();
      foreach(var s in sets)
      {
        var redCount = GetColorCount(s, "red");
        var greenCount = GetColorCount(s, "green");
        var blueCount = GetColorCount(s, "blue");
        
        if (redCount <= 12 && greenCount <= 13 && blueCount <= 14)
        {
            validGames.Add(gameNum);
        }
      }
      if(validGames.Count == sets.Length)
      {
        retval+=gameNum;
      }
    }
    return retval;
  }
  private static int GetColorCount(string setData, string color)
  {
      string pattern = $@"(\d+) {color}";
      Match match = Regex.Match(setData, pattern);

      if (match.Success)
      {
          return int.Parse(match.Groups[1].Value);
      }

      return 0;
  }
   public static int Day2_1()
   {
     var lines = File.ReadAllLines("inputs/day2.txt");
     var pattern = @"Game (\d+): (.*$)";
     var retval = 0;
     foreach(var line in lines)
     {
       var match = Regex.Match(line, pattern);

       var data = match.Groups[2].Value;
       var sets = data.Split(new[] { ';' });

       var maxColorCount = new Dictionary<string, int>();
       foreach(var s in sets)
       {
         var redCount = GetColorCount(s, "red");
         var greenCount = GetColorCount(s, "green");
         var blueCount = GetColorCount(s, "blue");

         if(maxColorCount.ContainsKey("red"))
         {
           if(maxColorCount["red"] < redCount)
           {
             maxColorCount["red"] = redCount;
           }
         }
         else
          {
            maxColorCount["red"] = redCount;
          }
         if(maxColorCount.ContainsKey("green"))
          {
            if(maxColorCount["green"] < greenCount)
            {
              maxColorCount["green"] = greenCount;
            }
          }
         else
         {
           maxColorCount["green"] = greenCount;
         }
         if(maxColorCount.ContainsKey("blue"))
          {
            if(maxColorCount["blue"] < blueCount)
            {
              maxColorCount["blue"] = blueCount;
            }
          }
         else
         {
           maxColorCount["blue"] = blueCount;
         }
       }
       retval+=maxColorCount["red"]*maxColorCount["green"]*maxColorCount["blue"];
     }
     return retval;
   }

  public static int Day3()
  {
    var lines = File.ReadAllLines("inputs/day3.txt");
    var retval = 0;

    for(var i = 0; i < lines.Length; i++)
    {
      var d = "";
      
      var isNeeded = false;
      for(var j = 0; j < lines[i].Length; j++)
      {
        if(Char.IsDigit(lines[i][j]))
        {
          d+=lines[i][j];
          
          isNeeded = isNeeded || CheckNeighbours(lines, i, j);
        }
        else if(d != "")
        {
          if(isNeeded)
          {
            retval+=int.Parse(d);
          }
          d = "";
          isNeeded = false;
        }        
      }
      if(d != "" && isNeeded)
      {
        retval+=int.Parse(d);
      }
    }
    return retval;
    
  }
  private static bool CheckNeighbours(string[] lines, int i, int j)
  {
    return (j < lines[i].Length-1 && lines[i][j+1] != '.' && !Char.IsDigit(lines[i][j+1]) ||
      j> 0 && lines[i][j-1] != '.' && !Char.IsDigit(lines[i][j-1])||
      i> 0 && lines[i-1][j] != '.' && !Char.IsDigit(lines[i-1][j]) ||
      i < lines.Length-1 && lines[i+1][j] != '.' && !Char.IsDigit(lines[i+1][j]) ||
      j< lines[i].Length-1 && i> 0 && lines[i-1][j+1]!= '.' && !Char.IsDigit(lines[i-1][j+1])||
      i < lines.Length-1 && j< lines[i].Length-1 && lines[i+1][j+1]!= '.' && !Char.IsDigit(lines[i+1][j+1]) ||
      i > 0 && j > 0 && lines[i-1][j-1]!= '.' && !Char.IsDigit(lines[i-1][j-1]) ||
      i < lines.Length-1 && j > 0 && lines[i+1][j-1]!= '.' && !Char.IsDigit(lines[i+1][j-1]));
  }

  public static int Day3_1()
  {
    var lines = File.ReadAllLines("inputs/day3.txt");
    var retval = 0;
    var coords = new Dictionary<(int, int), MyNumber>();
    
    for(var i = 0; i < lines.Length; i++)
    {
      var d = "";
      var myNumber = new MyNumber();
      for(var j = 0; j < lines[i].Length; j++)
      {        
        if(Char.IsDigit(lines[i][j]))
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
            foreach ((int di, int dj) in directions)
            {
                int nx = j + dj;
                int ny = i + di;
                
                if (coords.TryGetValue((ny, nx), out MyNumber v))
                {
                    nums.Add(v);
                }
                
            }
            if (nums.Count == 2)
            {
                int a = nums.ToList()[0].Value;
                int b = nums.ToList()[1].Value;
                retval += a * b;
            }
        }

      }
    }
    
    return retval;
  }

  public static int Day4()
  {
     var lines = File.ReadAllLines("inputs/day4.txt");
     var retval = 0;
     foreach(var line in lines)
    {
       var groups = line.Split(':');
       var firstGroup = groups[1].Trim(); 
       var subGroups = firstGroup.Split('|');

       var winningNums = subGroups[0].Split(new char[] { ' ', '\t' },  StringSplitOptions.RemoveEmptyEntries) ;
       Array.ConvertAll(winningNums, int.Parse);
       var myNums = subGroups[1].Split(new char[] { ' ', '\t' },  StringSplitOptions.RemoveEmptyEntries);
      Array.ConvertAll(myNums, int.Parse);

      var totalScore = 0;
      foreach(var winningNum in winningNums)
      {
        if(myNums.Contains(winningNum))
        {
          if(totalScore == 0)
          {
            totalScore = 1;
          }
          else
          {
            totalScore *= 2;
          }
        }
      }
      retval+=totalScore;
    }
    return retval;
  }

  public static int Day4_1()
  {
    var lines = File.ReadAllLines("inputs/day4.txt");
    var retval = 0;
    var cardsDict = new Dictionary<int, int>();
    foreach(var line in lines)
    {
       var groups = line.Split(':');

      var cardNum = int.Parse(groups[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
       var firstGroup = groups[1].Trim(); 
       var subGroups = firstGroup.Split('|');

       var winningNums = subGroups[0].Split(new char[] { ' ', '\t' },  StringSplitOptions.RemoveEmptyEntries) ;
       Array.ConvertAll(winningNums, int.Parse);
       var myNums = subGroups[1].Split(new char[] { ' ', '\t' },  StringSplitOptions.RemoveEmptyEntries);
      Array.ConvertAll(myNums, int.Parse);

      var winningCount = 0;
      foreach(var winningNum in winningNums)
      {
        if(myNums.Contains(winningNum))
        {
          winningCount+=1; 
        }
      }
      
      cardsDict[cardNum] = cardsDict.GetValueOrDefault(cardNum, 0) + 1;
            
      retval += cardsDict[cardNum];
      
      for(var i = 1; i <= winningCount; i++)
      {
        cardsDict[cardNum+i] = cardsDict.GetValueOrDefault(cardNum+i, 0) +cardsDict[cardNum];
      }
    }
    
    return retval;
  }
  
}