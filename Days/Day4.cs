using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2023.UI;

namespace AdventOfCode2023.Days;

public class Day4 : BaseDay
{
    public override string PartOne()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day4.txt");
        var retval = 0;
        foreach (var line in lines)
        {
            var groups = line.Split(':');
            var firstGroup = groups[1].Trim();
            var subGroups = firstGroup.Split('|');

            var winningNums = subGroups[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ConvertAll(winningNums, int.Parse);
            var myNums = subGroups[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ConvertAll(myNums, int.Parse);

            var totalScore = 0;
            foreach (var winningNum in winningNums)
            {
                if (myNums.Contains(winningNum))
                {
                    if (totalScore == 0)
                    {
                        totalScore = 1;
                    }
                    else
                    {
                        totalScore *= 2;
                    }
                }
            }

            retval += totalScore;
        }

        return retval.ToString();
    }

    public override string PartTwo()
    {
        var lines = File.ReadAllLines("/Users/valeria/RiderProjects/AoC_2023/inputs/day4.txt");
        var retval = 0;
        var cardsDict = new Dictionary<int, int>();
        foreach (var line in lines)
        {
            var groups = line.Split(':');

            var cardNum = int.Parse(groups[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            var firstGroup = groups[1].Trim();
            var subGroups = firstGroup.Split('|');

            var winningNums = subGroups[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ConvertAll(winningNums, int.Parse);
            var myNums = subGroups[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ConvertAll(myNums, int.Parse);

            var winningCount = 0;
            foreach (var winningNum in winningNums)
            {
                if (myNums.Contains(winningNum))
                {
                    winningCount += 1;
                }
            }

            cardsDict[cardNum] = cardsDict.GetValueOrDefault(cardNum, 0) + 1;

            retval += cardsDict[cardNum];

            for (var i = 1; i <= winningCount; i++)
            {
                cardsDict[cardNum + i] = cardsDict.GetValueOrDefault(cardNum + i, 0) + cardsDict[cardNum];
            }
        }

        return retval.ToString();
    }
}