using TesteDotnet.Domain.Entities;

namespace TesteDotnet.Domain.Interfaces.Repositories;

public interface IPessoaRepository : IBaseRepository<Guid, Pessoa>
{
    Task<List<Contato>> GetContatos(Guid id);
}