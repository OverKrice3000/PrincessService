using PrincessProject.utils;

namespace PrincessProject.utils;

public static class Constants
{
    // Loader configuration variables
    public const string FromProjectRootCsvNamesFilepath = "recourses\\names\\russian_names.csv";
    public const string CsvNamesColumn = "Name";
    public const string FromProjectRootCsvSurnamesFilepath = "recourses\\names\\russian_surnames.csv";
    public const string CsvSurnamesColumn = "Surname";
    public const char CsvNamesSurnamesSeparator = ';';
    
    // Configurable contenders count
    public const int DefaultContendersCount = 900;
    // Configurable contenders count starting from which alternative strategy is to be used
    public const int ManyCandidatesStrategyCandidatesLowerBorder = 1000;
    
    // CurrentCandidatePositionAnalysisStrategy config
    public static class CurrentCandidatePositionAnalysisStrategyConfig
    {
        public const double FirstContendersRejectedPercentage = 0.1;
        public const double WorthyContenderSatisfactoryLowerBorderPercentage = 0.95;
        public static readonly BigFloat WorthyContenderSatisfactoryProbability = new BigFloat("0.9");
    }

    public static class LargeNumbersLawStrategyConfig
    {
        public const double FirstContendersRejectedPercentage = 1 / Math.E;
        public const int SatisfactoryContenderPositionUpperBorder = 8;
    }


    // Non configurable constants
    public const double IdiotHusbandTopBorderPercentage = 0.5;
    public const int IdiotHusbandHappinessLevel = 0;
    public const int NoHusbandHappinessLevel = 10;
}