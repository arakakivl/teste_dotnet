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
        throw new NotImplementedException();
    }
}