using SportBarFormula.Core.Services;
using SportBarFormula.Core.ViewModels.Reservation;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;
using System.Globalization;
using static SportBarFormula.Infrastructure.Constants.DataConstants.ReservationConstants;

namespace SportBarFormula.UnitTests.ServiceTest;

[TestFixture]
public class ReservationServiceTests
{

    private SportBarFormulaDbContext _dbContext;
    private ReservationService _reservationService;

    [SetUp]
    public void SetUp()
    {

        _dbContext = MockDbContextFactory.Create();

        var repository = new ReservationRepository(_dbContext);
        _reservationService = new ReservationService(repository);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }


    /// <summary>
    /// Tests if AddReservationAsync adds a new reservation correctly.
    /// </summary>
    [Test]
    public async Task AddReservationAsync_ShouldAddReservation()
    {
        // Arrange
        var newReservation = new ReservationViewModel
        {
            UserId = "test-user-id-1",
            ReservationDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
            NumberOfGuests = 4,
            IsIndor = true
        };

        // Act
        await _reservationService.AddReservationAsync(newReservation);

        // Assert
        var addedReservation = _dbContext.Reservations.FirstOrDefault(r => r.UserId == newReservation.UserId && r.NumberOfGuests == newReservation.NumberOfGuests);
        Assert.That(addedReservation, Is.Not.Null);
        Assert.That(addedReservation.ReservationDate.ToString("dd-MM-yyyy HH:mm"), Is.EqualTo(newReservation.ReservationDate));
        Assert.That(addedReservation.NumberOfGuests, Is.EqualTo(newReservation.NumberOfGuests));
        Assert.That(addedReservation.IsIndor, Is.EqualTo(newReservation.IsIndor));
    }

    /// <summary>
    /// Tests if AddReservationAsync throws ArgumentException for invalid date format.
    /// </summary>
    [Test]
    public void AddReservationAsync_ShouldThrowArgumentException_ForInvalidDateFormat()
    {
        // Arrange
        var newReservation = new ReservationViewModel
        {
            UserId = "test-user-id",
            ReservationDate = "invalid date format",
            NumberOfGuests = 4,
            IsIndor = true
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _reservationService.AddReservationAsync(newReservation));
        Assert.That(exception.Message, Is.EqualTo("Invalid reservation date format."));
    }

    /// <summary>
    /// Tests if CancelReservationAsync cancels the reservation correctly.
    /// </summary>
    [Test]
    public async Task CancelReservationAsync_ShouldCancelReservation()
    {
        // Arrange
        var reservationId = 1; 

        // Act
        await _reservationService.CancelReservationAsync(reservationId);

        // Assert
        var canceledReservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
        Assert.That(canceledReservation, Is.Not.Null);
        Assert.That(canceledReservation.IsCanceled, Is.True);
    }

