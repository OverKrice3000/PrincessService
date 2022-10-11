using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGeneratorClasses;

namespace PrincessTestProject.Builder.ContenderContainerBuilder;

public class ContenderContainerBuilder
{
    private IContenderGenerator _generator = TestBuilder
        .BuildIContenderGenerator()
        .BuildContenderGenerator()
        .Build();

    public ContenderContainerBuilder WithContenderGenerator(IContenderGenerator generator)
    {
        _generator = generator;
        return this;
    }

    public IContenderContainer Build()
    {
        return new ContenderContainer(_generator);
    }
}