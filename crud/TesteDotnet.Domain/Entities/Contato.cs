namespace TesteDotnet.Domain.Entities;

public class Contato
{
    public Guid PessoaId { get; set; }

    public string Nome { get; set; }
    public long Celular { get; set; }

    public Contato(Guid pessoaId, string nome, long celular)
    {
        PessoaId = pessoaId;
        Nome = nome;
        Celular = celular;
    }
}