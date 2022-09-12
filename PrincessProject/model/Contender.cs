namespace PrincessProject.model;

public class Contender
{
    public readonly string Name;
    public readonly string Surname;
    public readonly int Value;
    public bool HasVisited { get; private set; }

    public Contender(string name, string surname, int value)
    {
        Name = name;
        Surname = surname;
        Value = value;
        HasVisited = false;
    }
    public void SetHasVisited()
    {
        HasVisited = true;
    }
};