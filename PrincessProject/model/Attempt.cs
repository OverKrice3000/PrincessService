namespace PrincessProject.model;

public record Attempt(int ContendersCount, ContenderData[] Contenders, int? ChosenContenderValue, int Happiness);