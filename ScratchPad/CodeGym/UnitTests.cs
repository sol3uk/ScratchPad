using System.Collections;
using NUnit.Framework;

namespace CodeGym;

public class Tests
{
    [Test]
    public void AnagramKataTests()
    {
        // https://codingdojo.org/kata/Anagram/
        // Write a program that generates all two-word anagrams of the string “documenting”. 
        // It must not use the same letters across the two words
        var expectedWordsOutput = new List<string>()
        {
            "ship", "ping"
        };

        var actualWordsOutput = new List<string>()
        {
        };

        AnagramKata.GetWordsOutput(AnagramKata.WordInput, actualWordsOutput);

        Assert.AreEqual(expectedWordsOutput, actualWordsOutput);
    }

    [Test]
    public void InglenookShuntingTests()
    {
        // https://asoscom.atlassian.net/wiki/spaces/CHAS/pages/3911647492/Inglenook+Shunting+Puzzle
        /*The puzzle consists of:
        a main line track (A) that can hold 3 carriages in the left section and 5 in the right (B)
        two sidings each can hold 3 carriages (C and D)
        one locomotive, which is small enough to fit in each section of track along with the stated carriages
        8 carriages, each given a letter, in an arbitrary arrangement on the mainline & sidings 
         */
        var trackA = new Stack<string>(3) // Limit of 3
        {
        };
        var trackB = new Stack<string>(5); // Limit of 5
        trackB.Push("e");
        trackB.Push("d");
        trackB.Push("c");
        trackB.Push("b");
        trackB.Push("a");

        var trackC = new Stack<string>(3); // Limit of 3
        trackC.Push("g");
        trackC.Push("f");

        var trackD = new Stack<string>(3); // Limit of 3
        trackD.Push("h");

        var mainTargetTrackState = new Stack<string>();
        mainTargetTrackState.Push("e");
        mainTargetTrackState.Push("d");
        mainTargetTrackState.Push("c");
        mainTargetTrackState.Push("b");
        mainTargetTrackState.Push("h");

        var actualMoves =
            InglenookShuntingKata.GetMovesFromTarget(trackA, trackB, trackC, trackD, mainTargetTrackState);

        var expectedMoves = new List<string>()
        {
            "B to C +1",
            "D to B +1",
        };
        Assert.AreEqual(expectedMoves, actualMoves);
    }

    [TestCase(0, "Midnight")]
    [TestCase(60, "One minute past midnight")]
    [TestCase(53700, "Five minutes to three")]
    [TestCase(53708, "Four minutes and fifty two seconds to three")]
    [TestCase(1800, "Half past midnight")]
    public void FullEnglishTimeTests(int secondsSinceMidnight, string expectedOutput)
    {
        // https://asoscom.atlassian.net/wiki/spaces/CHAS/pages/3928425370/Full+English+Time
        /*The puzzle consists of:
         You are given the number of seconds elapsed since midnight 
            (12:00 am, 00:00, one minute passed 23:59, however you want to represent that).
            Output Examples
            0 : Midnight
            60 : One minute past midnight
            553700 : Five minutes to three
            53708  : Four minutes and fifty two seconds to three
         */

        var actualOutput = FullEnglishTimeKata.GetEnglishTime(secondsSinceMidnight);


        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [TestCase("n,s,n,s,n,s,n,s,n,s", 0, 0)]
    [TestCase("e,w,e,w,e,w,e,w,e,w", 0, 0)]
    [TestCase("e,e,e,e,e,e,e,e,e,e", 10, 0)]
    public void CompassTests(string directionsString, int expectedX, int expectedY)
    {
        /*
         given an array of 'n','s','w','e' values, each of which represent a direction you walk in for 1 minutes.
         You have to work out if you end up back where you started within 10 minutes
         */

        var directions = directionsString.Split(',').ToList();
        var actualOutput = CompassKata.GetResultOfWalk(directions);

        Assert.AreEqual((expectedX, expectedY), actualOutput);
    }

    [TestCase("n,e,s,w,e", "w")]
    [TestCase("n,n,n,n,e", "w,s,s,s,s")]
    public void CompassDirectionsTests(string directionsString, string expectedDirections)
    {
        var directions = directionsString.Split(',').ToList();
        var actualOutput = CompassKata.GetDirectionsBack(directions);

        Assert.AreEqual(expectedDirections, actualOutput);
    }
}