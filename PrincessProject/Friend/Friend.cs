using PrincessProject.ContenderContainer;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.Friend;

public class Friend : IFriend
{
    private IContenderContainer _contenderContainer;

    public Friend(IContenderContainer contenderContainer)
    {
        _contenderContainer = contenderContainer;
    }

    public VisitingContender CompareContenders(VisitingContender first, VisitingContender second)
    {
        Contender firstCondender = Util.FindContenderByName(_contenderContainer, first);
        Contender secondCondender = Util.FindContenderByName(_contenderContainer, second);
        if (!firstCondender.HasVisited || !secondCondender.HasVisited)
            throw new ApplicationException("Not so fast baby!");
        return (firstCondender.Value < secondCondender.Value) ? second : first;
    }
}