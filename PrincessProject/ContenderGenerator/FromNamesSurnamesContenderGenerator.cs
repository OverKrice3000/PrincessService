using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.ContenderNamesLoader;

namespace PrincessProject.ContenderGenerator;

public class FromNamesSurnamesContenderGenerator : IContenderGenerator
{
    private ITableLoader? _namesLoader;
    private ITableLoader? _surnamesLoader;

    public FromNamesSurnamesContenderGenerator WithNamesLoader(ITableLoader loader)
    {
        this._namesLoader = loader;
        return this;
    }
    public FromNamesSurnamesContenderGenerator WithSurnamesLoader(ITableLoader loader)
    {
        this._surnamesLoader = loader;
        return this;
    }

    public Contender[] Generate(int size = 100)
    {
        if (this._namesLoader is null || this._surnamesLoader is null)
        {
            throw new ArgumentException("Loaders are not set!");
        }
        var names = this._namesLoader
            .Load()[Constants.CsvNamesColumn];
        var surnames = this._surnamesLoader
            .Load()[Constants.CsvSurnamesColumn];
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