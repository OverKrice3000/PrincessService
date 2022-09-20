using PrincessProject.model;

namespace PrincessProject.ContenderContainer;

/// <summary>
/// Defines container for contenders, which is able to regenerate contenders
/// </summary>
public interface IContenderContainer
{
    /// <summary>
    /// Readonly array of contenders
    /// </summary>
    Contender[] Contenders { get; }

    /// <summary>
    /// Indexer, which returns contender from inner array
    /// </summary>
    /// <param name="index">index of contender in inner array</param>
    public Contender this[int index] { get; }

    /// <summary>
    /// Method, which regenerates inner array of contenders
    /// </summary>
    /// <param name="size">size of new contenders array</param>
    public void Reset(in int size);
}