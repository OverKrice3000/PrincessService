using PrincessProject.utils.ContenderNamesLoader;

namespace PrincessTestProject.Mocks.ContenderNamesLoaders;

public class MSingleColumnLoader : ITableLoader
{
    private string _column;
    private int _count;

    public MSingleColumnLoader(string column, int count)
    {
        _column = column;
        _count = count;
    }

    public Dictionary<string, List<string>> Load()
    {
        var valueBase = _column.ToUpper();
        var list = new List<string>(_count);
        for (int i = 0; i < _count; i++)
        {
            list.Add($"{valueBase}{i}");
        }

        var dictionary = new Dictionary<string, List<string>>(1);
        dictionary.Add(_column, list);
        return dictionary;
    }
}