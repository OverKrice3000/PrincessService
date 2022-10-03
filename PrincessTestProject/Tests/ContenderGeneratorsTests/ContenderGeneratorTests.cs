using FluentAssertions;
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
        countToGenerate.Should().Be(contenders.Length);
    }

    [
        TestCase(101),
        TestCase(501),
        TestCase(1001)
    ]
    public void ThrowsWhenImpossibleToGenerate(int countToGenerate)
    {
        Action act = () => _generator.Generate(countToGenerate);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void GeneratesUniqueNames()
    {
        var contenders = _generator.Generate(LoadedFieldsCount).Select((contender) => contender.FullName);
        contenders.Should().OnlyHaveUniqueItems();
    }
}