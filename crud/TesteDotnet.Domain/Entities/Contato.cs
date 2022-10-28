namespace TesteDotnet.Domain.Entities;

public class Contato
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid PessoaId { get; init; }

    public string Nome { get; init; }
    public long Celular { get; init; }

    public Contato(Guid pessoaId, string nome, long celular)
    {
        PessoaId = pessoaId;
        Nome = nome;
        Celular = celular;
    }
}