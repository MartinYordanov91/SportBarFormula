using Microsoft.AspNetCore.Identity;
using Moq;
using SportBarFormula.Core.Services;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.ServiceTest;

[TestFixture]
public class UserServiceTests
{
    private Mock<UserManager<IdentityUser>> _mockUserManager;
    private Mock<RoleManager<IdentityRole>> _mockRoleManager;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _mockUserManager = MockUserManager.Create();
        _mockRoleManager = MockRoleManager.Create();
        _userService = new UserService(_mockUserManager.Object, _mockRoleManager.Object);
    }

    /// <summary>
    /// Tests if GetAllUsersAsync returns all users.
    /// </summary>
    [Test]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(5));
        Assert.That(result.Any(u => u.UserName == "user1"));
        Assert.That(result.Any(u => u.UserName == "user2"));
        Assert.That(result.Any(u => u.UserName == "user3"));
        Assert.That(result.Any(u => u.UserName == "user4"));
        Assert.That(result.Any(u => u.UserName == "user5"));
    }

    /// <summary>
    /// Tests if GetManageRolesViewModelAsync returns the correct ManageRolesViewModel.
    /// </summary>
    [Test]
    public async Task GetManageRolesViewModelAsync_ShouldReturnManageRolesViewModel()
    {
        // Arrange
        var user = new IdentityUser { Id = "1", UserName = "user1" };
        var userRoles = new List<string> { "Admin", "User" };
        var allRoles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "Manager" },
            new IdentityRole { Name = "Staff" },
            new IdentityRole { Name = "Chef" },
            new IdentityRole { Name = "Waiter" },
            new IdentityRole { Name = "Bartender" },
            new IdentityRole { Name = "Cleaner" }
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(userRoles);
        _mockRoleManager.Setup(r => r.Roles).Returns(allRoles.AsQueryable());

        // Act
        var result = await _userService.GetManageRolesViewModelAsync("1");

        // Assert
        Assert.That(result.UserId, Is.EqualTo("1"));
        Assert.That(result.UserName, Is.EqualTo("user1"));
        Assert.That(result.UserRoles.Count, Is.EqualTo(2));
        Assert.That(result.AllRoles.Count, Is.EqualTo(7));
    }

    /// <summary>
    /// Tests if GetManageRolesViewModelAsync throws InvalidOperationException when the user is not found.
    /// </summary>
    [Test]
    public void GetManageRolesViewModelAsync_ShouldThrowInvalidOperationException_WhenUserNotFound()
    {
        // Arrange
        _mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.GetManageRolesViewModelAsync("1"));
        Assert.That(exception.Message, Is.EqualTo("User not found"));
    }

    /// <summary>
    /// Tests if SetUnsetRolesToUsers correctly sets and unsets roles for a user.
    /// </summary>
    [Test]
    public async Task SetUnsetRolesToUsers_ShouldSetAndUnsetRolesForUser()
    {
        // Arrange
        var user = new IdentityUser { Id = "1", UserName = "user1" };
        var userRoles = new List<string> { "Admin" };
        var roles = new List<string> { "User", "Guest" };
        var allRoles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "Manager" },
            new IdentityRole { Name = "Staff" },
            new IdentityRole { Name = "Chef" },
            new IdentityRole { Name = "Waiter" },
            new IdentityRole { Name = "Bartender" },
            new IdentityRole { Name = "Cleaner" }
        };

        _mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _mockUserManager.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(userRoles);
        _mockRoleManager.Setup(r => r.Roles).Returns(allRoles.AsQueryable());

        // Act
        await _userService.SetUnsetRolesToUsers("1", roles);

        // Assert
        _mockUserManager.Verify(u => u.AddToRolesAsync(user, It.Is<IEnumerable<string>>(r => r.Contains("User") && r.Contains("Guest"))), Times.Once);
        _mockUserManager.Verify(u => u.RemoveFromRolesAsync(user, It.Is<IEnumerable<string>>(r => r.Contains("Admin"))), Times.Once);
    }

    /// <summary>
    /// Tests if SetUnsetRolesToUsers throws InvalidOperationException when the user is not found.
    /// </summary>
    [Test]
    public void SetUnsetRolesToUsers_ShouldThrowInvalidOperationException_WhenUserNotFound()
    {
        // Arrange
        _mockUserManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.SetUnsetRolesToUsers("1", new List<string>()));
        Assert.That(exception.Message, Is.EqualTo("User not found"));
    }
}
