using TesteDotnet.Domain.Interfaces.Repositories;

namespace TesteDotnet.Domain.Interfaces;

public interface IUnitOfWork
{
    IPessoaRepository PessoaRepository { get; }
    IContatoRepository ContatoRepository { get; }

    Task SaveChangesAsync();
}