using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.utils;
using PrincessProject.utils.ContenderNamesLoader;

namespace PrincessTestProject.Builder.ContenderGeneratorBuilder;

public class ContenderGeneratorBuilder
{
    private ITableLoader _namesLoader = TestBuilder
        .BuildITableLoader()
        .BuildMTableLoader()
        .WithLoadedColumnName(Constants.CsvNamesColumn)
        .Build();

    private ITableLoader _surnamesLoader = TestBuilder
        .BuildITableLoader()
        .BuildMTableLoader()
        .WithLoadedColumnName(Constants.CsvSurnamesColumn)
        .Build();

    public ContenderGeneratorBuilder WithNamesLoader(ITableLoader namesLoader)
    {
        _namesLoader = namesLoader;
        return this;
    }

    public ContenderGeneratorBuilder WithSurnamesLoader(ITableLoader surnamesLoader)
    {
        _surnamesLoader = surnamesLoader;
        return this;
    }

    public IContenderGenerator Build()
    {
        return new ContenderGenerator(_namesLoader, _surnamesLoader);
    }
}