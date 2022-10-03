using PrincessProject.ContenderContainer;
using PrincessTestProject.Mocks.ContenderContainer;

namespace PrincessTestProject.Builder.ContenderContainerBuilder;

public class MContenderContainerBuilder
{
    private int _size = 100;

    public MContenderContainerBuilder WithNumberOfContenders(in int size)
    {
        _size = size;
        return this;
    }

    public IContenderContainer Build()
    {
        return new MContenderContainer(_size);
    }
}