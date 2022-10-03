using PrincessProject.ContenderContainer;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessTestProject.Builder;
using PrincessTestProject.utils;

namespace PrincessTestProject.HallsTests;

public class HallTests
{
    private const int ContendersInContainer = 100;
    private IContenderContainer _contenderContainer;
    private IFriend _friend;
    private IHall _hall;

    [SetUp]
    public void InitializeHallDependencies()
    {
        _contenderContainer = TestBuilder
            .BuildIContenderContainer()
            .BuildMContenderContainer()
            .WithNumberOfContenders(ContendersInContainer)
            .Build();
        _friend = TestBuilder
            .BuildIFriend()
            .BuildFriend()
            .WithContainer(_contenderContainer)
            .Build();
        _hall = TestBuilder
            .BuildIHall()
            .BuildHall()
            .WithContainer(_contenderContainer)
            .WithFriend(_friend)
            .WithSize(ContendersInContainer)
            .Build();
    }

    [Test]
    public void ReturnsNextContenderIfExists()
    {
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            Assert.DoesNotThrow(() => _hall.GetNextContender());
        }
    }

    [Test]
    public void ThrowsWhenContenderDoesNotExists()
    {
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            _hall.GetNextContender();
        }

        Assert.Throws<ApplicationException>(() => _hall.GetNextContender());
    }
}