using PrincessProject.Princess.Strategy;

namespace PrincessProject.utils;

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

    // Configurable contenders count
    public const int DefaultContendersCount = 100;
    
    // Configurable contenders count starting from which alternative strategy is to be used
    public const int ManyCandidatesStrategyCandidatesLowerBorder = 1000;
    
    // Debug mode, which enables some logs
    public const bool DebugMode = true;
    
    // Fully qualified name of IStrategy implementation used by princess
    public static Type PrincessStrategyClassType = typeof(CandidatePositionAnalysisStrategy);

    // Non configurable constants
    public const double IdiotHusbandTopBorderPercentage = 0.5;
    public const int IdiotHusbandHappinessLevel = 0;
    public const int NoHusbandHappinessLevel = 10;
    public const int First7DigitInteger = 1000000;
}