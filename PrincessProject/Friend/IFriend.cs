using PrincessProject.model;

namespace PrincessProject.Friend;

/*
 * Defines friend abstraction, which is able to compare contenders
 */
public interface IFriend
{
    Contender CompareContenders(Contender first, Contender second);
}