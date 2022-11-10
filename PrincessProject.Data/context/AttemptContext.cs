using Microsoft.EntityFrameworkCore;
using PrincessProject.Data.model;

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
        if (!Attempts.Any())
            return -1;
        return Attempts.Max<AttemptEntity>((data) => data.AttemptId);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}