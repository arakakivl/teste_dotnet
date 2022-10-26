namespace TesteDotnet.Application.Models.ViewModels;

public class ContatoViewModel
{
    public Guid PessoaId { get; set; }

    public string Nome { get; set; } = null!;
    public long Celular { get; set; }
}