namespace PrincessTestProject.Builder.AttemptSaverBuilder;

public class AttemptSaverInterfaceBuilder
{
    public DatabaseAttemptSaverBuilder BuildDatabaseAttemptSaver()
    {
        return new DatabaseAttemptSaverBuilder();
    }
}