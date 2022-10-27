using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Services;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Domain.Interfaces;
using Xunit;

namespace TesteDotnet.Tests.Services;

public class PessoaServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkStub;
    private readonly PessoaService _sut;

    public PessoaServiceTests()
    {
        _unitOfWorkStub = new();
        _sut = new(_unitOfWorkStub.Object);
    }

    [Fact]
    public async Task CreatePessoaAsync_ShouldReturnId_WhenExecuted()
    {
        // Arrange
        NewPessoaInputModel model = new()
        {
            Nome = "",
            Email = "",
            CPF = 12345678900
        };

        _unitOfWorkStub.Setup(x => x.PessoaRepository.AddAsync(It.IsAny<Pessoa>())).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        var result = await _sut.CreatePessoaAsync(model);

        // Assert
        _unitOfWorkStub.Verify(x => x.PessoaRepository.AddAsync(It.IsAny<Pessoa>()), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);

        Assert.IsType(typeof(Guid), result);
    }

    [Fact]
    public async Task GetPessoaAsync_ShouldReturnPessoa_WhenPessoaFound()
    {
        // Arrange
        var pessoa = new Pessoa("", "", 12345678900);
        _unitOfWorkStub.Setup(x => x.PessoaRepository.GetEntityAsync(pessoa.Id)).ReturnsAsync(pessoa);

        // Act
        var result = await _sut.GetPessoaAsync(pessoa.Id);

        // Assert
        Assert.Equal(pessoa.Id, result!.Id);
    }

    [Fact]
    public async Task GetPessoaAsync_ShouldReturnNull_WhenPessoaNotFound()
    {
        // Arrange
        var pessoa = new Pessoa("", "", 12345678900);
        _unitOfWorkStub.Setup(x => x.PessoaRepository.GetEntityAsync(pessoa.Id)).ReturnsAsync((Pessoa?)null);

        // Act
        var result = await _sut.GetPessoaAsync(pessoa.Id);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task GetPessoasAsync_ShouldReturnPessoas_WhenExecuted()
    {
        // Arrange
        var pessoa = new Pessoa("", "", 12345678900);
        _unitOfWorkStub.Setup(x => x.PessoaRepository.GetEntitiesAsync()).ReturnsAsync(new List<Pessoa>() { pessoa });

        // Act
        var result = await _sut.GetPessoasAsync();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task UpdatePessoaAsync_ShouldCallUpdate_WhenExecuted()
    {
        // Arrange
        NewPessoaInputModel model = new()
        {
            Nome = "",
            Email = "",
            CPF = 12345678900
        };

        _unitOfWorkStub.Setup(x => x.PessoaRepository.UpdateAsync(It.IsAny<Pessoa>())).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        await _sut.UpdatePessoaAsync(Guid.NewGuid(), model);

        // Assert
        _unitOfWorkStub.Verify(x => x.PessoaRepository.UpdateAsync(It.IsAny<Pessoa>()), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task DeletePessoaAsync_ShouldCallDelete_WhenExecuted()
    {
        // Arrange
        var id = Guid.NewGuid();

        _unitOfWorkStub.Setup(x => x.PessoaRepository.DeleteAsync(id)).Verifiable();
        _unitOfWorkStub.Setup(x => x.SaveChangesAsync()).Verifiable();

        // Act
        await _sut.DeletePessoaAsync(id);

        // Assert
        _unitOfWorkStub.Verify(x => x.PessoaRepository.DeleteAsync(id), Times.Once);
        _unitOfWorkStub.Verify(x => x.SaveChangesAsync(), Times.Once);  
    }
}