using PrincessProject.ContenderContainer;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessTestProject.Builder;

namespace PrincessTestProject;

public class FriendTests
{
    private const int ContendersInContainer = 100;
    private VisitingContender _best;

    private Contender _contender1;
    private Contender _contender2;
    private IContenderContainer _contenderContainer;
    private IFriend _friend;
    private VisitingContender _visitingContender1;
    private VisitingContender _visitingContender2;

    [SetUp]
    public void InitializeFriendDependencies()
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
        _contender1 = _contenderContainer[0];
        _contender2 = _contenderContainer[1];
        _visitingContender1 = Mappers.ContenderToVisitingContender(_contender1);
        _visitingContender2 = Mappers.ContenderToVisitingContender(_contender2);
        _best =
            Mappers.ContenderToVisitingContender((_contender1.Value < _contender2.Value) ? _contender2 : _contender1);
    }

    [Test]
    public void CorrectComparison()
    {
        _contender1.SetHasVisited();
        _contender2.SetHasVisited();
        Assert.True(_friend.CompareContenders(_visitingContender1, _visitingContender2).Equals(_best));
        Assert.True(_friend.CompareContenders(_visitingContender2, _visitingContender1).Equals(_best));
    }

    [Test]
    public void ThrowsWhenBothHasNotVisited()
    {
        Assert.Throws<ApplicationException>(() => _friend.CompareContenders(_visitingContender1, _visitingContender2));
        Assert.Throws<ApplicationException>(() => _friend.CompareContenders(_visitingContender2, _visitingContender1));
    }

    [Test]
    public void ThrowsWhenOneHasNotVisited()
    {
        _contender1.SetHasVisited();
        Assert.Throws<ApplicationException>(() => _friend.CompareContenders(_visitingContender1, _visitingContender2));
        Assert.Throws<ApplicationException>(() => _friend.CompareContenders(_visitingContender2, _visitingContender1));
    }
}