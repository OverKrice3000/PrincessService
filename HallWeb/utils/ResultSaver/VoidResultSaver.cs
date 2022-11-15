using PrincessProject.Data.model.data;

namespace HallWeb.utils.ResultSaver;

public class VoidResultSaver : IResultSaver
{
    public Task Save(Result result)
    {
        return Task.CompletedTask;
    }
}