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
        TreeMatrix = GetTreeMatrix();
    }

    public int[,] TreeMatrix;

    private static int[,] GetTreeMatrix()
    {
        var treeRows =
            File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day8\PuzzleInput.txt");
        var height = treeRows.Length;
        var width = treeRows[0].Length;
        var treeMatrix = new int[width, height];
        for (var rowIndex = 0; rowIndex < treeRows.Length; rowIndex++)
        {
            var row = treeRows[rowIndex];
            for (var colIndex = 0; colIndex < row.ToArray().Length; colIndex++)
            {
                var c = row.ToArray()[colIndex];
                var tree = int.Parse(c.ToString());
                treeMatrix[colIndex, rowIndex] = tree;
            }
        }

        return treeMatrix;
    }

    public static int[] GetSouthTrees(int y, int[,] input, int x)
    {
        return Enumerable.Range(y + 1, input.GetLength(1) - y - 1)
            .Select(yCoord => input[x, yCoord])
            .ToArray();
    }

    public static int[] GetNorthTrees(int y, int[,] input, int x)
    {
        return Enumerable.Range(0, y)
            .Select(yCoord => input[x, yCoord])
            .ToArray();
    }

    public static int[] GetEastTrees(int x, int[,] input, int y)
    {
        return Enumerable.Range(x + 1, input.GetLength(0) - x - 1)
            .Select(xCoord => input[xCoord, y])
            .ToArray();
    }

    public static int[] GetWestTrees(int x, int[,] input, int y)
    {
        return Enumerable.Range(0, x)
            .Select(xCoord => input[xCoord, y])
            .ToArray();
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
        var input = puzzleInput.TreeMatrix;
        for (int x = 0; x < input.GetLength(0); x++)
        {
            for (int y = 0; y < input.GetLength(1); y++)
            {
                var tree = input[x, y];
                var width = input.GetLength(0);
                var height = input.GetLength(1);

                if (x == 0 || x == width - 1)
                {
                    totalVisibleTrees++;
                }
                else if (y == 0 || y == height - 1)
                {
                    totalVisibleTrees++;
                }
                else
                {
                    var leftSideRow = Day8TreetopTreeHouse.GetWestTrees(x, input, y);
                    var rightSideRow = Day8TreetopTreeHouse.GetEastTrees(x, input, y);
                    var topSideColumn = Day8TreetopTreeHouse.GetNorthTrees(y, input, x);
                    var belowSideColumn = Day8TreetopTreeHouse.GetSouthTrees(y, input, x);
                    if (!leftSideRow.Any(lst => lst >= tree))
                    {
                        Console.WriteLine($"{tree} Visible");
                        totalVisibleTrees++;
                    }
                    else if (!rightSideRow.Any(rst => rst >= tree))
                    {
                        Console.WriteLine($"{tree} Visible");
                        totalVisibleTrees++;
                    }
                    else if (!topSideColumn.Any(tst => tst >= tree))
                    {
                        Console.WriteLine($"{tree} Visible");
                        totalVisibleTrees++;
                    }
                    else if (!belowSideColumn.Any(bst => bst >= tree))
                    {
                        Console.WriteLine($"{tree} Visible");
                        totalVisibleTrees++;
                    }
                    else
                    {
                        Console.WriteLine($"{tree}");
                    }
                }
            }
        }

        totalVisibleTrees.Should().Be(1688);
    }

    

    [Test]
    public void Part2()
    {
        //https://adventofcode.com/2022/day/8
        // The input is a grid of trees
        // A tree's scenic score is found by multiplying together its viewing distance in each of the four directions.
        // Consider each tree on your map. What is the highest scenic score possible for any tree?

        var puzzleInput = new Day8TreetopTreeHouse();
        var highestScenicScore = 0;
        var input = puzzleInput.TreeMatrix;
        for (int x = 0; x < input.GetLength(0); x++)
        {
            for (int y = 0; y < input.GetLength(1); y++)
            {
                var tree = input[x, y];
                var width = input.GetLength(0);
                var height = input.GetLength(1);

                if (x == 0 || x == width - 1)
                {
                    continue;
                }
                else if (y == 0 || y == height - 1)
                {
                    continue;
                }
                else
                {
                    var leftSideRow = Enumerable.Range(0, x)
                        .Select(xCoord => input[xCoord, y])
                        .Reverse().ToArray();
                    var rightSideRow = Enumerable.Range(x + 1, input.GetLength(0) - x - 1)
                        .Select(xCoord => input[xCoord, y])
                        .ToArray();
                    var topSideColumn = Enumerable.Range(0, y)
                        .Select(yCoord => input[x, yCoord])
                        .Reverse().ToArray();
                    var belowSideColumn = Enumerable.Range(y + 1, input.GetLength(1) - y - 1)
                        .Select(yCoord => input[x, yCoord])
                        .ToArray();
                    int leftScore = 0, rightScore = 0, upScore = 0, downScore = 0;

                    leftScore = GetTreeScore(leftSideRow, tree);
                    rightScore = GetTreeScore(rightSideRow, tree);
                    upScore = GetTreeScore(topSideColumn, tree);
                    downScore = GetTreeScore(belowSideColumn, tree);

                    var calculatedScenicScore = leftScore * rightScore * upScore * downScore;
                    if (calculatedScenicScore > highestScenicScore) highestScenicScore = calculatedScenicScore;
                }
            }
        }

        highestScenicScore.Should().Be(410400);
    }

    private static int GetTreeScore(int[] treesInDirection, int tree)
    {
        var directionScore = 0;
        foreach (var lst in treesInDirection)
        {
            if (lst >= tree)
            {
                directionScore++;
                break;
            }

            directionScore++;
        }

        return directionScore;
    }
}