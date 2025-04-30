using Microsoft.EntityFrameworkCore;
using DiamondsApi.Models;

namespace DiamondsApi.Infrastructure;

public class DiamondDbContext : DbContext
{
    public DiamondDbContext(DbContextOptions<DiamondDbContext> options) : base(options)
    {
    }

    public DbSet<Diamond> Diamonds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
    }
} 