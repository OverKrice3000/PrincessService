using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.Hall;

public class HallImpl : IHall
{
    private readonly int _size;
    private readonly Contender[] _contenders;
    private int _currentContender = 0;
    private readonly IFriend _friend;
    
    public HallImpl(
        IContenderGenerator generator,
        IFriend friend,
        int size = Constants.DefaultContendersCount
    )
    {
        _size = size;
        _friend = friend;
        var random = new Random();
        _contenders = generator.Generate(size)
            .OrderBy(item => random.Next())
            .ToArray();
    }
    public int GetTotalCandidates()
    {
        return this._size;
    }

    public ContenderName GetNextContender()
    {
        if (_size == _currentContender)
            throw new ApplicationException("No more contenders!");
        if (Constants.DebugMode)
        {
            Console.WriteLine("NEXT CONTENDER IS:");
            Console.WriteLine(_contenders[_currentContender].Value);
        }
        Contender nextContender = _contenders[_currentContender++];
        nextContender.SetHasVisited();
        return _formNameFromContender(nextContender);
    }

    public ContenderName CompareContenders(ContenderName first, ContenderName second)
    {
        return _formNameFromContender(
            _friend.CompareContenders
                (
                    _findContenderByName(first),
                    _findContenderByName(second)
                )
            );
    }
    
    public void ChooseContenderAndCalculateHappiness(ContenderName? contenderName)
    {
        if (contenderName is null)
        {
            Console.WriteLine("Princess hasn't chosen anyone!");
            Console.WriteLine("Her happiness level: " + Constants.NoHusbandHappinessLevel);
            return;
        }
        Contender contender = _findContenderByName(contenderName);
        if (!_formNameFromContender(_contenders[_currentContender - 1]).Equals(_formNameFromContender(contender)))
        {
            throw new ApplicationException("Princess is trying to cheat!");
        }

        if (contender.Value <= _size * Constants.IdiotHusbandTopBorderPercentage)
        {
            Console.WriteLine("Princess has chosen an idiot husband: " + contenderName.Name + " " + contenderName.Surname);
            Console.WriteLine("Her happiness level: " + Constants.IdiotHusbandHappinessLevel);
            return;
        }
        Console.WriteLine("Princess has chosen a worthy contender: " + contenderName.Name + " " + contenderName.Surname);
        Console.WriteLine("Her happiness level: " + contender.Value);
    }

    private Contender _findContenderByName(ContenderName contenderName)
    {
        return Array.Find(
                   _contenders, 
              contender => contender.Name == contenderName.Name && contender.Surname == contenderName.Surname
            ) ?? 
               throw new ArgumentException("No contender with such name!");
    }

    private ContenderName _formNameFromContender(Contender contender)
    {
        return new ContenderName(contender.Name, contender.Surname);
    }
    
}
