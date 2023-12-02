using System;
using System.IO; 
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class Program {
  public static void Main (string[] args) {

    Console.WriteLine(Day2());
    Console.WriteLine(Day2_1());
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
  
}