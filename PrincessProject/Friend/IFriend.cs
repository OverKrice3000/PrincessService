using PrincessProject.model;

namespace PrincessProject.Friend;

/*
 * Defines friend abstraction, which is able to compare contenders
 */
public interface IFriend
{
    VisitingContender CompareContenders(VisitingContender first, VisitingContender second);
}