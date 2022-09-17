using System.Security.AccessControl;
using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;

namespace PrincessProject.Hall;

public class Hall : IHall
{
    private readonly int _size;
    private readonly IContenderGenerator _contenderGenerator;
    private readonly IFriend _friend;
    private Contender[] _contenders;
    private int _nextContender = 0;
    private IAttemptSaver _attemptSaver;

    
    public Hall(
        IContenderGenerator generator,
        IFriend friend,
        IAttemptSaver attemptSaver,
        int size = Constants.DefaultContendersCount
    )
    {
        _size = size;
        _friend = friend;
        _contenderGenerator = generator;
        var random = new Random();
        _contenders = _contenderGenerator.Generate(size)
            .OrderBy(item => random.Next())
            .ToArray();
        _nextContender = 0;
        _attemptSaver = attemptSaver;
    }

    public void SetAttemptSaver(IAttemptSaver attemptSaver)
    {
        _attemptSaver = attemptSaver;
    }
    
    public int GetTotalCandidates()
    {
        return _size;
    }

    public ContenderName GetNextContender()
    {
        if (_size == _nextContender)
            throw new ApplicationException("No more contenders!");
        if (Constants.DebugMode)
        {
            Console.WriteLine("NEXT CONTENDER IS:");
            Console.WriteLine(_contenders[_nextContender].Value);
        }
        Contender nextContender = _contenders[_nextContender++];
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

    public void Reset()
    {
        var random = new Random();
        _contenders = _contenderGenerator.Generate(_size)
            .OrderBy(item => random.Next())
            .ToArray();
        _nextContender = 0;
    }
    
    public int ChooseContender(ContenderName contenderName)
    {
        Contender contender = _findContenderByName(contenderName);
        
        // Throw when princess has chosen not the last assessed contender
        if (!_formNameFromContender(_contenders[_nextContender - 1]).Equals(_formNameFromContender(contender)))
        {
            throw new ApplicationException("Princess is trying to cheat!");
        }
        
        return contender.Value;
    }

    public void SaveAttempt(int happiness)
    {
        _attemptSaver.Save(new Attempt(
            Constants.DefaultContendersCount,
            Mappers.ContenderToContenderData(_contenders),
            null,
            happiness
        ));
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
