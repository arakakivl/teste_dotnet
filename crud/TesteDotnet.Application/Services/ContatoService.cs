using TesteDotnet.Application.Extensions;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Application.Services.Interfaces;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces;

namespace TesteDotnet.Application.Services;

public class ContatoService : IContatoService
{
    private readonly IUnitOfWork _unitOfWork;
    public ContatoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateContatoAsync(NewContatoInputModel model)
    {
        Contato c = new(model.PessoaId, model.Nome, model.Celular);

        await _unitOfWork.ContatoRepository.AddAsync(c);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ContatoViewModel?> GetContatoAsync(Guid pessoaId, long celular)
    {
        var r = await _unitOfWork.ContatoRepository.GetContatoAsync(pessoaId, celular);
        if (r is null)
            return null;

        return r.AsViewModel();
    }

    public async Task<List<ContatoViewModel>> GetContatosAsync(Guid? pessoaId)
    {
        if (pessoaId is null)
            return (await _unitOfWork.ContatoRepository.GetEntitiesAsync()).Select(x => x.AsViewModel()).ToList();
        else
            return (await _unitOfWork.PessoaRepository.GetContatos((Guid)pessoaId)).Select(x => x.AsViewModel()).ToList();
    }

    public async Task UpdateContatoAsync(Guid pessoaId, NewContatoInputModel model)
    {
        var data = await _unitOfWork.PessoaRepository.GetContatos(pessoaId);
        var c = data.FirstOrDefault(x => x.Celular == model.OldCelular);

        var contato = new Contato(model.PessoaId, model.Nome, model.Celular);

        await _unitOfWork.ContatoRepository.UpdateAsync(contato);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteContatoAsync(Guid pessoaId, long celular)
    {
        var c = await _unitOfWork.ContatoRepository.GetContatoAsync(pessoaId, celular);
        
        await _unitOfWork.ContatoRepository.DeleteAsync(c!.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}