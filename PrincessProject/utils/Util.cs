namespace PrincessProject.utils;

public static class Util
{
    public static string deriveOutputFileName(int outputFilesExists)
    {
        return Constants.OutputFileBasename + "_" + outputFilesExists.ToString("D" + Constants.OutputFileNumberPadding) +
               Constants.OutputFileExtension;
    }

    public static void writeSectionSeparator(StreamWriter writer)
    {
        writer.Write("----------" + Environment.NewLine);
    }
}