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

public class PessoaControllerTests
{
    private readonly Mock<IPessoaService> _pessoaServiceStub;
    private readonly PessoasController _sut;

    public PessoaControllerTests()
    {
        _pessoaServiceStub = new();
        _sut = new(_pessoaServiceStub.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreatedAtAction_WhenExecuted()
    {
        // Arrange
        var id = Guid.NewGuid();

        _pessoaServiceStub.Setup(x => x.CreatePessoaAsync(It.IsAny<NewPessoaInputModel>())).ReturnsAsync(id);
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync(new PessoaViewModel() { Id = id });

        _pessoaServiceStub.Setup(x => x.CreatePessoaAsync(It.IsAny<NewPessoaInputModel>())).Verifiable();

        // Act
        var r = await _sut.CreateAsync(new NewPessoaInputModel());
        var createdAt = r as CreatedAtActionResult;

        // Assert
        Assert.IsType<CreatedAtActionResult>(r);

        Assert.NotNull(createdAt?.Value);
        Assert.IsType<PessoaViewModel>(createdAt?.Value);
    
        _pessoaServiceStub.Verify(x => x.CreatePessoaAsync(It.IsAny<NewPessoaInputModel>()), Times.Once);    
    }

    [Fact]
    public async Task GetAsync_ShouldReturnOk_WhenPessoaFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync(new PessoaViewModel() { Id = id });

        // Act
        var result = await _sut.GetAsync(id);
        var obj = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(obj?.Value);
        Assert.IsType<PessoaViewModel>(obj?.Value);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFound_WhenPessoaNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync((PessoaViewModel?)null);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAll_WhenExecuted()
    {
        // Arrange
        var retrieved = new List<PessoaViewModel>()
        {
            new PessoaViewModel(),
            new PessoaViewModel()
        };

        _pessoaServiceStub.Setup(x => x.GetPessoasAsync()).ReturnsAsync(retrieved);

        // Act
        var result = await _sut.GetAllAsync();
        var obj = result as OkObjectResult;

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<ICollection<PessoaViewModel>>(obj?.Value);

        Assert.Equal(retrieved, obj?.Value);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNoContent_WhenPessoaFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync(new PessoaViewModel() { Id = id });
    
        // Act
        var r = await _sut.UpdateAsync(id, new NewPessoaInputModel() { });

        // Assert
        Assert.IsType<NoContentResult>(r);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFound_WhenPessoaNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync((PessoaViewModel?)null);
    
        // Act
        var r = await _sut.UpdateAsync(id, new NewPessoaInputModel() { });

        // Assert
        Assert.IsType<NotFoundResult>(r);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNoContent_WhenPessoaFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync(new PessoaViewModel() { Id = id });
    
        // Act
        var r = await _sut.DeleteAsync(id);

        // Assert
        Assert.IsType<NoContentResult>(r); 
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNotFound_WhenPessoaNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _pessoaServiceStub.Setup(x => x.GetPessoaAsync(id)).ReturnsAsync((PessoaViewModel?)null);
    
        // Act
        var r = await _sut.DeleteAsync(id);

        // Assert
        Assert.IsType<NotFoundResult>(r);
    }
}
