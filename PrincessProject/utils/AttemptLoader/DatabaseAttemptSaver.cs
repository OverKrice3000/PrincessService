using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessProject.model;

namespace PrincessProject.utils.AttemptLoader;

public class DatabaseAttemptSaver : IAttemptSaver
{
    private AttemptContext _context;

    public DatabaseAttemptSaver(AttemptContext context)
    {
        _context = context;
    }

    public async Task Save(Attempt attempt)
    {
        await using var transaction = _context.Database.BeginTransaction();
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
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}