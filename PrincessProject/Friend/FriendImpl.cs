using PrincessProject.model;

namespace PrincessProject.Friend;

public class FriendImpl : IFriend
{
    public Contender CompareContenders(Contender first, Contender second)
    {
        if (!first.HasVisited || !second.HasVisited)
            throw new ApplicationException("Not so fast baby!");
        return (first.Value < second.Value) ? second : first;
    }
}