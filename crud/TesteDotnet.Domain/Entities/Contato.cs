namespace TesteDotnet.Domain.Entities;

public class Contato
{
    public Guid PessoaId { get; set; }

    public string Nome { get; set; }
    public int Celular { get; set; }

    public Contato(Guid pessoaId, string nome, int celular)
    {
        PessoaId = pessoaId;
        Nome = nome;
        Celular = celular;
    }
}