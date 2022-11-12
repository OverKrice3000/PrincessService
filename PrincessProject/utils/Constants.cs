namespace PrincessProject.utils;

public static class Constants
{
    // Configurable contenders count starting from which alternative strategy is to be used
    public const int ManyCandidatesStrategyCandidatesLowerBorder = 1000;

    // Debug mode, which enables some logs
    public const bool DebugMode = true;

    // Non configurable constants
    public const double IdiotHusbandTopBorderPercentage = 0.5;
    public const int IdiotHusbandHappinessLevel = 0;
    public const int NoHusbandHappinessLevel = 10;

    // Session
    public const int SessionId = 98098;

    // HallApi
    public const string WebAppApiBase = "http://localhost:12345";
    public const string HallApiBase = WebAppApiBase + "/hall";
    public const string FriendApiBase = WebAppApiBase + "/friend";
}