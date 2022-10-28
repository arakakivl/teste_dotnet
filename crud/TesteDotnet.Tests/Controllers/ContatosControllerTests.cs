using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteDotnet.Api.Controllers;
using TesteDotnet.Application.Models.InputModels;
using TesteDotnet.Application.Models.ViewModels;
using TesteDotnet.Application.Services.Interfaces;
using Xunit;

namespace TesteDotnet.Tests.Controllers;

public class ContatosControllerTests
{
    private readonly Mock<IPessoaService> _pessoaServiceStub;
    private readonly Mock<IContatoService> _contatoServiceStub;

    private readonly ContatosController _sut;

    public ContatosControllerTests()
    {
        _pessoaServiceStub = new();
        _contatoServiceStub = new();

        _sut = new(_pessoaServiceStub.Object, _contatoServiceStub.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnOk_WhenContatoDoesNotExistYetOrPessoaDoesNotExist()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 12345678900011;
        
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync((ContatoViewModel?)null);
        
        _contatoServiceStub.Setup(x => x.CreateContatoAsync(It.IsAny<NewContatoInputModel>())).Verifiable();

        // Act
        var result = await _sut.CreateAsync(new NewContatoInputModel() { PessoaId = pessoaId, Celular = celular });

        // Assert
        Assert.IsType<OkResult>(result);
        _contatoServiceStub.Verify(x => x.CreateContatoAsync(It.IsAny<NewContatoInputModel>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnBadRequest_WhenContatoAlreadyExists()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 12345678900011;
        
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync(new ContatoViewModel() { PessoaId = pessoaId, Celular = celular });
        _contatoServiceStub.Setup(x => x.CreateContatoAsync(It.IsAny<NewContatoInputModel>())).Verifiable();

        // Act
        var result = await _sut.CreateAsync(new NewContatoInputModel() { PessoaId = pessoaId, Celular = celular });

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnAllFromPessoa_WhenPessoaExists()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();

        var data = new List<ContatoViewModel>()
        {
            new ContatoViewModel(),
            new ContatoViewModel()
        };

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatosAsync(pessoaId)).ReturnsAsync(data);

        // Act
        var r = await _sut.GetAsync(pessoaId, null);
        var result = r as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(r);

        Assert.IsAssignableFrom<ICollection<ContatoViewModel>>(result?.Value);
        Assert.Equal(data, result?.Value);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnContato_WhenPessoaExistsAndCelularIsProvided()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync(new ContatoViewModel() { PessoaId = pessoaId, Celular = celular });

        // Act
        var r = await _sut.GetAsync(pessoaId, celular);
        var result = r as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(r);
        Assert.IsType<ContatoViewModel>(result?.Value);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnBadRequest_WhenPessoaDoesNotExist()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync((PessoaViewModel?)null);

        // Act
        var r = await _sut.GetAsync(pessoaId, celular);

        // Assert
        Assert.IsType<BadRequestResult>(r);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNoContent_WhenPessoaExistsAndFoundContato()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync(new ContatoViewModel() { PessoaId = pessoaId, Celular = celular });

        // Act
        var r = await _sut.UpdateAsync(pessoaId, new NewContatoInputModel() { OldCelular = celular, PessoaId = pessoaId });

        // Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNoContent_WhenNeitherPessoaExistsNorFoundContato()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync((ContatoViewModel?)null);

        // Act
        var r = await _sut.UpdateAsync(pessoaId, new NewContatoInputModel() { OldCelular = celular, PessoaId = pessoaId });

        // Assert
        Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNoContent_WhenPessoaExistsAndFoundContato()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync(new ContatoViewModel() { PessoaId = pessoaId, Celular = celular });

        // Act
        var r = await _sut.DeleteAsync(pessoaId, celular);

        // Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNoContent_WhenNeitherPessoaExistsNorFoundContato()
    {
        // Arrange
        Guid pessoaId = Guid.NewGuid();
        long celular = 0011123456789;

        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(pessoaId)).ReturnsAsync(new PessoaViewModel() { Id = pessoaId });
        _contatoServiceStub.Setup(x => x.GetContatoAsync(pessoaId, celular)).ReturnsAsync((ContatoViewModel?)null);

        // Act
        var r = await _sut.DeleteAsync(pessoaId, celular);

        // Assert
        Assert.IsType<NotFoundResult>(r);
    }
}