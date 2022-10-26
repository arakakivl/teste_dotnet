using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TesteDotnet.Infrastructure.Persistence;
using Xunit;

namespace TesteDotnet.Tests.Persistence;

public class UnitOfWorkTests
{
    private readonly Mock<AppDbContext> _contextStub;
    private readonly UnitOfWork _sut;

    public UnitOfWorkTests()
    {
        _contextStub = new(new DbContextOptions<AppDbContext>());
        _sut = new(_contextStub.Object);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldCallSaveChangesAsync_WhenExecuted()
    {
        // Arrange
        _contextStub.Setup(x => x.SaveChangesAsync(default)).Verifiable();

        // Act
        await _sut.SaveChangesAsync();

        // Assert
        _contextStub.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }
}