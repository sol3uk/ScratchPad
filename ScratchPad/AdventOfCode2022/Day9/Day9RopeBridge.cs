using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Coord = AdventOfCode2022.Day9.Day9RopeBridge.Coord;

namespace AdventOfCode2022.Day9;

public class Day9RopeBridge
{
    public record Coord(int X, int Y)
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord GoUp(int distance)
        {
            Y += distance;
            return this;
        }

        public Coord GoDown(int distance)
        {
            Y -= distance;
            return this;
        }

        public Coord GoRight(int distance)
        {
            X += distance;
            return this;
        }

        public Coord GoLeft(int distance)
        {
            X -= distance;
            return this;
        }

        public bool IsAdjacentTo(Coord otherCoord)
        {
            if (Math.Abs(X - otherCoord.X) > 1)
            {
                return false;
            }

            if (Math.Abs(Y - otherCoord.Y) > 1)
            {
                return false;
            }

            return true;
        }
    };

    public record Motion(string Direction, int Distance);

    public Day9RopeBridge()
    {
        RopeMotions = GetMotions();
    }

    private List<Motion> GetMotions()
    {
        var fileLines = File.ReadAllLines(Environment.CurrentDirectory + @"\Day9\PuzzleInput.txt");

        return fileLines
            .Select(line =>
                new Motion(line.Split(" ")[0], int.Parse(line.Split(" ")[1])))
            .ToList();
    }

    public List<Motion> RopeMotions;
}

[TestFixture]
public class Day9
{
    [Test]
    public void Part1()
    {
        //https://adventofcode.com/2022/day/9
        // The input is a list of commands to move rope around
        // Consider a rope with a knot at each end; these knots mark the head and the tail of the rope.
        // If the head moves far enough away from the tail, the tail is pulled toward the head.
        // the head (H) and tail (T) must always be touching (diagonally adjacent and even overlapping both count as touching):
        // Assume the head and the tail both start at the same position, overlapping.
        // How many positions does the tail of the rope visit at least once?
        var tailLocations = new List<Tuple<int, int>>();
        var puzzleInput = new Day9RopeBridge();
        var headLocation = new Coord(0, 0);
        var tailLocation = new Coord(0, 0);
        foreach (var (direction, distance) in puzzleInput.RopeMotions)
        {
            var travelled = 0;
            // Initial location
            tailLocations.Add(new(tailLocation.X, tailLocation.Y));
            while (travelled < distance)
            {
                if (direction == "U")
                {
                    headLocation.GoUp(1);
                    if (tailLocation != headLocation && !tailLocation.IsAdjacentTo(headLocation))
                    {
                        if (tailLocation.X == headLocation.X) // In the same column
                        {
                            tailLocation.GoUp(1);
                        }
                        else
                        {
                            if (tailLocation.X > headLocation.X)
                            {
                                tailLocation.GoLeft(1);
                            }
                            else
                            {
                                tailLocation.GoRight(1);
                            }

                            tailLocation.GoUp(1);
                        }

                        tailLocations.Add(new(tailLocation.X, tailLocation.Y));
                    }
                }

                if (direction == "D")
                {
                    headLocation.GoDown(1);
                    if (tailLocation != headLocation && !tailLocation.IsAdjacentTo(headLocation))
                    {
                        if (tailLocation.X == headLocation.X) // In the same column
                        {
                            tailLocation.GoDown(1);
                        }
                        else
                        {
                            if (tailLocation.X > headLocation.X)
                            {
                                tailLocation.GoLeft(1);
                            }
                            else
                            {
                                tailLocation.GoRight(1);
                            }

                            tailLocation.GoDown(1);
                        }

                        tailLocations.Add(new(tailLocation.X, tailLocation.Y));
                    }
                }

                if (direction == "R")
                {
                    headLocation.GoRight(1);
                    if (tailLocation != headLocation && !tailLocation.IsAdjacentTo(headLocation))
                    {
                        if (tailLocation.Y == headLocation.Y) // On the same row
                        {
                            tailLocation.GoRight(1);
                        }
                        else
                        {
                            if (tailLocation.Y > headLocation.Y)
                            {
                                tailLocation.GoDown(1);
                            }
                            else
                            {
                                tailLocation.GoUp(1);
                            }

                            tailLocation.GoRight(1);
                        }

                        tailLocations.Add(new(tailLocation.X, tailLocation.Y));
                    }
                }

                if (direction == "L")
                {
                    headLocation.GoLeft(1);
                    if (tailLocation != headLocation && !tailLocation.IsAdjacentTo(headLocation))
                    {
                        if (tailLocation.Y == headLocation.Y) // On the same row
                        {
                            tailLocation.GoLeft(1);
                        }
                        else
                        {
                            if (tailLocation.Y > headLocation.Y)
                            {
                                tailLocation.GoDown(1);
                            }
                            else
                            {
                                tailLocation.GoUp(1);
                            }

                            tailLocation.GoLeft(1);
                        }

                        tailLocations.Add(new(tailLocation.X, tailLocation.Y));
                    }
                }

                travelled++;
            }
        }

        var numberOfPositionsTailMoved = tailLocations.Distinct().Count();
        numberOfPositionsTailMoved.Should().Be(5930);
    }
}