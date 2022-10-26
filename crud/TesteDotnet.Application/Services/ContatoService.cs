using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Application.Services.Interfaces;
using TesteDotnet.Domain.Interfaces;

namespace TesteDotnet.Application.Services;

public class ContatoService : IContatoService
{
    private readonly IUnitOfWork _unitOfWork;
    public ContatoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task CreateContatoAsync(NewContatoInputModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteContatoAsync(Guid pessoaId, long celular)
    {
        throw new NotImplementedException();
    }

    public Task<ContatoViewModel> GetContatoAsync(Guid pessoaId, long celular)
    {
        throw new NotImplementedException();
    }

    public Task<List<ContatoViewModel>> GetContatosAsync(Guid? pessoaId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContatoAsync(Guid pessoaId, NewContatoInputModel model)
    {
        throw new NotImplementedException();
    }
}