using HallWeb.ContenderContainer;
using PrincessProject.Data.model;

namespace HallWeb.utils;

public static class Util
{
    public static string DeriveOutputFileName(int outputFilesExists)
    {
        return
            $"{Constants.OutputFileBasename}_{outputFilesExists:D6}_{new Random().Next(Constants.First7DigitInteger):D6}{Constants.OutputFileExtension}";
    }

    public static void WriteSectionSeparator(StreamWriter writer)
    {
        writer.WriteLine("----------");
    }

    public static string GetProjectBaseDirectory()
    {
        var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        return currentDirectory.Parent!.Parent!.Parent!.FullName;
    }

    public static Contender FindContenderByName(
        IContenderContainer contenderContainer,
        int attemptId,
        VisitingContender visitingContender
    )
    {
        return Array.Find(
                   contenderContainer[attemptId].Contenders,
                   contender => contender.FullName.Equals(visitingContender.FullName)
               ) ??
               throw new ArgumentException("No contender with such name!");
    }

    public static VisitingContender VisitingContenderFromFullName(string fullName)
    {
        var split = fullName.Split(" ");
        if (split.Length != 2)
        {
            throw new ArgumentException("Bad contender name");
        }

        return new VisitingContender(split[0], split[1]);
    }
}