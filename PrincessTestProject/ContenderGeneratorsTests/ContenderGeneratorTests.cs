using PrincessProject.ContenderGenerator;
using PrincessProject.utils.ContenderNamesLoader;
using PrincessTestProject.utils;

namespace PrincessTestProject;

public class ContenderGeneratorTests
{
    private ITableLoader _namesLoader;
    private ITableLoader _surnamesLoader;

    [SetUp]
    public void InitializeLoaders()
    {
        _namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvNamesColumn });
        _surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
            .WithSeparator(Constants.CsvNamesSurnamesSeparator)
            .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
    }

    [Test]
    public void GeneratesAsMuchAsRequestedWhenPossible()
    {
        IContenderGenerator generator = new ContenderGenerator(_namesLoader, _surnamesLoader);
        var contenders = generator.Generate(Constants.PossibleToGenerateContendersAmount);
        Assert.That(contenders.Length, Is.EqualTo(Constants.PossibleToGenerateContendersAmount));
    }

    [Test]
    public void ThrowsWhenImpossibleToGenerate()
    {
        IContenderGenerator generator = new ContenderGenerator(_namesLoader, _surnamesLoader);
        Assert.Throws<ArgumentException>(() => generator.Generate(Constants.ImpossibleToGenerateContendersAmount));
    }

    [Test]
    public void GeneratesUniqueNames()
    {
        IContenderGenerator generator = new ContenderGenerator(_namesLoader, _surnamesLoader);
        var contenders = generator.Generate(Constants.PossibleToGenerateContendersAmount);
        for (int i = 0; i < contenders.Length; i++)
        {
            for (int j = i + 1; j < contenders.Length; j++)
            {
                Assert.That(contenders[j].FullName, Is.Not.EqualTo(contenders[i].FullName));
            }
        }
    }
}