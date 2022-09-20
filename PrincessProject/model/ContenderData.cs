namespace PrincessProject.model;

public record ContenderData(string Name, string Surname, int Value)
{
    public string FullName => $"{Name} {Surname}";
}