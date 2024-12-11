using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Areas.Admin.Controllers;

namespace SportBarFormula.UnitTests.Controllers;

/// <summary>
/// Unit tests for the AdminHomeController.
/// </summary>
[TestFixture]
public class AdminHomeControllerTests
{
    private HomeController _adminHomeController;

    /// <summary>
    /// Sets up the test environment before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _adminHomeController = new HomeController();
    }

    /// <summary>
    /// Cleans up the test environment after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _adminHomeController.Dispose();
    }

    /// <summary>
    /// Tests if the Index method returns a ViewResult.
    /// </summary>
    [Test]
    public void Index_ShouldReturnViewResult()
    {
        // Act
        var result = _adminHomeController.Index();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }
}
