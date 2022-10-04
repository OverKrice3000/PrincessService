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

    public DbSet<AttemptDto> Attempts { get; set; }
    public DbSet<AttemptDataDto> AttemptData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}