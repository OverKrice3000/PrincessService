namespace PrincessTestProject.utils;

public class Constants
{
    // Contender generator tests constants
    public const string FromProjectRootCsvNamesFilepath = "recourses\\names\\russian_names.csv";
    public const string CsvNamesColumn = "Name";
    public const string FromProjectRootCsvSurnamesFilepath = "recourses\\names\\russian_surnames.csv";
    public const string CsvSurnamesColumn = "Surname";
    public const char CsvNamesSurnamesSeparator = ';';
    public const int PossibleToGenerateContendersAmount = 100;
    public const int ImpossibleToGenerateContendersAmount = Int32.MaxValue;

    public const string InMemoryDatabaseDefaultName = "AttemptsDatabase";
}