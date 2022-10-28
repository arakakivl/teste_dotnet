using TesteDotnet.Application.Extensions;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Application.Services.Interfaces;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces;

namespace TesteDotnet.Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IUnitOfWork _unitOfWork;
    public PessoaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> CreatePessoaAsync(NewPessoaInputModel model)
    {
        var c = new Pessoa(model.Nome, model.Email, model.CPF)
        {
            CEP = model.CEP,
            Logradouro = model.Logradouro,
            Bairro = model.Bairro,
            Complemento = model.Complemento,
            UF = model.UF,
        };

        var id = await _unitOfWork.PessoaRepository.AddAsync(c);
        await _unitOfWork.SaveChangesAsync();

        return id;
    }

    public async Task<PessoaViewModel?> GetPessoaAsync(Guid id)
    {
        var r = await _unitOfWork.PessoaRepository.GetEntityAsync(id);
        if (r is null)
            return null;
        
        return r.AsViewModel();
    }

    public async Task<List<PessoaViewModel>> GetPessoasAsync()
    {
        var data = await _unitOfWork.PessoaRepository.GetEntitiesAsync();
        return data.Select(x => x.AsViewModel()).ToList();
    }

    public async Task UpdatePessoaAsync(Guid id, NewPessoaInputModel model)
    {
        var p = new Pessoa(model.Nome, model.Email, model.CPF)
        {
            Id = id,
            CEP = model.CEP,
            Logradouro = model.Logradouro,
            Bairro = model.Bairro,
            Complemento = model.Complemento,
            UF = model.UF,
        };

        await _unitOfWork.PessoaRepository.UpdateAsync(p);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePessoaAsync(Guid id)
    {
        await _unitOfWork.PessoaRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}