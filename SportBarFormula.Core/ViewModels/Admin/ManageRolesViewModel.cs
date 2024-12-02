using Microsoft.AspNetCore.Identity;

namespace SportBarFormula.Core.ViewModels.Admin;

public class ManageRolesViewModel
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public IEnumerable<string> UserRoles { get; set; } = null!;
    public IEnumerable<IdentityRole> AllRoles { get; set; } = null!;
}
