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


    AttemptContainerContext this[int attemptId] { get; }
}