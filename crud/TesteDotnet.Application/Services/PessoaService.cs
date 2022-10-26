using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Application.Services.Interfaces;
using TesteDotnet.Domain.Interfaces;

namespace TesteDotnet.Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IUnitOfWork _unitOfWork;
    public PessoaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<Guid> CreatePessoaAsync(NewPessoaInputModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeletePessoaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PessoaViewModel?> GetPessoaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PessoaViewModel>> GetPessoasAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdatePessoaAsync(Guid id, NewPessoaInputModel model)
    {
        throw new NotImplementedException();
    }
}