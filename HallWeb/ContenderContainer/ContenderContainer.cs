using HallWeb.ContenderGeneratorClasses;
using PrincessProject.Data.model;

namespace HallWeb.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    private readonly IContenderGenerator _generator;

    public ContenderContainer(IContenderGenerator generator,
        int initialSize = PrincessProject.Data.Constants.DefaultContendersCount)
    {
        _generator = generator;
        Contenders = Array.Empty<Contender>();
    }

    public Contender[] Contenders { get; private set; }

    public void Reset(in int size = PrincessProject.Data.Constants.DefaultContendersCount)
    {
        Contenders = _generator.Generate(size);
    }

    public Contender this[int index] => Contenders[index];
}