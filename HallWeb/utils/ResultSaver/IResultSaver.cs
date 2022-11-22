using PrincessProject.Data.model.data;

namespace HallWeb.utils.ResultSaver;

/*
 * Defines method, which saves program execution data to some source.
 */
public interface IResultSaver
{
    /*
     * Method, which saves program execution data to some source.
     */
    Task Save(Result result);
}