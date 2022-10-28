using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces;
using TesteDotnet.Domain.Interfaces.Repositories;

namespace TesteDotnet.Infrastructure.Persistence.Repositories;

public class PessoaRepository : BaseRepository<Guid, Pessoa>, IPessoaRepository
{
    public PessoaRepository(AppDbContext context) : base(context)
    {
        
    }
    
    public async Task<List<Contato>> GetContatos(Guid id)
    {
        var pessoa = await DbSet.FindAsync(id);

        // Before reaching this call, we're going to verify if this 'pessoa' exists or not in the database.
        if (pessoa is null)
            return new List<Contato>();
        else
            return pessoa.Contatos;
    }
}