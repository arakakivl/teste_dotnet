using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;

namespace TesteDotnet.Application.Services.Interfaces;

public interface IPessoaService
{
    Task<Guid> CreatePessoaAsync(NewPessoaInputModel model);

    Task<PessoaViewModel?> GetPessoaAsync(Guid id);
    Task<List<PessoaViewModel>> GetPessoasAsync();

    Task UpdatePessoaAsync(Guid id, NewPessoaInputModel model);
    Task DeletePessoaAsync(Guid id);
}