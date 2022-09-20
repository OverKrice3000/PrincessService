using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.ContenderNamesLoader;
using PrincessTestProject.utils;

namespace PrincessTestProject.HallsTests;

public class HallTests
{
    private IAttemptSaver _attemptSaver;
    private IContenderContainer _contenderContainer;
    private IFriend _friend;

    [SetUp]
    public void InitializeHallDependencies()
    {
        var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvNamesColumn });
        var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
        _contenderContainer = new ContenderContainer(new ContenderGenerator(namesLoader, surnamesLoader),
            Constants.PossibleToGenerateContendersAmount);
        _friend = new Friend(_contenderContainer);
        _attemptSaver = new VoidAttemptSaver();
    }

    [Test]
    public void ReturnsNextContenderIfExists()
    {
        IHall hall = new Hall(_friend, _attemptSaver, _contenderContainer,
            Constants.PossibleToGenerateContendersAmount);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            Assert.DoesNotThrow(() => hall.GetNextContender());
        }
    }

    [Test]
    public void ThrowsWhenContenderDoesNotExists()
    {
        IHall hall = new Hall(_friend, _attemptSaver, _contenderContainer,
            Constants.PossibleToGenerateContendersAmount);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            hall.GetNextContender();
        }

        Assert.Throws<ApplicationException>(() => hall.GetNextContender());
    }
}