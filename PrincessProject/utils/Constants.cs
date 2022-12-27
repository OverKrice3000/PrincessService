namespace PrincessProject.utils;

public static class Constants
{
    // Configurable contenders count starting from which alternative strategy is to be used
    public const int ManyCandidatesStrategyCandidatesLowerBorder = 1000;

    // Debug mode, which enables some logs
    public const bool DebugMode = true;

    // Non configurable constants
    public const int FirstHusbandHappinessLevel = 20;
    public const int ThirdHusbandHappinessLevel = 50;
    public const int FifthHusbandHappinessLevel = 100;

    public const int FirstContenderValue = 100;
    public const int ThirdContenderValue = 98;
    public const int FifthContenderValue = 96;

    public const double IdiotHusbandTopBorderPercentage = 0.5;
    public const int IdiotHusbandHappinessLevel = 0;
    public const int NoHusbandHappinessLevel = 10;

    // Session
    public const int SessionId = 98098;
}