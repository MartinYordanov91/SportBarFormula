using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("Contains information about tables in the restaurant, such as table number and capacity.")]
public class Table
{
    [Key]
    [Comment("Unique identifier of the table")]
    public int TableId { get; set; }

    [Comment("Table number")]
    public required string TableNumber { get; set; }

    [Comment("Table capacity")]
    public required int Capacity { get; set; }

    [Comment("Table location (e.g., indoor, outdoor)")]
    public string? Location { get; set; }

    [Comment("Table availability flag")]
    public bool IsAvailable { get; set; } = true;
}
