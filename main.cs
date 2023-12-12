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

    Console.WriteLine(Day12());
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

  public static long Day5()
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
          return result.Item1;
      }
  }

  public static long Day5_1()
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
          Console.WriteLine($"{string.Join(",", seeds)}");
          var result = seeds.Min(seed => seed.Item1);
          
        }

    return seeds.Min(seed => seed.Item1);
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
   public static int Day6()
  {
     var lines = File.ReadAllLines("inputs/day6.txt");
    
    var timeLine = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
    var distanceLine = lines[1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

    var time = timeLine.Skip(1).Select(int.Parse).ToList();
    var distance = distanceLine.Skip(1).Select(int.Parse).ToList(); 

    var retval = 1;
    for(var i = 0; i < time.Count; i++)
    {
      var dist = distance[i];
      var winningRacesCount = 0;
      for(var j = 0; j < time[i]+1; j++)
      {
        var possibleTime = (time[i] - j) * j;
        if(possibleTime > dist)
        {
          winningRacesCount += 1;
        }
        
      }
      retval*= winningRacesCount;
    }

    return retval;
   }
  public static long Day6_1()
  {
     var lines = File.ReadAllLines("inputs/day6.txt");

    var time = long.Parse(string.Join("", lines[0].Where(char.IsDigit)));
    var distance = long.Parse(string.Join("", lines[1].Where(char.IsDigit)));

    var winningRacesCount = 0;
    for(var j = 0; j < time+1; j++)
    {
      var possibleTime = (time - j) * j;
      if(possibleTime > distance)
      {
        winningRacesCount += 1;
      }

    }
    return winningRacesCount;
   }

  public static int Day7()
  {
    var lines = File.ReadAllLines("inputs/day7_1.txt");

    var playsDict = new Dictionary<string, int>();
    var cardsTypeDict = new Dictionary<string, List<string>>();
    foreach (var line in lines)
    {
        var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        playsDict[parts[0]] = int.Parse(parts[1]);       
        var handType = DetermineHandType(parts[0]);
      
      if (cardsTypeDict.ContainsKey(handType))
      {
        cardsTypeDict[handType].Add(parts[0]);
      }
      else
      {
        cardsTypeDict[handType] = new List<string> {parts[0]};
      }
      
    }

    var sortedHighCard = cardsTypeDict.GetValueOrDefault("High card", new List<string>())
     .OrderByDescending(hand => Strength(hand));

    var sortedOnePair = cardsTypeDict.GetValueOrDefault("One pair", new List<string>())
     .OrderByDescending(hand => Strength(hand));

    var sortedTwoPair = cardsTypeDict.GetValueOrDefault("Two pair", new List<string>())
     .OrderByDescending(hand => Strength(hand));
    //Console.WriteLine($"{string.Join(",", sortedTwoPair)}");

    var sortedThreeOfAKind = cardsTypeDict.GetValueOrDefault("Three of a kind", new List<string>())
     .OrderByDescending(hand => Strength(hand));

    //Console.WriteLine($"{string.Join(",", sortedThreeOfAKind)}");
    
    var sortedFullHouse = cardsTypeDict.GetValueOrDefault("Full house", new List<string>())
     .OrderByDescending(hand => Strength(hand));

     var sortedFourOfAKind = cardsTypeDict.GetValueOrDefault("Four of a kind", new List<string>())
     .OrderByDescending(hand => Strength(hand));
    
    var sortedFiveOfAKind = cardsTypeDict.GetValueOrDefault("Five of a kind", new List<string>())
     .OrderByDescending(hand => Strength(hand));

   var combinedList = new List<string>();

    combinedList.AddRange(sortedHighCard);
    combinedList.AddRange(sortedOnePair);
    combinedList.AddRange(sortedTwoPair);
    combinedList.AddRange(sortedThreeOfAKind);
    combinedList.AddRange(sortedFullHouse);
    combinedList.AddRange(sortedFourOfAKind);
    combinedList.AddRange(sortedFiveOfAKind);

    var retval = 0;
    for(var i = 0; i < combinedList.Count; i++)
    {
      var hand = combinedList[i];
      var bid = playsDict[hand];

      retval += bid * (i+1);
      //Console.WriteLine($"{hand} - {bid} - {bid * (i+1)} ");
      
    }
    
    return retval;
  }
  
  private static string Strength(string hand)
  {
    var value = "ABCDEFGHIJKLM";
    var order = "AKQJT98765432";
    var res = hand.Select(card => value[order.IndexOf(card)]);
    var strRes = string.Join("", res);
    //Console.WriteLine($"{hand} = {strRes}");
    return strRes;
  }
  
  private static string DetermineHandType(string hand)
  {
      var counts = new Dictionary<char, int>();

      foreach (char card in hand)
      {
          if (counts.ContainsKey(card))
              counts[card]++;
          else
              counts[card] = 1;
      }

      var uniqueCount = counts.Count;

      if (uniqueCount == 5)
      {
          return "High card";
      }
      else if (uniqueCount == 4)
      {
          return "One pair";
      }
      else if (uniqueCount == 3)
      {
          int maxCount = counts.Values.Max();
          if (maxCount == 3)
              return "Three of a kind";
          else
              return "Two pair";
      }
      else if (uniqueCount == 2)
      {
          int maxCount = counts.Values.Max();
          if (maxCount == 4)
              return "Four of a kind";
          else
              return "Full house";
      }
    return "";
  }

  public static long Day11()
  {

    var grid = File.ReadAllLines("inputs/day11.txt");

    var emptyRows = Enumerable.Range(0, grid.Length)
        .Where(r => grid[r].All(ch => ch == '.'))
        .ToList();

    var emptyCols = Enumerable.Range(0, grid[0].Length)
        .Where(c => grid.All(row => row[c] == '.'))
        .ToList();

    var points = new List<(int, int)>();
    for (var r = 0; r < grid.Length; r++)
    {
        for (var c = 0; c < grid[0].Length; c++)
        {
            if (grid[r][c] == '#')
            {
                points.Add((r, c));
            }
        }
    }

    var combinations = points.SelectMany((p, i) => points.Skip(i + 1), (p, q) => (p, q));

    var total = combinations.Sum(pair =>
    {
        long sum = Math.Abs(pair.p.Item1 - pair.q.Item1) +
                   Math.Abs(pair.p.Item2 - pair.q.Item2);

        sum += 999999L * emptyRows.Intersect(Enumerable.Range(Math.Min(pair.p.Item1, pair.q.Item1), Math.Abs(pair.p.Item1 - pair.q.Item1) + 1)).Count();
        sum += 999999L * emptyCols.Intersect(Enumerable.Range(Math.Min(pair.p.Item2, pair.q.Item2), Math.Abs(pair.p.Item2 - pair.q.Item2) + 1)).Count();

        return sum;
    });

    return total;
  }

  public static int Day12()
  {
     var lines = File.ReadAllLines("inputs/day12.txt");
     var retval = 0;
     foreach(var line in lines)
    {
      var pattern = line.Split(" ")[0];
      var comb = line.Split(" ")[1].Split(",").Select(str => int.Parse(str.Trim())).ToArray();
      retval += CombinationPossibleCount(pattern, comb);
      Console.WriteLine($"{pattern} - {CombinationPossibleCount(pattern, comb)}");
      
    }
    
    return retval;
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
    Console.WriteLine($"{cfg} - result = {result}");

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
