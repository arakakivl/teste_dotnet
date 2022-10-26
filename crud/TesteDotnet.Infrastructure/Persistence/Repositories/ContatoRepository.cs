using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces.Repositories;

namespace TesteDotnet.Infrastructure.Persistence.Repositories;

public class ContatoRepository : BaseRepository<int, Contato>, IContatoRepository
{
    public ContatoRepository(AppDbContext context) : base(context)
    {
        
    }
}