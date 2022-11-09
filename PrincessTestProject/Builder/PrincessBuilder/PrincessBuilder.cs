using HallWeb.Hall;
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
        // TODO fix tests
        return new Princess();
    }
}