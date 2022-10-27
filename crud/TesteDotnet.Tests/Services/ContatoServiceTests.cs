using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Services;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces;
using Xunit;
using TesteDotnet.Infrastructure.Persistence.Repositories;

namespace TesteDotnet.Tests.Services;

public class ContatoServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkStub;
    private readonly ContatoService _sut;

    public ContatoServiceTests()
    {
        _unitOfWorkStub = new();
        _sut = new(_unitOfWorkStub.Object);
    }

    [Fact]
    public async Task CreateContatoAsync_ShouldReturnId_WhenExecuted()
    {
        // Arrange
        NewContatoInputModel model = new()
        {
            PessoaId = Guid.NewGuid(),
            Nome = "",
            Celular = 1111123456789
        };

        _unitOfWorkStub.Setup(x => x.ContatoRepository.AddAsync(It.IsAny<Contato>())).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        await _sut.CreateContatoAsync(model);

        // Assert
        _unitOfWorkStub.Verify(x => x.ContatoRepository.AddAsync(It.IsAny<Contato>()), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetContatoAsync_ShouldReturnContato_WhenContatoFound()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 1111123456789);
        _unitOfWorkStub.Setup(x => x.ContatoRepository.GetContatoAsync(contato.PessoaId, contato.Celular)).ReturnsAsync(contato);

        // Act
        var result = await _sut.GetContatoAsync(contato.PessoaId, contato.Celular);

        // Assert
        Assert.Equal(contato.PessoaId, result!.PessoaId);
    }

    [Fact]
    public async Task GetContatoAsync_ShouldReturnNull_WhenContatoNotFound()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 1111123456789);
        _unitOfWorkStub.Setup(x => x.ContatoRepository.GetContatoAsync(contato.PessoaId, contato.Celular)).ReturnsAsync((Contato?)null);

        // Act
        var result = await _sut.GetContatoAsync(contato.PessoaId, contato.Celular);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task GetContatosAsync_ShouldReturnContatos_WhenExecuted()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 1111123456789);
        _unitOfWorkStub.Setup(x => x.ContatoRepository.GetEntitiesAsync(c => c.PessoaId == contato.PessoaId, "")).ReturnsAsync(new List<Contato>() { contato });

        // Act
        var result = await _sut.GetContatosAsync(contato.PessoaId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task UpdateContatoAsync_ShouldCallUpdate_WhenExecuted()
    {
        // Arrange
        NewContatoInputModel model = new()
        {
            PessoaId = Guid.NewGuid(),
            Nome = "",
            Celular = 1111123456789
        };

        _unitOfWorkStub.Setup(x => x.ContatoRepository.UpdateAsync(It.IsAny<Contato>())).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        await _sut.UpdateContatoAsync(Guid.NewGuid(), model);

        // Assert
        _unitOfWorkStub.Verify(x => x.ContatoRepository.UpdateAsync(It.IsAny<Contato>()), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task DeleteContatoAsync_ShouldCallDelete_WhenExecuted()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 1111123456789);

        _unitOfWorkStub.Setup(x => x.ContatoRepository.GetContatoAsync(contato.PessoaId, contato.Celular)).ReturnsAsync(contato);

        _unitOfWorkStub.Setup(x => x.ContatoRepository.DeleteAsync(contato.Id)).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        await _sut.DeleteContatoAsync(contato.PessoaId, contato.Celular);

        // Assert
        _unitOfWorkStub.Verify(x => x.ContatoRepository.DeleteAsync(contato.Id), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);  
    }
}