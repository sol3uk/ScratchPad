using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Day8;

public class Day8TreetopTreeHouse
{
    public Day8TreetopTreeHouse()
    {
        InteriorTreeMatrix = GetTreeMatrix();
    }

    public Dictionary<List<int>, List<int>> InteriorTreeMatrix;
    public Dictionary<List<int>, List<int>> FullTreeMatrix;

    private static Dictionary<List<int>, List<int>> GetTreeMatrix()
    {
        var treeRows =
            File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day8\PuzzleInput.txt");
        return new Dictionary<List<int>, List<int>>()
        {
        };
    }
}

[TestFixture]
public class Day8
{
    [Test]
    public void Part1()
    {
        //https://adventofcode.com/2022/day/8
        // The input is a grid of trees
        // Each tree is represented as a single digit whose value is its height, where 0 is the shortest and 9 is the tallest.
        // A tree is visible if all of the other trees between it and an edge of the grid are shorter than it.
        // Only consider trees in the same row or column; that is, only look up, down, left, or right from any given tree.
        // All of the trees around the edge of the grid are visible
        // How many trees are visible from outside the grid?

        var puzzleInput = new Day8TreetopTreeHouse();
        var totalVisibleTrees = 0;


        totalVisibleTrees.Should().Be(21);
    }
}