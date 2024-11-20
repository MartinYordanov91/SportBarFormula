﻿using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Reservation;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using System.Globalization;
using static SportBarFormula.Infrastructure.Constants.DataConstants.ReservationConstants;


namespace SportBarFormula.Core.Services;

/// <summary>
/// Reservation Management Service.
/// </summary>
public class ReservationService(IRepository<Reservation> repository) : IReservationService
{
    private readonly IRepository<Reservation> _repository = repository;

    /// <summary>
    /// Returns all reservations.
    /// </summary>
    /// <returns>A list of all reservations.</returns>
    public async Task<IEnumerable<ReservationViewModel>> GetAllReservationsAsync()
    {
        var reservations = await _repository.GetAllAsync();
        return reservations
            .Select(r => new ReservationViewModel
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ReservationDate = r.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
                TableId = r.TableId,
                NumberOfGuests = r.NumberOfGuests,
                IsCanceled = r.IsCanceled
            })
            .ToList();
    }

    /// <summary> 
    /// Returns reservations for a specific user.
    /// </summary> 
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of reservations for the user.</returns>
    public async Task<IEnumerable<ReservationViewModel>> GetReservationsByUserIdAsync(string userId)
    {
        var reservations = await _repository.GetAllAsync();

        return reservations
            .Where(r => r.UserId == userId)
            .Select(r => new ReservationViewModel
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ReservationDate = r.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
                TableId = r.TableId,
                NumberOfGuests = r.NumberOfGuests,
                IsCanceled = r.IsCanceled
            })
            .ToList();
    }

}