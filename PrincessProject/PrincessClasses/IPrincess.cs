namespace PrincessProject.PrincessClasses;

/// <summary>
/// Defines princess abstraction, which is able to choose husband
/// </summary>
public interface IPrincess
{
    /// <summary>
    /// Method, which makes princess assess all the contenders,
    /// and optionally choose one
    /// </summary>
    /// <returns>happiness of princess</returns>
    Task<int> ChooseHusband();

    /// <summary>
    /// Sets attempt id used to interact with hall web application
    /// </summary>
    /// <param name="attemptId">attempt id</param>
    public void SetAttemptId(int attemptId);
}