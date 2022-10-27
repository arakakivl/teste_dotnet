namespace TesteDotnet.Application.Models.ViewModels;

public class PessoaViewModel
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    
    public long CPF { get; init; }

    // Info that may be null
    public long? CEP { get; set; }
    public string? Logradouro { get; set; } 
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? UF { get; set; }

    public List<ContatoViewModel> Contatos { get; init; } = new List<ContatoViewModel>();
}