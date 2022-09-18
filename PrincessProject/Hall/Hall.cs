using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;

namespace PrincessProject.Hall;

public class Hall : IHall
{
    private readonly IContenderGenerator _contenderGenerator;
    private readonly IFriend _friend;
    private readonly int _size;
    private IAttemptSaver _attemptSaver;
    private Contender[] _contenders;
    private int _nextContender = 0;


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

    public int GetTotalCandidates()
    {
        return _size;
    }

    public VisitingContender GetNextContender()
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
        return Mappers.ContenderToVisitingContender(nextContender);
    }

    public VisitingContender AskFriendToCompareContenders(VisitingContender first, VisitingContender second)
    {
        return Mappers.ContenderToVisitingContender(
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

    public int ChooseContender(VisitingContender visitingContender)
    {
        Contender contender = _findContenderByName(visitingContender);

        // Throw when princess has chosen not the last assessed contender
        if (!Mappers.ContenderToContenderName(_contenders[_nextContender - 1])
                .Equals(Mappers.ContenderToContenderName(contender)))
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

    public void SetAttemptSaver(IAttemptSaver attemptSaver)
    {
        _attemptSaver = attemptSaver;
    }

    private Contender _findContenderByName(VisitingContender visitingContender)
    {
        return Array.Find(
                   _contenders,
                   contender => contender.FullName.Equals(visitingContender.FullName)
               ) ??
               throw new ArgumentException("No contender with such name!");
    }
}