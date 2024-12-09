using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.UnitTests.Moq;

public static class MockDbContextFactory
{
    public static SportBarFormulaDbContext Create()
    {
        var options = new DbContextOptionsBuilder<SportBarFormulaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new SportBarFormulaDbContext(options);

        // Seed data
        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(
                new IdentityUser { Id = "test-user-id", UserName = "testuser", Email = "testuser@example.com" },
                new IdentityUser { Id = "user2", UserName = "seconduser", Email = "seconduser@example.com" },
                new IdentityUser { Id = "user3", UserName = "thirduser", Email = "thirduser@example.com" }
            );
            dbContext.SaveChanges();
        };

        if (!dbContext.Categories.Any())
        {
            dbContext.Categories.AddRange(
                new Category { CategoryId = 1, Name = "Pizza" },
                new Category { CategoryId = 2, Name = "Drinks" },
                new Category { CategoryId = 3, Name = "Desserts" },
                new Category { CategoryId = 4, Name = "Category to Delete" }
            );
            dbContext.SaveChanges();
        }

        if (!dbContext.MenuItems.Any())
        {
            dbContext.MenuItems.AddRange(
                new MenuItem { MenuItemId = 1, Name = "Margherita Pizza", Price = 8.99m, Quantity = 1, ImageURL = "pizza.jpg", PreparationTime = 15, CategoryId = 1 },
                new MenuItem { MenuItemId = 2, Name = "Coke", Price = 1.99m, Quantity = 1, ImageURL = "coke.jpg", PreparationTime = 1, CategoryId = 2 },
                new MenuItem { MenuItemId = 3, Name = "Cheesecake", Price = 4.99m, Quantity = 1, ImageURL = "cheesecake.jpg", PreparationTime = 10, CategoryId = 3 },
                new MenuItem { MenuItemId = 4, Name = "Burger", Price = 5.99m, Quantity = 1, ImageURL = "burger.jpg", PreparationTime = 10, CategoryId = 1 }
            );
            dbContext.SaveChanges();
        }

        if (!dbContext.Orders.Any())
        {
            dbContext.Orders.AddRange(
                new Order { OrderId = 1, UserId = "user1", OrderDate = DateTime.UtcNow, TotalAmount = 15.97m, Status = OrderStatus.Completed },
                new Order { OrderId = 2, UserId = "user2", OrderDate = DateTime.UtcNow, TotalAmount = 5.98m, Status = OrderStatus.Draft },
                new Order { OrderId = 3, UserId = "test-user-id", OrderDate = DateTime.UtcNow, TotalAmount = 10.99m, Status = OrderStatus.Completed }
            );
            dbContext.SaveChanges();
        }

        if (!dbContext.OrderItems.Any())
        {
            dbContext.OrderItems.AddRange(
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuItemId = 1, Quantity = 1, Price = 8.99m },
                new OrderItem { OrderItemId = 2, OrderId = 1, MenuItemId = 2, Quantity = 1, Price = 1.99m },
                new OrderItem { OrderItemId = 3, OrderId = 2, MenuItemId = 3, Quantity = 1, Price = 4.99m }
            );
            dbContext.SaveChanges();
        }

        if (!dbContext.Reservations.Any())
        {
            dbContext.Reservations.AddRange(
                new Reservation { ReservationId = 1, UserId = "test-user-id", ReservationDate = DateTime.UtcNow.AddDays(1), TableId = 1, IsIndor = true, NumberOfGuests = 4, IsCanceled = false },
                new Reservation { ReservationId = 2, UserId = "user2", ReservationDate = DateTime.UtcNow.AddDays(2), TableId = 2, IsIndor = false, NumberOfGuests = 2, IsCanceled = false },
                new Reservation { ReservationId = 3, UserId = "user3", ReservationDate = DateTime.UtcNow.AddDays(3), TableId = 3, IsIndor = true, NumberOfGuests = 6, IsCanceled = true },
                new Reservation { ReservationId = 4, UserId = "test-user-id", ReservationDate = DateTime.UtcNow.AddDays(4), TableId = 1, IsIndor = false, NumberOfGuests = 3, IsCanceled = false },
                new Reservation { ReservationId = 5, UserId = "user2", ReservationDate = DateTime.UtcNow.AddDays(5), TableId = 2, IsIndor = true, NumberOfGuests = 5, IsCanceled = true }
            );
            dbContext.SaveChanges();
        }

        return dbContext;
    }
}