    /// <summary>
    /// Tests if CancelReservationAsync throws InvalidOperationException when reservation is not found.
    /// </summary>
    [Test]
    public void CancelReservationAsync_ShouldThrowInvalidOperationException_WhenReservationNotFound()
    {
        // Arrange
        var nonExistingReservationId = 999; // ID, който не съществува

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _reservationService.CancelReservationAsync(nonExistingReservationId));
        Assert.That(exception.Message, Is.EqualTo("No reservation found in the repository."));
    }

    /// <summary>
    /// Tests if GetAllReservationsAsync returns all reservations correctly.
    /// </summary>
    [Test]
    public async Task GetAllReservationsAsync_ShouldReturnAllReservations()
    {
        // Act
        var result = await _reservationService.GetAllReservationsAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(_dbContext.Reservations.Count()));

        foreach (var reservation in result)
        {
            var expectedReservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == reservation.ReservationId);
            Assert.That(expectedReservation, Is.Not.Null);
            Assert.That(reservation.UserId, Is.EqualTo(expectedReservation.UserId));
            Assert.That(reservation.ReservationDate, Is.EqualTo(expectedReservation.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture)));
            Assert.That(reservation.NumberOfGuests, Is.EqualTo(expectedReservation.NumberOfGuests));
            Assert.That(reservation.IsCanceled, Is.EqualTo(expectedReservation.IsCanceled));
        }
    }

    /// <summary>
    /// Tests if GetReservationByIdAsync returns the correct reservation by ID.
    /// </summary>
    [Test]
    public async Task GetReservationByIdAsync_ShouldReturnCorrectReservation()
    {
        // Arrange
        var existingReservationId = 1;

        // Act
        var result = await _reservationService.GetReservationByIdAsync(existingReservationId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ReservationId, Is.EqualTo(existingReservationId));

        var expectedReservation = _dbContext.Reservations.Find(existingReservationId);
        Assert.That(expectedReservation, Is.Not.Null); 
        Assert.That(result.UserId, Is.EqualTo(expectedReservation.UserId));
        Assert.That(result.ReservationDate, Is.EqualTo(expectedReservation.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture)));
        Assert.That(result.NumberOfGuests, Is.EqualTo(expectedReservation.NumberOfGuests));
        Assert.That(result.IsCanceled, Is.EqualTo(expectedReservation.IsCanceled));
    }

    /// <summary>
    /// Tests if GetReservationByIdAsync throws InvalidOperationException when reservation is not found.
    /// </summary>
    [Test]
    public void GetReservationByIdAsync_ShouldThrowInvalidOperationException_WhenReservationNotFound()
    {
        // Arrange
        var nonExistingReservationId = 999; // ID, който не съществува

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _reservationService.GetReservationByIdAsync(nonExistingReservationId));
        Assert.That(exception.Message, Is.EqualTo("No reservation found in the repository."));
    }

    /// <summary>
    /// Tests if GetReservationsByUserIdAsync returns all reservations for a specific user.
    /// </summary>
    [Test]
    public async Task GetReservationsByUserIdAsync_ShouldReturnReservationsForSpecificUser()
    {
        // Arrange
        var userId = "test-user-id";

        // Act
        var result = await _reservationService.GetReservationsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.All(r => r.UserId == userId && !r.IsCanceled), Is.True);
        Assert.That(result.Count(), Is.EqualTo(_dbContext.Reservations.Count(r => r.UserId == userId && !r.IsCanceled)));

        foreach (var reservation in result)
        {
            var expectedReservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == reservation.ReservationId);
            Assert.That(expectedReservation, Is.Not.Null);
            Assert.That(reservation.UserId, Is.EqualTo(expectedReservation.UserId));
            Assert.That(reservation.ReservationDate, Is.EqualTo(expectedReservation.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture)));
            Assert.That(reservation.NumberOfGuests, Is.EqualTo(expectedReservation.NumberOfGuests));
            Assert.That(reservation.IsCanceled, Is.EqualTo(expectedReservation.IsCanceled));
        }
    }

    /// <summary>
    /// Tests if UpdateReservationAsync updates the reservation correctly.
    /// </summary>
    [Test]
    public async Task UpdateReservationAsync_ShouldUpdateReservation()
    {
        // Arrange
        var existingReservationId = 1;
        var updatedReservationModel = new ReservationViewModel
        {
            ReservationId = existingReservationId,
            UserId = "test-user-id",
            ReservationDate = DateTime.UtcNow.AddDays(1).ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
            TableId = 2,
            NumberOfGuests = 5,
            IsIndor = false,
            IsCanceled = true
        };

        // Act
        await _reservationService.UpdateReservationAsync(updatedReservationModel);

        // Assert
        var updatedReservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == existingReservationId);
        Assert.That(updatedReservation, Is.Not.Null);
        Assert.That(updatedReservation.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture), Is.EqualTo(updatedReservationModel.ReservationDate));
        Assert.That(updatedReservation.TableId, Is.EqualTo(updatedReservationModel.TableId));
        Assert.That(updatedReservation.NumberOfGuests, Is.EqualTo(updatedReservationModel.NumberOfGuests));
        Assert.That(updatedReservation.IsIndor, Is.EqualTo(updatedReservationModel.IsIndor));
        Assert.That(updatedReservation.IsCanceled, Is.EqualTo(updatedReservationModel.IsCanceled));
    }

    /// <summary>
    /// Tests if UpdateReservationAsync throws InvalidOperationException when reservation is not found.
    /// </summary>
    [Test]
    public void UpdateReservationAsync_ShouldThrowInvalidOperationException_WhenReservationNotFound()
    {
        // Arrange
        var nonExistingReservationId = 999; // ID, който не съществува
        var updatedReservationModel = new ReservationViewModel
        {
            ReservationId = nonExistingReservationId,
            UserId = "test-user-id",
            ReservationDate = DateTime.UtcNow.AddDays(1).ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
            TableId = 2,
            NumberOfGuests = 5,
            IsIndor = false,
            IsCanceled = true
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _reservationService.UpdateReservationAsync(updatedReservationModel));
        Assert.That(exception.Message, Is.EqualTo("No reservation found in the repository."));
    }

    /// <summary>
    /// Tests if UpdateReservationAsync throws ArgumentException for invalid date format.
    /// </summary>
    [Test]
    public void UpdateReservationAsync_ShouldThrowArgumentException_ForInvalidDateFormat()
    {
        // Arrange
        var existingReservationId = 1;
        var updatedReservationModel = new ReservationViewModel
        {
            ReservationId = existingReservationId,
            UserId = "test-user-id",
            ReservationDate = "invalid date format",
            TableId = 2,
            NumberOfGuests = 5,
            IsIndor = false,
            IsCanceled = true
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _reservationService.UpdateReservationAsync(updatedReservationModel));
        Assert.That(exception.Message, Is.EqualTo("Invalid reservation date format."));
    }

}
