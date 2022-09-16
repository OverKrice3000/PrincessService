using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.ContenderNamesLoader;

namespace PrincessProject.ContenderGenerator;

public class ContenderGenerator : IContenderGenerator
{
    private ITableLoader _namesLoader;
    private ITableLoader _surnamesLoader;

    public ContenderGenerator(ITableLoader namesLoader, ITableLoader surnamesLoader)
    {
        _namesLoader = namesLoader;
        _surnamesLoader = surnamesLoader;
    }
    
    public void SetNamesLoader(ITableLoader loader)
    {
        _namesLoader = loader;
    }
    
    public void SetSurnamesLoader(ITableLoader loader)
    {
        _surnamesLoader = loader;
    }

    public Contender[] Generate(int size = Constants.DefaultContendersCount)
    {
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