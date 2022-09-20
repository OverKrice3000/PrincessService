using PrincessProject.model;

namespace PrincessProject.ContenderGenerator;

/// <summary>
/// Defines contender generator abstraction, which is able to generate
/// an array of contenders
/// </summary>
public interface IContenderGenerator
{
    /// <summary>
    /// Method, which generates array of contenders
    /// </summary>
    /// <param name="size">size of array to generate</param>
    /// <returns>generated array of contenders</returns>
    Contender[] Generate(int size);
}