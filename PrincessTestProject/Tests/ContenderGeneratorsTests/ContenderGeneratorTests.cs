using FluentAssertions;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.utils.ContenderNamesLoader;
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
            .WithLoadedColumnName(HallWeb.utils.Constants.CsvNamesColumn)
            .WithLoadedFieldsCount(LoadedFieldsCount)
            .Build();

        _surnamesLoader = TestBuilder
            .BuildITableLoader()
            .BuildMTableLoader()
            .WithLoadedColumnName(HallWeb.utils.Constants.CsvSurnamesColumn)
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
        TestCase(0),
        TestCase(LoadedFieldsCount)
    ]
    public void ShouldGenerateAsMuchAsRequestedWhenPossible(int countToGenerate)
    {
        var contenders = _generator.Generate(countToGenerate);
        countToGenerate.Should().Be(contenders.Length);
    }

    [
        TestCase(LoadedFieldsCount + 1),
        TestCase(-1)
    ]
    public void ShouldThrowWhenImpossibleToGenerate(int countToGenerate)
    {
        Action act = () => _generator.Generate(countToGenerate);
        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void ShouldGenerateUniqueItems()
    {
        var contenders = _generator.Generate(LoadedFieldsCount).Select((contender) => contender.FullName);
        contenders.Should().OnlyHaveUniqueItems();
    }
}