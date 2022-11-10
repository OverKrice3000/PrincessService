namespace PrincessProject.Data.model;

public class Contender
{
    public readonly string Name;
    public readonly string Surname;
    public readonly int Value;

    public Contender(string name, string surname, int value)
    {
        Name = name;
        Surname = surname;
        Value = value;
        HasVisited = false;
    }

    public string FullName => $"{Name} {Surname}";
    public bool HasVisited { get; private set; }

    public void SetHasVisited()
    {
        HasVisited = true;
    }

    public override string ToString()
    {
        return Name + " " + Surname;
    }
};