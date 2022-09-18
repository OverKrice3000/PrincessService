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
    private IFriend _friend;
    private IContenderGenerator _generator;

    [SetUp]
    public void InitializeHallDependencies()
    {
        var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvNamesColumn });
        var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
        _generator = new ContenderGenerator(namesLoader, surnamesLoader);
        _friend = new Friend();
        _attemptSaver = new VoidAttemptSaver();
    }

    [Test]
    public void ReturnsNextContenderIfExists()
    {
        IHall hall = new Hall(_generator, _friend, _attemptSaver, Constants.PossibleToGenerateContendersAmount);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            Assert.DoesNotThrow(() => hall.GetNextContender());
        }
    }

    [Test]
    public void ThrowsWhenContenderDoesNotExists()
    {
        IHall hall = new Hall(_generator, _friend, _attemptSaver, Constants.PossibleToGenerateContendersAmount);
        for (int i = 0; i < Constants.PossibleToGenerateContendersAmount; i++)
        {
            hall.GetNextContender();
        }

        Assert.Throws<ApplicationException>(() => hall.GetNextContender());
    }
}