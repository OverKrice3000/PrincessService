using HallWeb.ContenderContainer;
using HallWeb.Friend;
using HallWeb.Hall;
using HallWeb.utils.AttemptSaver;

namespace PrincessTestProject.Builder.HallBuilder;

public class HallBuilder
{
    private IAttemptSaver _attemptSaver;
    private IContenderContainer _container;
    private IFriend _friend = TestBuilder.BuildIFriend().BuildFriend().Build();
    private int _size;

    public HallBuilder()
    {
        _container = TestBuilder.BuildIContenderContainer().BuildMContenderContainer().Build();
        _friend = TestBuilder.BuildIFriend().BuildFriend().WithContainer(_container).Build();
        _attemptSaver = new VoidAttemptSaver();
        _size = 100;
    }

    public HallBuilder WithContainer(in IContenderContainer container)
    {
        _container = container;
        return this;
    }

    public HallBuilder WithFriend(in IFriend friend)
    {
        _friend = friend;
        return this;
    }

    public HallBuilder WithSize(in int size)
    {
        _size = size;
        return this;
    }

    public IHall Build()
    {
        return new Hall(_friend, _attemptSaver, _container, _size);
    }
}