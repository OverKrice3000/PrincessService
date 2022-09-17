using PrincessProject.model;

namespace PrincessProject.utils.AttemptLoader;

/*
 * Defines method, which loads program execution data from some source.
 */
public interface IAttemptLoader
{
    /*
     * Defines method, which loads program execution data from some source.
     */
    Attempt Load();
}