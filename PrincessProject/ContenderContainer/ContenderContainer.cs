using PrincessProject.ContenderGenerator;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    private readonly IContenderGenerator _generator;

    public ContenderContainer(IContenderGenerator generator, int initialSize = Constants.DefaultContendersCount)
    {
        _generator = generator;
        Contenders = Array.Empty<Contender>();
    }

    public Contender[] Contenders { get; private set; }

    public void Reset(in int size = Constants.DefaultContendersCount)
    {
        Contenders = _generator.Generate(size);
    }

    public Contender this[int index] => Contenders[index];
}