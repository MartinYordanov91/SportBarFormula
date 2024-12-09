using Microsoft.AspNetCore.Identity;
using Moq;

namespace SportBarFormula.UnitTests.Moq;

public static class MockRoleManager
{
    public static Mock<RoleManager<IdentityRole>> Create()
    {
        var roleStore = new Mock<IRoleStore<IdentityRole>>();
        var mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

        // Seed roles
        var roles = new List<IdentityRole>
         {
             new IdentityRole { Name = "Admin" },
             new IdentityRole { Name = "Manager" },
             new IdentityRole { Name = "Staff" },
             new IdentityRole { Name = "Chef" },
             new IdentityRole { Name = "Waiter" },
             new IdentityRole { Name = "Bartender" },
             new IdentityRole { Name = "Cleaner" }
         }.AsQueryable();

        mockRoleManager.Setup(r => r.Roles).Returns(roles);

        return mockRoleManager;
    }
}