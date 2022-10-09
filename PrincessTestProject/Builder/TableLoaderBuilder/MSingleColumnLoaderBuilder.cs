using PrincessProject.utils.ContenderNamesLoader;
using PrincessTestProject.Mocks.ContenderNamesLoaders;

namespace PrincessTestProject.Builder.TableLoaderBuilder;

public class MSingleColumnLoaderBuilder
{
    private string _column = "Default";
    private int _size = 100;

    public MSingleColumnLoaderBuilder WithLoadedColumnName(in string column)
    {
        _column = column;
        return this;
    }

    public MSingleColumnLoaderBuilder WithLoadedFieldsCount(in int size)
    {
        _size = size;
        return this;
    }

    public ITableLoader Build()
    {
        return new MSingleColumnLoader(_column, _size);
    }
}