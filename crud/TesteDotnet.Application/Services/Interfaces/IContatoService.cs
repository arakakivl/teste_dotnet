using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;

namespace TesteDotnet.Application.Services.Interfaces;

public interface IContatoService
{
    Task CreateContatoAsync(NewContatoInputModel model);

    Task<ContatoViewModel> GetContatoAsync(Guid pessoaId, long celular);
    Task<List<ContatoViewModel>> GetContatosAsync(Guid? pessoaId);

    Task UpdateContatoAsync(Guid pessoaId, NewContatoInputModel model);
    Task DeleteContatoAsync(Guid pessoaId, long celular);
}