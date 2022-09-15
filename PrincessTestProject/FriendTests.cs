using PrincessProject.model;

namespace PrincessTestProject;
using PrincessProject.Friend;

public class FriendTests
{

    [Test]
    public void CorrectComparison()
    {
        IFriend friend = new FriendImpl();
        Contender contender1 = new Contender("Test1", "Product1", 69);
        Contender contender2 = new Contender("Test2", "Product2", 70);
        contender1.SetHasVisited();
        contender2.SetHasVisited();
        Assert.True(friend.CompareContenders(contender1, contender2).Equals(contender2));
        Assert.True(friend.CompareContenders(contender2, contender1).Equals(contender2));
    }
    
    [Test]
    public void ThrowsWhenContenderHasNotVisited()
    {
        IFriend friend = new FriendImpl();
        Contender contender1 = new Contender("Test1", "Product1", 69);
        Contender contender2 = new Contender("Test2", "Product2", 70);
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(contender1, contender2));
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(contender2, contender1));
        contender1.SetHasVisited();
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(contender1, contender2));
        Assert.Throws<ApplicationException>(() => friend.CompareContenders(contender2, contender1));
    }
}