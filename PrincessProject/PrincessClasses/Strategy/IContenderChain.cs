using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

/// <summary>
/// Custom queue of contenders
/// </summary>
public interface IContenderChain
{
    /// <summary>
    /// Method, containing custom logic to add contender to chain
    /// and return position he has been inserted to
    /// </summary>
    /// <param name="visitingContender">contender to add to chain</param>
    /// <returns>position of added contender in chain</returns>
    int Add(VisitingContender visitingContender);

    /// <summary>
    /// Method, which returns size of the chain
    /// </summary>
    /// <returns>size of the chain</returns>
    int Size();
}