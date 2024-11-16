﻿using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.MenuItem;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

public class MenuItemService(IRepository<MenuItem> repository) : IMenuItemService
{
    private readonly IRepository<MenuItem> _repository = repository;

    public async Task AddMenuItemAsync(CreateMenuItemViewModel model)
    {
        MenuItem newMenuItem = new MenuItem()
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

        await _repository.AddAsync(newMenuItem);
    }

    public async Task<MenuItemDetailsViewModel> GetMenuItemDetailsByIdAsync(int id)
    {
        var menuItem = await _repository.GetByIdAsync(id);

        if (menuItem == null)
        {
           throw new ArgumentNullException(nameof(menuItem));
        }

        var viewModel = new MenuItemDetailsViewModel
        {
            MenuItemId = menuItem.MenuItemId,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            Quantity = menuItem.Quantity,
            Category = menuItem.Category.Name,
            CategoryId = menuItem.CategoryId,
            ImageURL = menuItem.ImageURL,
            IsAvailable = menuItem.IsAvailable,
            Ingredients = menuItem.Ingredients,
            PreparationTime = menuItem.PreparationTime
        };

        return viewModel;
    }

    public async Task<ICollection<MenuItemCardViewModel>> GetCardMenuItemsAsync()
    {
        var menuItemColection = await _repository.GetAllAsync();

        return menuItemColection
         .Where(mi => mi.IsDeleted == false)
         .Select(mi => new MenuItemCardViewModel
         {
             MenuItemId = mi.MenuItemId,
             Name = mi.Name,
             Price = mi.Price,
             Quantity = mi.Quantity,
             ImageURL = mi.ImageURL,
             Ingredients = mi.Ingredients,
             CategoryId = mi.CategoryId,
         })
         .ToList();
    }

    public async Task<ICollection<MenuItemCardViewModel>> GetMenuItemsByCategoryAsync(int? categoryId)
    {
        var menuItems = await _repository.GetAllAsync();

        var filteredMenuItems = menuItems
            .Where(mi => !categoryId.HasValue || mi.CategoryId == categoryId)
            .Select(mi => new MenuItemCardViewModel
            {
                MenuItemId = mi.MenuItemId,
                Name = mi.Name,
                ImageURL = mi.ImageURL,
                Ingredients = mi.Ingredients,
                Price = mi.Price,
                Quantity = mi.Quantity,
                CategoryId = mi.CategoryId
            })
            .ToList();

        return filteredMenuItems;
    }

    public async Task<MenuItemViewModel> GetMenuItemByIdAsync(int id)
    {
        var currentItem = await _repository.GetByIdAsync(id);

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
