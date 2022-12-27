namespace HallWeb.utils.ContenderNamesLoader;

/*
 * Table Loader
 */
public interface ITableLoader
{
    /*
     * Method, containing custom logic to load the table from some source
     */
    Dictionary<string, List<string>> Load();
}