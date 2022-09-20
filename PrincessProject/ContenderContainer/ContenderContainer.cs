using PrincessProject.ContenderGenerator;
using PrincessProject.model;

namespace PrincessProject.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    private readonly IContenderGenerator _generator;

    public ContenderContainer(IContenderGenerator generator, int initialSize)
    {
        _generator = generator;
        Contenders = Array.Empty<Contender>();
    }

    public Contender[] Contenders { get; private set; }

    public void Reset(in int size)
    {
        var random = new Random();
        Contenders = _generator.Generate(size)
            .OrderBy(item => random.Next())
            .ToArray();
    }

    public Contender this[int index] => Contenders[index];
}