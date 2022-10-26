namespace TesteDotnet.Domain.Entities;

public class Pessoa
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Nome { get; }
    public string Email { get; }
    
    public long CPF { get; }

    // Info that may be null
    public long? CEP { get; set; }
    public string? Logradouro { get; set; } 
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? UF { get; set; }

    public List<Contato> Contatos { get; } = new List<Contato>();

    public Pessoa(string nome, string email, long cpf)
    {
        CPF = cpf;
        Nome = nome;
        Email = email;
    }
}