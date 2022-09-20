using PrincessProject.ContenderContainer;
using PrincessProject.Friend;
using PrincessProject.model;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;

namespace PrincessProject.Hall;

public class Hall : IHall
{
    private readonly int _size;
    private IAttemptSaver _attemptSaver;
    private IContenderContainer _contenderContainer;
    private int _nextContender;

    public Hall(
        IFriend friend,
        IAttemptSaver attemptSaver,
        IContenderContainer contenderContainer,
        int size = Constants.DefaultContendersCount
    )
    {
        var random = new Random();
        _size = size;
        Friend = friend;
        _contenderContainer = contenderContainer;
        _contenderContainer.Reset(size);
        _nextContender = 0;
        _attemptSaver = attemptSaver;
    }

    public IFriend Friend { get; }

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
            Console.WriteLine(_contenderContainer[_nextContender].Value);
        }

        Contender nextContender = _contenderContainer[_nextContender++];
        nextContender.SetHasVisited();
        return Mappers.ContenderToVisitingContender(nextContender);
    }

    public void Reset()
    {
        var random = new Random();
        _contenderContainer.Reset(_size);
        _nextContender = 0;
    }

    public int ChooseContender(VisitingContender visitingContender)
    {
        Contender contender = Util.FindContenderByName(_contenderContainer, visitingContender);

        // Throw when princess has chosen not the last assessed contender
        if (!Mappers.ContenderToContenderName(_contenderContainer[_nextContender - 1])
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
            Mappers.ContenderToContenderData(_contenderContainer.Contenders),
            null,
            happiness
        ));
    }

    public void SetAttemptSaver(IAttemptSaver attemptSaver)
    {
        _attemptSaver = attemptSaver;
    }
}