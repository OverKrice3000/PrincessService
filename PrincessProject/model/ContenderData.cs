namespace PrincessProject.model;

public record ContenderData(string Name, string Surname, int Value)
{
    public override string ToString()
    {
        return Name + " " + Surname;
    }
}