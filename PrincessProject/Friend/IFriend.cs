using PrincessProject.model;

namespace PrincessProject.Friend;

/// <summary>
/// Defines friend abstraction, which is able to compare contenders
/// </summary>
public interface IFriend
{
    /// <summary>
    /// Method, which compares contenders
    /// </summary>
    /// <param name="first">first contender to compare</param>
    /// <param name="second">second contender to compare</param>
    /// <returns>contender, which has more value</returns>
    VisitingContender CompareContenders(VisitingContender first, VisitingContender second);
}