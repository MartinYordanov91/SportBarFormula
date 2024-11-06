using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportBarFormula.Infastructure.Data.Models;

[Comment("Stores customer feedback and ratings for service, menu, or events.")]
public class Feedback
{
    [Key]
    [Comment("Unique identifier of the feedback")]
    public int FeedbackId { get; set; }

    [Comment("Identifier of the user who provided the feedback")]
    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required virtual IdentityUser User { get; set; }

    [Comment("Rating given by the user (e.g., from 1 to 5)")]
    public required int Rating { get; set; }

    [Comment("User's comment")]
    public string? Comment { get; set; }

    [Comment("Date and time when the feedback was created")]
    public required DateTime CreatedDate { get; set; }
}
