namespace PrincessProject.Data.model;

public record Attempt(int ContendersCount, ContenderData[] Contenders, int? ChosenContenderValue);