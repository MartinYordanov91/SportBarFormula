using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infrastructure.Constants.DataConstants.ShiftConstants;

namespace SportBarFormula.Infrastructure.Data.Models;


[Comment("Manages employee shifts to organize working hours.")]
public class Shift
{
    [Key]
    [Comment("Unique identifier of the shift")]
    public int ShiftId { get; set; }

    [Comment("Identifier of the user assigned to the shift")]
    public required string UserId { get; set; }


    [Comment("Start time of the shift")]
    public required DateTime StartTime { get; set; }

    [Comment("End time of the shift")]
    public required DateTime EndTime { get; set; }

    [MaxLength(ShiftRoleMaxLength)]
    [Comment("Role of the employee during the shift")]
    public required string Role { get; set; }



    [ForeignKey(nameof(UserId))]
    [Comment("Details of the user assigned to the shift")]
    public required virtual IdentityUser User { get; set; }
}

