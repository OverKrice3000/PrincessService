using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.ContenderNamesLoader;

namespace PrincessProject.ContenderGenerator;

public class ContenderGenerator : IContenderGenerator
{
    private List<string>? _names;
    private ITableLoader _namesLoader;
    private List<string>? _surnames;
    private ITableLoader _surnamesLoader;

    public ContenderGenerator(ITableLoader namesLoader, ITableLoader surnamesLoader)
    {
        _namesLoader = namesLoader;
        _surnamesLoader = surnamesLoader;
    }

    public Contender[] Generate(int size = Constants.DefaultContendersCount)
    {
        if (size < 0)
        {
            throw new ArgumentException($"Bad size: {size}");
        }

        if (_names is null)
            _names = this._namesLoader
                .Load()[Constants.CsvNamesColumn];
        if (_surnames is null)
            _surnames = this._surnamesLoader
                .Load()[Constants.CsvSurnamesColumn];
        var names = new List<string>(_names);
        var surnames = new List<string>(_surnames);
        if (names.Count < size || surnames.Count < size)
        {
            throw new ArgumentException("Cannot generate given amount of candidate names!");
        }

        var random = new Random((int)DateTime.Now.Ticks);
        var contenders = new Contender[size];
        for (int i = 0; i < size; i++)
        {
            int namesNextIndex = random.Next(names.Count);
            int surnamesNextIndex = random.Next(surnames.Count);
            contenders[i] = new Contender(
                names[random.Next(namesNextIndex)],
                surnames[surnamesNextIndex],
                i + 1
            );
            names.RemoveAt(namesNextIndex);
            surnames.RemoveAt(surnamesNextIndex);
        }

        return contenders;
    }
}