using PrincessProject.Data.model;

namespace PrincessProject.PrincessClasses;

/// <summary>
/// Defines princess abstraction, which is able to choose husband
/// </summary>
public interface IPrincess
{
    public void SetAttemptId(int attemptId);

    public Task ResetAttempt();

    public Task AskForNextContender();

    public Task<bool> AssessNextContender(VisitingContender contender);

    public Task<int> SelectContenderAndCommentOnTopic(VisitingContender? chosen);
}