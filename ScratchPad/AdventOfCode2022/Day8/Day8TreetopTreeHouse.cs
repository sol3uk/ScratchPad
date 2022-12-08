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

    public readonly int[,] TreeMatrix;

    private static int[,] GetTreeMatrix()
    {
        var treeRows =
            File.ReadAllLines(Environment.CurrentDirectory + @"\Day8\PuzzleInput.txt");
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

    public static IEnumerable<int> GetSouthTrees(int y, int[,] input, int x)
    {
        return Enumerable.Range(y + 1, input.GetLength(1) - y - 1)
            .Select(yCoord => input[x, yCoord])
            .ToArray();
    }

    public static IEnumerable<int> GetNorthTrees(int y, int[,] input, int x)
    {
        return Enumerable.Range(0, y)
            .Select(yCoord => input[x, yCoord])
            .ToArray();
    }

    public static IEnumerable<int> GetEastTrees(int x, int[,] input, int y)
    {
        return Enumerable.Range(x + 1, input.GetLength(0) - x - 1)
            .Select(xCoord => input[xCoord, y])
            .ToArray();
    }

    public static IEnumerable<int> GetWestTrees(int x, int[,] input, int y)
    {
        return Enumerable.Range(0, x)
            .Select(xCoord => input[xCoord, y])
            .ToArray();
    }

    public static int GetTreeScore(IEnumerable<int> treesInDirection, int tree)
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

    public static bool IsTreeVisibleFromDirection(IEnumerable<int> treesInDirection, int tree)
    {
        return !treesInDirection.Any(lst => lst >= tree);
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
        for (var x = 0; x < input.GetLength(0); x++)
        {
            var width = input.GetLength(0);
            var height = input.GetLength(1);
            for (var y = 0; y < height; y++)
            {
                var tree = input[x, y];

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
                    var westTrees = Day8TreetopTreeHouse.GetWestTrees(x, input, y);
                    var eastTrees = Day8TreetopTreeHouse.GetEastTrees(x, input, y);
                    var northTrees = Day8TreetopTreeHouse.GetNorthTrees(y, input, x);
                    var southTrees = Day8TreetopTreeHouse.GetSouthTrees(y, input, x);
                    if (Day8TreetopTreeHouse.IsTreeVisibleFromDirection(westTrees, tree)
                        || Day8TreetopTreeHouse.IsTreeVisibleFromDirection(eastTrees, tree)
                        || Day8TreetopTreeHouse.IsTreeVisibleFromDirection(northTrees, tree)
                        || Day8TreetopTreeHouse.IsTreeVisibleFromDirection(southTrees, tree))
                    {
                        totalVisibleTrees++;
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
        for (var x = 0; x < input.GetLength(0); x++)
        {
            var width = input.GetLength(0);
            var height = input.GetLength(1);
            for (var y = 0; y < height; y++)
            {
                var tree = input[x, y];

                if (x == 0 || x == width - 1)
                {
                    continue;
                }

                if (y == 0 || y == height - 1)
                {
                    continue;
                }

                var westTreesInverted = Day8TreetopTreeHouse.GetWestTrees(x, input, y).Reverse().ToArray();
                var eastTrees = Day8TreetopTreeHouse.GetEastTrees(x, input, y);
                var northTreesInverted = Day8TreetopTreeHouse.GetNorthTrees(y, input, x).Reverse().ToArray();
                var southTrees = Day8TreetopTreeHouse.GetSouthTrees(y, input, x);

                var westScore = Day8TreetopTreeHouse.GetTreeScore(westTreesInverted, tree);
                var eastScore = Day8TreetopTreeHouse.GetTreeScore(eastTrees, tree);
                var northScore = Day8TreetopTreeHouse.GetTreeScore(northTreesInverted, tree);
                var southScore = Day8TreetopTreeHouse.GetTreeScore(southTrees, tree);

                var calculatedScenicScore = westScore * eastScore * northScore * southScore;
                if (calculatedScenicScore > highestScenicScore) highestScenicScore = calculatedScenicScore;
            }
        }

        highestScenicScore.Should().Be(410400);
    }
}