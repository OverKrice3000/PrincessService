using PrincessProject.Data.context;
using PrincessProject.Data.model;

namespace HallWeb.utils.AttemptSaver;

public class DatabaseAttemptSaver : IAttemptSaver
{
    private AttemptContext _context;

    public DatabaseAttemptSaver(AttemptContext context)
    {
        _context = context;
    }

    public async Task Save(Attempt attempt)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            int attemptId = _context.FindLastAttemptId() + 1;
            for (int i = 0; i < attempt.ContendersCount; i++)
            {
                _context.Add(new AttemptEntity(
                    attemptId,
                    attempt.Contenders[i].Name,
                    attempt.Contenders[i].Surname,
                    attempt.Contenders[i].Value,
                    i
                ));
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await transaction.RollbackAsync();
        }
    }
}