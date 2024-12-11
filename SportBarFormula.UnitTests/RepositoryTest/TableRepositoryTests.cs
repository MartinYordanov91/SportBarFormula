using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

/// <summary>
/// Unit tests for the TableRepository class.
/// </summary>
[TestFixture]
public class TableRepositoryTests
{
    private TableRepository _repository;
    private SportBarFormulaDbContext _context;

    /// <summary>
    /// Initializes a new instance of the TableRepositoryTests class and sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _context = MockDbContextFactory.Create();
        _repository = new TableRepository(_context);
    }

    /// <summary>
    /// Disposes of the test context after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests if GetAllAsync returns all Table entities.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllTables()
    {
        // Arrange
        var expectedCount = await _context.Tables.CountAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(expectedCount));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a Table entity when it exists.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnTable_WhenTableExists()
    {
        // Arrange
        var table = await _context.Tables.FirstAsync();

        // Act
        var result = await _repository.GetByIdAsync(table.TableId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TableId, Is.EqualTo(table.TableId));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws KeyNotFoundException when the Table entity does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenTableDoesNotExist()
    {
        // Act & Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetByIdAsync(999)); // Non-existing ID
    }

    /// <summary>
    /// Tests if AddAsync correctly adds a new Table entity.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddTable()
    {
        // Arrange
        var table = new Table { TableId = 6, Capacity = 4, TableNumber = "105", Location = "outdoor", IsAvailable = true };

        // Act
        await _repository.AddAsync(table);
        var result = await _context.Tables.FindAsync(6);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TableId, Is.EqualTo(table.TableId));
    }

    /// <summary>
    /// Tests if UpdateAsync correctly updates an existing Table entity.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateTable()
    {
        // Arrange
        var table = await _context.Tables.FirstAsync();
        table.Capacity = 10;

        // Act
        await _repository.UpdateAsync(table);
        var result = await _context.Tables.FindAsync(table.TableId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Capacity, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests if DeleteAsync correctly deletes a Table entity when it exists.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldDeleteTable_WhenTableExists()
    {
        // Arrange
        var table = await _context.Tables.FirstAsync();

        // Act
        await _repository.DeleteAsync(table.TableId);
        var result = await _context.Tables.FindAsync(table.TableId);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteAsync throws KeyNotFoundException when the Table entity does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenTableDoesNotExist()
    {
        // Act & Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.DeleteAsync(999)); // Non-existing ID
    }
}
