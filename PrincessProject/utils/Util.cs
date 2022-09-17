namespace PrincessProject.utils;

public static class Util
{
    public static string DeriveOutputFileName(int outputFilesExists)
    {
        return
            $"{Constants.OutputFileBasename}_{new Random().Next(Constants.First7DigitInteger)}_{outputFilesExists:D6}{Constants.OutputFileExtension}";
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