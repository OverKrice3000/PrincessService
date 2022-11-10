using PrincessProject.Data.model;

namespace HallWeb.Friend;

/// <summary>
/// Defines friend abstraction, which is able to compare contenders
/// </summary>
public interface IFriend
{
    /// <summary>
    /// Method, which compares contenders for an attempt id
    /// </summary>
    /// <param name="attemptId">attempt id</param>
    /// <param name="first">first contender to compare</param>
    /// <param name="second">second contender to compare</param>
    /// <returns>contender, which has more value</returns>
    VisitingContender CompareContenders(int attemptId, VisitingContender first, VisitingContender second);
}