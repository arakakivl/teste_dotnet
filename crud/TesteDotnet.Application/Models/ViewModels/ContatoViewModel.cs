namespace TesteDotnet.Application.Models.ViewModels;

public class ContatoViewModel
{
    public Guid PessoaId { get; init; }

    public string Nome { get; init; } = null!;
    public long Celular { get; init; }
}