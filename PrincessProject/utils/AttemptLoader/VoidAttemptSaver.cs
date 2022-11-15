using PrincessProject.model;

namespace PrincessProject.utils.AttemptLoader;

public class VoidAttemptSaver : IAttemptSaver
{
    public Task Save(Attempt attempt)
    {
        return Task.CompletedTask;
    }
}