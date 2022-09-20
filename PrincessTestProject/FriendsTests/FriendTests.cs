using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.ContenderNamesLoader;
using Constants = PrincessTestProject.utils.Constants;

namespace PrincessTestProject;

public class FriendTests
{
    private IContenderContainer _contenderContainer;
    private IContenderGenerator _generator;

    [SetUp]
    public void InitializeFriendDependencies()
    {
        var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvNamesColumn });
        var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
        _generator = new ContenderGenerator(namesLoader, surnamesLoader);
        _contenderContainer = new ContenderContainer(_generator, Constants.PossibleToGenerateContendersAmount);
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