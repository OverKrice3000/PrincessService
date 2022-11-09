using PrincessProject.Data.model;

namespace HallWeb.utils.AttemptSaver;

public class VoidAttemptSaver : IAttemptSaver
{
    public Task Save(Attempt attempt)
    {
        return Task.CompletedTask;
    }
}