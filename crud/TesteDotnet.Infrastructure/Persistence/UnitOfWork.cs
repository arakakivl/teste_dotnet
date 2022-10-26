using TesteDotnet.Domain.Interfaces;
using TesteDotnet.Domain.Interfaces.Repositories;
using TesteDotnet.Infrastructure.Persistence.Repositories;

namespace TesteDotnet.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IPessoaRepository PessoaRepository { get; set; }
    public IContatoRepository ContatoRepository { get; set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        PessoaRepository ??= new PessoaRepository(context);
        ContatoRepository ??= new ContatoRepository(context);
    }

    public async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}