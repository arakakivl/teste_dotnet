namespace TesteDotnet.Application.Models.ViewModels;

public class PessoaViewModel
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Nome { get; } = null!;
    public string Email { get; } = null!;
    
    public long CPF { get; }

    // Info that may be null
    public long? CEP { get; set; }
    public string? Logradouro { get; set; } 
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? UF { get; set; }

    public List<ContatoViewModel> Contatos { get; } = new List<ContatoViewModel>();
}