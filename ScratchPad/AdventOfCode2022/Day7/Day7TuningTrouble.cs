using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Day7;

public class Day7NoSpaceLeft
{
    public record Directory(string Name, string Path, List<Directory>? Directories, List<File>? Files)
    {
        public static List<int> ListOfBelowLimitDirectories = new() { };

        public static List<int> SmallestDirectoriesToDelete = new() { };

        public Directory(string name, string path)
            : this(name, path, new List<Directory>(), new List<File>())
        {
        }

        public static Directory GetDirectoryByPath(Directory directory, string path)
        {
            // Recursively search directories and return the one with the right path.
            if (directory == null)
                return null;

            if (directory.Path == path)
                return directory;

            foreach (var child in directory.Directories)
            {
                var found = GetDirectoryByPath(child, path);
                if (found != null)
                    return found;
            }

            return null;
        }

        public static int GetTotalSizeOfDirectory(Directory directory)
        {
            var totalSize = 0;
            if (directory.Files != null)
            {
                foreach (var (_, size) in directory.Files)
                {
                    totalSize += size;
                }
            }

            if (directory.Directories != null)
            {
                foreach (var child in directory.Directories)
                {
                    var totalChildSize = GetTotalSizeOfDirectory(child);
                    if (totalChildSize != null)
                        totalSize += totalChildSize;
                }
            }

            if (totalSize >= 8381165)
            {
                SmallestDirectoriesToDelete.Add(totalSize);
            }

            return totalSize;
        }

        public static int GetTotalSizeBelowLimit(Directory directory)
        {
            var totalSize = 0;
            totalSize = GetTotalSizeRecursively(directory, totalSize);

            if (totalSize <= 100000)
            {
                ListOfBelowLimitDirectories.Add(totalSize);
            }

            return totalSize;
        }

        private static int GetTotalSizeRecursively(Directory directory, int totalSize)
        {
            if (directory.Files != null)
            {
                foreach (var (_, size) in directory.Files)
                {
                    totalSize += size;
                }
            }

            if (directory.Directories != null)
            {
                foreach (var child in directory.Directories)
                {
                    var totalChildSize = GetTotalSizeBelowLimit(child);
                    if (totalChildSize != null)
                        totalSize += totalChildSize;
                }
            }

            return totalSize;
        }
    };

    public record File(string Name, int Size);

    public Day7NoSpaceLeft()
    {
        RootDirectory = GetRootDirectory();
    }

    public readonly Directory? RootDirectory;

    private static Directory? GetRootDirectory()
    {
        var terminalOutput =
            System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day7\PuzzleInput.txt");

        var rootDirectory = new Directory("/", "", new List<Directory>(), new List<File>());

        Directory currentDirectory = rootDirectory;
        Directory? parentDirectory = null;
        foreach (var line in terminalOutput)
        {
            if (line.StartsWith("$"))
            {
                if (line.Contains("cd"))
                {
                    var cdName = line.Split(" ").LastOrDefault();

                    if (cdName == "/")
                    {
                        continue;
                    }

                    if (cdName == ".." && parentDirectory != null)
                    {
                        currentDirectory = parentDirectory;
                        var parentDirectoryItems = currentDirectory.Path.Split("/").ToList();
                        parentDirectoryItems.RemoveAt(parentDirectoryItems.Count - 1);
                        var parentDirectoryPath = string.Join("/", parentDirectoryItems);
                        parentDirectory = Directory.GetDirectoryByPath(rootDirectory, parentDirectoryPath);
                        continue;
                    }

                    parentDirectory = currentDirectory;
                    currentDirectory = currentDirectory.Directories.FirstOrDefault(x => x.Name == cdName);
                }

                else if (line.Contains("ls"))
                {
                    continue;
                }
            }

            else if (line.Contains("dir"))
            {
                var dirName = line.Split(" ").LastOrDefault();
                if (dirName != null)
                    currentDirectory.Directories.Add(new Directory(dirName,
                        (currentDirectory?.Path ?? "") + "/" + dirName));
            }

            else
            {
                var fileName = line.Split(" ").LastOrDefault();
                var fileSize = int.Parse(line.Split(" ").FirstOrDefault());

                currentDirectory.Files.Add(new File(fileName, fileSize));
            }
        }

        return rootDirectory;
    }
}

[TestFixture]
public class Day7
{
    [Test]
    public void Part1()
    {
        //https://adventofcode.com/2022/day/7
        // The input is a terminal output with commands beginning with "$"
        // cd means change directory. This changes which directory is the current directory, but the specific result depends on the argument:
        // cd x moves in one level: it looks in the current directory for the directory named x and makes it the current directory.
        //     cd .. moves out one level: it finds the directory that contains the current directory, then makes that directory the current directory.
        //     cd / switches the current directory to the outermost directory, /.
        // ls means list. It prints out all of the files and directories immediately contained by the current directory:
        // 123 abc means that the current directory contains a file named abc with size 123.
        //     dir xyz means that the current directory contains a directory named xyz.
        // IMPORTANT: this process can count files more than once!
        // Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?

        var puzzleInput = new Day7NoSpaceLeft();

        Day7NoSpaceLeft.Directory.GetTotalSizeBelowLimit(puzzleInput.RootDirectory);
        var sumOfDirectorySizes = Day7NoSpaceLeft.Directory.ListOfBelowLimitDirectories.Sum(x => x);

        sumOfDirectorySizes.Should().Be(1390824);
    }

    [Test]
    public void Part2()
    {
        //https://adventofcode.com/2022/day/7
        // The total disk space available to the filesystem is 70000000. To run the update, you need unused space of at least 30000000.
        // You need to find a directory you can delete that will free up enough space to run the update.
        // Find the smallest directory that, if deleted, would free up enough space on the filesystem to run the update. 
        // The update still requires a directory with total size of at least 8381165 to be deleted before it can run.
        // What is the total size of that directory?

        var puzzleInput = new Day7NoSpaceLeft();

        Day7NoSpaceLeft.Directory.GetTotalSizeOfDirectory(puzzleInput.RootDirectory);
        var smallestDirectoryToDelete = Day7NoSpaceLeft.Directory.SmallestDirectoriesToDelete.Min();

        smallestDirectoryToDelete.Should().Be(24933642);
    }
}