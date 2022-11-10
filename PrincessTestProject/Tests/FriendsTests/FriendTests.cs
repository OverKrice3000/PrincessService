using FluentAssertions;
using HallWeb.ContenderContainer;
using HallWeb.Friend;
using PrincessProject.Data.model;
using PrincessTestProject.Builder;

namespace PrincessTestProject.Tests.FriendsTests;

public class FriendTests
{
    private const int ContendersInContainerCount = 100;
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
            .WithNumberOfContenders(ContendersInContainerCount)
            .Build();
        _friend = TestBuilder
            .BuildIFriend()
            .BuildFriend()
            .WithContainer(_contenderContainer)
            .Build();
        _contender1 = _contenderContainer[0][0];
        _contender2 = _contenderContainer[0][1];
        _visitingContender1 = HallWeb.utils.Mappers.ContenderToVisitingContender(_contender1);
        _visitingContender2 = HallWeb.utils.Mappers.ContenderToVisitingContender(_contender2);
        _best =
            HallWeb.utils.Mappers.ContenderToVisitingContender((_contender1.Value < _contender2.Value)
                ? _contender2
                : _contender1);
    }

    [Test]
    public void ShouldCompareContendersCorrectly()
    {
        _contender1.SetHasVisited();
        _contender2.SetHasVisited();
        _friend.CompareContenders(0, _visitingContender1, _visitingContender2).Should().BeEquivalentTo(_best);
        _friend.CompareContenders(0, _visitingContender2, _visitingContender1).Should().BeEquivalentTo(_best);
    }

    [Test]
    public void ShouldThrowWhenBothHasNotVisited()
    {
        Action act1 = () => _friend.CompareContenders(0, _visitingContender1, _visitingContender2);
        act1.Should().Throw<ApplicationException>();

        Action act2 = () => _friend.CompareContenders(0, _visitingContender2, _visitingContender1);
        act2.Should().Throw<ApplicationException>();
    }

    [Test]
    public void ShouldThrowWhenOneHasNotVisited()
    {
        _contender1.SetHasVisited();

        Action act1 = () => _friend.CompareContenders(0, _visitingContender1, _visitingContender2);
        act1.Should().Throw<ApplicationException>();

        Action act2 = () => _friend.CompareContenders(0, _visitingContender2, _visitingContender1);
        act2.Should().Throw<ApplicationException>();
    }
}