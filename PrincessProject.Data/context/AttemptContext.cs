using Microsoft.EntityFrameworkCore;
using PrincessProject.Data.model;
using PrincessProject.Data.model.data;

namespace PrincessProject.Data.context;

public class AttemptContext : DbContext
{
    public AttemptContext()
    {
    }

    public AttemptContext(DbContextOptions<AttemptContext> options)
        : base(options)
    {
    }

    public DbSet<AttemptEntity> Attempts { get; set; }

    public int FindLastAttemptId()
    {
        return Attempts.Max<AttemptEntity>((data) => data.AttemptId);
    }

    public async Task SaveAttempt(Attempt attempt)
    {
        await using var transaction = await Database.BeginTransactionAsync();
        try
        {
            int attemptId = Attempts.Any() ? FindLastAttemptId() + 1 : 0;
            for (int i = 0; i < attempt.ContendersCount; i++)
            {
                Add(new AttemptEntity(
                    attemptId,
                    attempt.Contenders[i].Name,
                    attempt.Contenders[i].Surname,
                    attempt.Contenders[i].Value,
                    i
                ));
            }

            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await transaction.RollbackAsync();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}