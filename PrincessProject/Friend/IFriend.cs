using PrincessProject.model;

namespace PrincessProject.Friend;

public interface IFriend
{
    Contender CompareContenders(Contender first, Contender second);
}