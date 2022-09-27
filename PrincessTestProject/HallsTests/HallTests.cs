using PrincessProject.ContenderContainer;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.utils.AttemptLoader;
using PrincessTestProject.Mocks.ContenderContainer;
using PrincessTestProject.utils;

namespace PrincessTestProject.HallsTests;

public class HallTests
{
    private const int ContendersInContainer = 100;
    private IContenderContainer _contenderContainer;
    private IFriend _friend;

    [SetUp]
    public void InitializeHallDependencies()
    {
        _contenderContainer = new MContenderContainer(ContendersInContainer);
        _friend = new Friend(_contenderContainer);
    }

    [Test]
    public void ReturnsNextContenderIfExists()
    {
        IHall hall = new Hall(_friend, new VoidAttemptSaver(), _contenderContainer,
            ContendersInContainer);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            Assert.DoesNotThrow(() => hall.GetNextContender());
        }
    }

    [Test]
    public void ThrowsWhenContenderDoesNotExists()
    {
        IHall hall = new Hall(_friend, new VoidAttemptSaver(), _contenderContainer,
            ContendersInContainer);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            hall.GetNextContender();
        }

        Assert.Throws<ApplicationException>(() => hall.GetNextContender());
    }
}