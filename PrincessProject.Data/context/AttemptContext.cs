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

    public DbSet<AttemptDto> AttemptData { get; set; }

    public int FindLastAttemptId()
    {
        return AttemptData.Max<AttemptDto>((data) => data.AttemptId);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}