using System.ComponentModel.DataAnnotations;

namespace TesteDotnet.Application.Models.InputModels;

public class NewPessoaInputModel
{
    [Required]
    public string Nome { get; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; } = null!;
    
    [Required]
    public long CPF { get; }

    // Info that may be null
    public long? CEP { get; set; }
    public string? Logradouro { get; set; } 
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? UF { get; set; }
}