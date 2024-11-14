﻿using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.MenuItem;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Core.Services;

public class MenuItemService(SportBarFormulaDbContext context) : IMenuItemService
{
    private readonly SportBarFormulaDbContext _context = context;

    public async Task AddMenuItem(CreateMenuItemViewModel model)
    {
        var newMenuItem = new MenuItem()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity,
            ImageURL = model.ImageURL,
            CategoryId = model.CategoryId,
            Ingredients = model.Ingredients,
            PreparationTime = model.PreparationTime,
            IsDeleted = false,
            IsAvailable = true,
        };

        await _context.MenuItems.AddAsync(newMenuItem);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<MenuItemViewModel>> GetAllMenuItemsAsync()
    {
        return await _context.MenuItems
             .Where(mi => mi.IsDeleted == false)
             .Select(mi => new MenuItemViewModel
             {
                 Name = mi.Name,
                 Description = mi.Description,
                 Category = mi.Category.Name,
                 Price = mi.Price,
                 Quantity = mi.Quantity,
                 PreparationTime = mi.PreparationTime,
                 ImageURL = mi.ImageURL,
                 Ingredients = mi.Ingredients,
                 IsAvailable = mi.IsAvailable,
                 IsDeleted = mi.IsDeleted,
             })
             .ToListAsync();
    }

    public async Task<MenuItemViewModel> GetMenuItemByIdAsync(int id)
    {
        var currentItem = await _context.MenuItems
               .Where(mi => mi.IsDeleted == false)
               .FirstOrDefaultAsync(mi => mi.MenuItemId == id);

        if (currentItem == null)
        {
            throw new NullReferenceException("Invalid MenuItem Id");
        }

        var model = new MenuItemViewModel
        {
            Name = currentItem.Name,
            Description = currentItem.Description,
            Category = currentItem.Category.Name,
            Price = currentItem.Price,
            Quantity = currentItem.Quantity,
            PreparationTime = currentItem.PreparationTime,
            ImageURL = currentItem.ImageURL,
            Ingredients = currentItem.Ingredients,
            IsAvailable = currentItem.IsAvailable,
            IsDeleted = currentItem.IsDeleted,
        };

        return model;
    }

}
