using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Domain.Entities;

namespace TesteDotnet.Application.Extensions;

public static class Extensions
{
    public static PessoaViewModel AsViewModel(this Pessoa p)
    {
        return new PessoaViewModel()
        {
            Id = p.Id,
            Nome = p.Nome,
            Email = p.Email,
            CPF = p.CPF,
            CEP = p.CEP,
            Logradouro = p.Logradouro,
            Complemento = p.Complemento,
            Bairro = p.Bairro,
            UF = p.UF,
            Contatos = p.Contatos.Select(x => x.AsViewModel()).ToList()
        };
    }

    public static ContatoViewModel AsViewModel(this Contato c)
    {
        return new ContatoViewModel()
        {
            PessoaId = c.PessoaId,
            Nome = c.Nome,
            Celular = c.Celular
        };
    }
}