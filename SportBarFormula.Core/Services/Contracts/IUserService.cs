using Microsoft.AspNetCore.Identity;
using SportBarFormula.Core.ViewModels.Admin;

namespace SportBarFormula.Core.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<IdentityUser>> GetAllUsersAsync();

    Task<ManageRolesViewModel> GetManageRolesViewModelAsync(string userId);

    Task SetUnsetRolesToUsers(string userId, List<string> roles);
}
