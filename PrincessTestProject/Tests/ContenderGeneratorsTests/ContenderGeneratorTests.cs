using PrincessProject.ContenderGenerator;
using PrincessProject.utils.ContenderNamesLoader;
using PrincessTestProject.Builder;

namespace PrincessTestProject;

public class ContenderGeneratorTests
{
    private const int LoadedFieldsCount = 100;
    private IContenderGenerator _generator;
    private ITableLoader _namesLoader;
    private ITableLoader _surnamesLoader;

    [SetUp]
    public void InitializeLoaders()
    {
        _namesLoader = TestBuilder.BuildITableLoader()
            .BuildMTableLoader()
            .WithLoadedColumnName(PrincessProject.utils.Constants.CsvNamesColumn)
            .WithLoadedFieldsCount(LoadedFieldsCount)
            .Build();

        _surnamesLoader = TestBuilder
            .BuildITableLoader()
            .BuildMTableLoader()
            .WithLoadedColumnName(PrincessProject.utils.Constants.CsvSurnamesColumn)
            .WithLoadedFieldsCount(LoadedFieldsCount)
            .Build();

        _generator = TestBuilder
            .BuildIContenderGenerator()
            .BuildContenderGenerator()
            .WithNamesLoader(_namesLoader)
            .WithSurnamesLoader(_surnamesLoader)
            .Build();
    }

    [
        TestCase(10),
        TestCase(50),
        TestCase(100)
    ]
    public void GeneratesAsMuchAsRequestedWhenPossible(int countToGenerate)
    {
        var contenders = _generator.Generate(countToGenerate);
        Assert.That(contenders.Length, Is.EqualTo(countToGenerate));
    }

    [
        TestCase(101),
        TestCase(501),
        TestCase(1001)
    ]
    public void ThrowsWhenImpossibleToGenerate(int countToGenerate)
    {
        Assert.Throws<ArgumentException>(() => _generator.Generate(countToGenerate));
    }

    [Test]
    public void GeneratesUniqueNames()
    {
        var contenders = _generator.Generate(LoadedFieldsCount);
        for (int i = 0; i < contenders.Length; i++)
        {
            for (int j = i + 1; j < contenders.Length; j++)
            {
                Assert.That(contenders[j].FullName, Is.Not.EqualTo(contenders[i].FullName));
            }
        }
    }
}