using PrincessProject.Data.model;

namespace HallWeb.ContenderContainer;

/// <summary>
/// Defines container for contenders, which is able to regenerate contenders
/// </summary>
public interface IContenderContainer
{
    /// <summary>
    /// Dictionary, which contains all contenders of an attempt as value and attempt id as a key
    /// </summary>
    Dictionary<int, AttemptContainerContext> Container { get; }

    /// <summary>
    /// Indexed to retain context for an attempt
    /// </summary>
    /// <param name="attemptId">attempt id</param>
    AttemptContainerContext this[int attemptId] { get; }

    /// <summary>
    /// Method, which returns contender of an attempt with a name
    /// </summary>
    /// <param name="attemptId">id of an attempt</param>
    /// <param name="visitingContender">name of contender</param>
    /// <returns>contender with a name</returns>
    /// <exception cref="ArgumentException">thrown when there is no contender with such name</exception>
    public Contender FindContenderByName(
        int attemptId,
        VisitingContender visitingContender
    );
}