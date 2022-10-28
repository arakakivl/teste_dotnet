using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Infrastructure.Persistence;
using TesteDotnet.Infrastructure.Persistence.Repositories;
using Xunit;

namespace TesteDotnet.Tests.Persistence.Repositories;

public class ContatoRepositoryTests
{
    private readonly Mock<AppDbContext> _contextStub;
    private readonly Mock<DbSet<Contato>> _setStub;

    private readonly ContatoRepository _sut;

    public ContatoRepositoryTests()
    {
        _contextStub = new(new DbContextOptions<AppDbContext>());
        _setStub = new();

        _contextStub.Setup(x => x.Set<Contato>()).Returns(_setStub.Object);

        _sut = new(_contextStub.Object);
    }

    [Fact]
    public async Task GetContatoAsync_ShouldReturnContato_WhenContatoFound()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 5500912345678);
        var data = new List<Contato>()
        {
            contato
        }.AsQueryable();

        _setStub.Setup(x => x.AsQueryable()).Returns(data);

        // Act
        var result = await _sut.GetContatoAsync(contato.PessoaId, contato.Celular);

        // Assert
        Assert.Equal(contato, result);
    }

    [Fact]
    public async Task GetContatoAsync_ShouldReturnNull_WhenContatoNotFound()
    {
        // Arrange
        var contato = new Contato(Guid.NewGuid(), "", 5500912345678);
        var data = new List<Contato>().AsQueryable();

        _setStub.Setup(x => x.AsQueryable()).Returns(data);

        // Act
        var result = await _sut.GetContatoAsync(contato.PessoaId, contato.Celular);

        // Assert
        Assert.Null(result); 
    }
}