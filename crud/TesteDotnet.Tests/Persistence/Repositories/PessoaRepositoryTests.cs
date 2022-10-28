using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TesteDotnet.Domain.Entities;
using TesteDotnet.Infrastructure.Persistence;
using TesteDotnet.Infrastructure.Persistence.Repositories;
using Xunit;

namespace TesteDotnet.Tests.Persistence.Repositories;

public class PessoaRepositoryTests
{
    private readonly Mock<AppDbContext> _contextStub;
    private readonly Mock<DbSet<Pessoa>> _setStub;

    private readonly PessoaRepository _sut;

    public PessoaRepositoryTests()
    {
        _contextStub = new(new DbContextOptions<AppDbContext>());
        _setStub = new();

        _contextStub.Setup(x => x.Set<Pessoa>()).Returns(_setStub.Object);

        _sut = new(_contextStub.Object);
    }

    [Fact]
    public async Task GetContatos_ShouldReturnContatos_WhenExecuted()
    {
        // Arrange
        var pessoa = new Pessoa("nome", "email", 0);
        pessoa.Contatos.Add(new Contato(pessoa.Id, "nome1", 0));

        _setStub.Setup(x => x.FindAsync(pessoa.Id)).ReturnsAsync(pessoa);

        // Act
        var contatos = await _sut.GetContatos(pessoa.Id);

        // Assert
        Assert.Contains(pessoa.Contatos[0], contatos);
    }
}