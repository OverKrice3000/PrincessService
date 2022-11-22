namespace PrincessProject.Data.model;

public record VisitingContender(string Name, string Surname)
{
    public string FullName => $"{Name} {Surname}";
}