using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Controllers;

namespace SportBarFormula.UnitTests.Controllers;

/// <summary>
/// Unit tests for the HomeController.
/// </summary>
[TestFixture]
public class HomeControllerTests
{
    private HomeController _homeController;

    /// <summary>
    /// Sets up the test environment before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _homeController = new HomeController();
    }

    /// <summary>
    /// Cleans up the test environment after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _homeController.Dispose();
    }

    /// <summary>
    /// Tests if the Index method returns a ViewResult.
    /// </summary>
    [Test]
    public void Index_ShouldReturnViewResult()
    {
        // Act
        var result = _homeController.Index();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    /// <summary>
    /// Tests if the Privacy method returns a ViewResult.
    /// </summary>
    [Test]
    public void Privacy_ShouldReturnViewResult()
    {
        // Act
        var result = _homeController.Privacy();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    /// <summary>
    /// Tests if the Error method returns the Error400 view when the status code is 400.
    /// </summary>
    [Test]
    public void Error_ShouldReturnError400View_WhenStatusCodeIs400()
    {
        // Act
        var result = _homeController.Error(400);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.ViewName, Is.EqualTo("Error400"));
    }

    /// <summary>
    /// Tests if the Error method returns the Error401 view when the status code is 401.
    /// </summary>
    [Test]
    public void Error_ShouldReturnError401View_WhenStatusCodeIs401()
    {
        // Act
        var result = _homeController.Error(401);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.ViewName, Is.EqualTo("Error401"));
    }

    /// <summary>
    /// Tests if the Error method returns the default error view when the status code is neither 400 nor 401.
    /// </summary>
    [Test]
    public void Error_ShouldReturnDefaultErrorView_WhenStatusCodeIsNot400Or401()
    {
        // Act
        var result = _homeController.Error(500);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.ViewName, Is.Null);
    }
}
