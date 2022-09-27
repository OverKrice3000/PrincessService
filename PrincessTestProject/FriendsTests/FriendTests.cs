using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessTestProject.Mocks.ContenderContainer;

namespace PrincessTestProject;

public class FriendTests
{
    private const int ContendersInContainer = 100;
    private IContenderContainer _contenderContainer;
    private IContenderGenerator _generator;

    [SetUp]
    public void InitializeFriendDependencies()
    {
        _contenderContainer = new MContenderContainer(ContendersInContainer);
    }

    [Test]
    public void CorrectComparison()
    {
        IFriend friend = new Friend(_contenderContainer);
        Contender contender1 = _contenderContainer[0];
        Contender contender2 = _contenderContainer[1];
        VisitingContender visitingContender1 = Mappers.ContenderToVisitingContender(contender1);
        VisitingContender visitingContender2 = Mappers.ContenderToVisitingContender(contender2);
        VisitingContender best =
            Mappers.ContenderToVisitingContender((contender1.Value < contender2.Value) ? contender2 : contender1);
        contender1.SetHasVisited();
        contender2.SetHasVisited();
        Assert.True(friend.CompareContenders(visitingContender1, visitingContender2).Equals(best));
        Assert.True(friend.CompareContenders(visitingContender2, visitingContender1).Equals(best));
    }

    [Test]
    public void ThrowsWhenContenderHasNotVisited()
    {
        IFriend friend = new Friend(_contenderContainer);
        Contender contender1 = _contenderContainer[0];
        Contender contender2 = _contenderContainer[1];
        VisitingContender visitingContender1 = Mappers.ContenderToVisitingContender(contender1);
        VisitingContender visitingContender2 = Mappers.ContenderToVisitingContender(contender2);
        VisitingContender best =
            Mappers.ContenderToVisitingContender((contender1.Value < contender2.Value) ? contender2 : contender1);
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(visitingContender1, visitingContender2));
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(visitingContender2, visitingContender1));
        contender1.SetHasVisited();
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(visitingContender1, visitingContender2));
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(visitingContender2, visitingContender1));
    }
}