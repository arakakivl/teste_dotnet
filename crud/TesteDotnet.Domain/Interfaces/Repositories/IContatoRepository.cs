using TesteDotnet.Domain.Entities;

namespace TesteDotnet.Domain.Interfaces.Repositories;

public interface IContatoRepository : IBaseRepository<Guid, Contato>
{
    Task<Contato?> GetContatoAsync(Guid pessoaId, long celular);
}