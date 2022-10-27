using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces.Repositories;

namespace TesteDotnet.Infrastructure.Persistence.Repositories;

public class ContatoRepository : BaseRepository<Guid, Contato>, IContatoRepository
{
    public ContatoRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<Contato?> GetContatoAsync(Guid pessoaId, long celular)
    {
        throw new NotImplementedException();
    }
}