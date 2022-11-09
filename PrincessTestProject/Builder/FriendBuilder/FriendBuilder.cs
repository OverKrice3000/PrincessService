using HallWeb.ContenderContainer;
using HallWeb.Friend;

namespace PrincessTestProject.Builder.FriendBuilder;

public class FriendBuilder
{
    private IContenderContainer _container = TestBuilder.BuildIContenderContainer().BuildMContenderContainer().Build();

    public FriendBuilder WithContainer(IContenderContainer container)
    {
        _container = container;
        return this;
    }

    public IFriend Build()
    {
        return new Friend(_container);
    }
}