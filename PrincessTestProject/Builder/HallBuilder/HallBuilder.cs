using HallWeb.ContenderContainer;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.Friend;
using HallWeb.Hall;
using HallWeb.utils.ResultSaver;

namespace PrincessTestProject.Builder.HallBuilder;

public class HallBuilder
{
    private readonly IResultSaver _resultSaver;
    private IContenderContainer _container;
    private IFriend _friend;
    private FromDatabaseContenderGenerator _generator;
    private int _size;

    public HallBuilder()
    {
        _container = TestBuilder.BuildIContenderContainer().BuildMContenderContainer().Build();
        _friend = TestBuilder.BuildIFriend().BuildFriend().WithContainer(_container).Build();
        _generator = TestBuilder.BuildIContenderGenerator().BuildFromDatabaseContenderGenerator().Build();
        _resultSaver = new VoidResultSaver();
        _size = 100;
    }

    public HallBuilder WithContainer(in IContenderContainer container)
    {
        _container = container;
        return this;
    }

    public HallBuilder WithGenerator(in FromDatabaseContenderGenerator generator)
    {
        _generator = generator;
        return this;
    }

    public HallBuilder WithSize(in int size)
    {
        _size = size;
        return this;
    }

    public IHall Build()
    {
        return new Hall(_resultSaver, _generator, _container, _size);
    }
}