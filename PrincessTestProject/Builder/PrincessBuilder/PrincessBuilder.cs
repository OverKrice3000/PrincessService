using PrincessProject.Hall;
using PrincessProject.PrincessClasses;

namespace PrincessTestProject.Builder.PrincessBuilder;

public class PrincessBuilder
{
    private IHall _hall = TestBuilder.BuildIHall().BuildHall().Build();

    public PrincessBuilder WithHall(IHall hall)
    {
        _hall = hall;
        return this;
    }

    public IPrincess Build()
    {
        return new Princess(_hall);
    }
}