using Microsoft.EntityFrameworkCore;
using TesteDotnet.Domain.Entities;

namespace TesteDotnet.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Pessoa>()
            .HasKey(p => p.Id);

        builder.Entity<Contato>()
            .HasKey(c => c.Id);

        builder.Entity<Pessoa>()
            .HasMany(p => p.Contatos)
                .WithOne()
                    .HasForeignKey(c => c.PessoaId);
    }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Contato> Contatos => Set<Contato>();
}