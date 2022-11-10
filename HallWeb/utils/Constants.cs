namespace HallWeb.utils;

public static class Constants
{
    // Files configuration variables
    public const string FromProjectRootCsvNamesFilepath = "recourses\\names\\russian_names.csv";
    public const string CsvNamesColumn = "Name";
    public const string FromProjectRootCsvSurnamesFilepath = "recourses\\names\\russian_surnames.csv";
    public const string CsvSurnamesColumn = "Surname";
    public const char CsvNamesSurnamesSeparator = ';';

    // Output files configuration variables
    public const string FromProjectRootOutputFolderPath = "output";
    public const string OutputFileBasename = "attempt";
    public const string OutputFileExtension = ".txt";

    // Count of attempts generated at start if database is empty
    public const int DatabaseAttemptsGenerated = 100;

    // Debug mode, which enables some logs
    public const bool DebugMode = true;

    // Non configurable constants
    public const int First7DigitInteger = 1000000;
}