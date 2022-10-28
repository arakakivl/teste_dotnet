using System.ComponentModel.DataAnnotations;

namespace TesteDotnet.Application.Models.InputModels;

public class NewContatoInputModel
{
    [Required]
    public Guid PessoaId { get; set; }

    [Required]
    public string Nome { get; set; } = null!;

    [Phone]
    public long OldCelular { get; set; }

    [Phone]
    public long Celular { get; set; }
}