using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

[TestFixture]
public class ReservationRepositoryTests
{
    private SportBarFormulaDbContext _dbContext;
    private ReservationRepository _reservationRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();
        _reservationRepository = new ReservationRepository(_dbContext);
    }

    /// <summary>
    /// Cleans up the in-memory database after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests if AddAsync adds a new Reservation.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddReservation()
    {
        // Arrange
        var newReservation = new Reservation
        {
            ReservationId = 6,
            UserId = "user3",
            ReservationDate = DateTime.UtcNow.AddDays(7),
            TableId = 4,
            IsIndor = true,
            NumberOfGuests = 4,
            IsCanceled = false
        };

        // Act
        await _reservationRepository.AddAsync(newReservation);
        var result = await _dbContext.Reservations.FindAsync(newReservation.ReservationId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo("user3"));
        Assert.That(result.NumberOfGuests, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests if DeleteAsync removes a Reservation.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveReservation()
    {
        // Act
        await _reservationRepository.DeleteAsync(1);
        var result = await _dbContext.Reservations.FindAsync(1);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteAsync throws KeyNotFoundException when the Reservation does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenReservationDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _reservationRepository.DeleteAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Reservation not found"));
    }

    /// <summary>
    /// Tests if GetAllAsync returns all Reservations.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllReservations()
    {
        // Act
        var result = await _reservationRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(5));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a Reservation by ID.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnReservation_WhenReservationExists()
    {
        // Act
        var result = await _reservationRepository.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo("test-user-id"));
        Assert.That(result.NumberOfGuests, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws KeyNotFoundException when the Reservation does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenReservationDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _reservationRepository.GetByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Reservation not found"));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing Reservation.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateReservation()
    {
        // Arrange
        var reservation = await _reservationRepository.GetByIdAsync(1);
        reservation.NumberOfGuests = 5;

        // Act
        await _reservationRepository.UpdateAsync(reservation);
        var result = await _dbContext.Reservations.FindAsync(reservation.ReservationId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.NumberOfGuests, Is.EqualTo(5));
    }
}
