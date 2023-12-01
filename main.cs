using System;
using System.IO; 
using System.Text;
using System.Collections.Generic;

public static class Program {
  public static void Main (string[] args) {

    Console.WriteLine(Day1());
    Console.WriteLine(Day1_2());
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
}