using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy;

/// <summary>
/// Interface, which describes custom logic for contenders assessment
/// </summary>
public interface IStrategy
{
    /// <summary>
    /// Method, which either approves or rejects a contender by his name
    /// </summary>
    /// <param name="visitingContender">contender to be assessed</param>
    /// <returns>recommendation whether to choose contender or not</returns>
    bool AssessNextContender(VisitingContender visitingContender);
}