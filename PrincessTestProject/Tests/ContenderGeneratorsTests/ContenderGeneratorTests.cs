using PrincessProject.ContenderGenerator;
using PrincessProject.utils.ContenderNamesLoader;
using PrincessTestProject.Builder;
using PrincessTestProject.utils;

namespace PrincessTestProject;

public class ContenderGeneratorTests
{
    private ITableLoader _namesLoader;
    private ITableLoader _surnamesLoader;

    [SetUp]
    public void InitializeLoaders()
    {
        _namesLoader = TestBuilder.BuildITableLoader()
            .BuildMTableLoader()
            .WithLoadedColumnName(PrincessProject.utils.Constants.CsvNamesColumn)
            .WithLoadedFieldsCount(Constants.PossibleToGenerateContendersAmount)
            .Build();

        _surnamesLoader = TestBuilder
            .BuildITableLoader()
            .BuildMTableLoader()
            .WithLoadedColumnName(PrincessProject.utils.Constants.CsvSurnamesColumn)
            .WithLoadedFieldsCount(Constants.PossibleToGenerateContendersAmount)
            .Build();
    }

    [Test]
    public void GeneratesAsMuchAsRequestedWhenPossible()
    {
        IContenderGenerator generator = TestBuilder
            .BuildIContenderGenerator()
            .BuildContenderGenerator()
            .WithNamesLoader(_namesLoader)
            .WithSurnamesLoader(_surnamesLoader)
            .Build();
        var contenders = generator.Generate(Constants.PossibleToGenerateContendersAmount);
        Assert.That(contenders.Length, Is.EqualTo(Constants.PossibleToGenerateContendersAmount));
    }

    [Test]
    public void ThrowsWhenImpossibleToGenerate()
    {
        IContenderGenerator generator = TestBuilder
            .BuildIContenderGenerator()
            .BuildContenderGenerator()
            .WithNamesLoader(_namesLoader)
            .WithSurnamesLoader(_surnamesLoader)
            .Build();
        Assert.Throws<ArgumentException>(() => generator.Generate(Constants.ImpossibleToGenerateContendersAmount));
    }

    [Test]
    public void GeneratesUniqueNames()
    {
        IContenderGenerator generator = TestBuilder
            .BuildIContenderGenerator()
            .BuildContenderGenerator()
            .WithNamesLoader(_namesLoader)
            .WithSurnamesLoader(_surnamesLoader)
            .Build();
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