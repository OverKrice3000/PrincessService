using PrincessProject.PrincessClasses.Strategy;

namespace PrincessProject.utils;

public static class Util
{
    public static string DeriveOutputFileName(int outputFilesExists)
    {
        return Constants.OutputFileBasename 
               + "_" + new Random().Next(Constants.First7DigitInteger).ToString("D" + Constants.OutputFileNumberPadding)
               + "_" + outputFilesExists.ToString("D" + Constants.OutputFileNumberPadding)
               + Constants.OutputFileExtension;
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

}