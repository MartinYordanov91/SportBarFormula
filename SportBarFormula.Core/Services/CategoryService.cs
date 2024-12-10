using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

public class CategoryService(
    IRepository<Category> repository
    ) : ICategoryService
{

    private readonly IRepository<Category> _repository = repository;


    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A collection of CategoryViewModel containing category details.</returns>
    public async Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories
            .Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
            })
            .ToList();
    }


    /// <summary>
    /// Adds a new category.
    /// </summary>
    /// <param name="model">The view model containing category details to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddCategoryAsync(CategoryViewModel model)
    {
        var category = new Category()
        {
            Name = model.Name
        };

        await _repository.AddAsync(category);
    }


    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The view model containing category details.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no categories are found in the repository.</exception>
    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
        Category category;

        try
        {
            category = await _repository.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No categories found in the repository.");
        }

        return new CategoryViewModel()
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };
    }


    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="model">The view model containing updated category details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no categories are found in the repository.</exception>
    public async Task UpdateCategoryAsync(CategoryViewModel model)
    {
        Category category;

        try
        {
            category = await _repository.GetByIdAsync(model.CategoryId);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No categories found in the repository.");
        }

        category.Name = model.Name;
        await _repository.UpdateAsync(category);
    }


    /// <summary>
    /// Deletes a category by its ID if it has no associated menu items.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no categories are found in the repository.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the category has associated menu items.</exception>
    public async Task DeleteCategoryAsync(int id)
    {
        Category category;

        try
        {
            category = await _repository.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No categories found in the repository.");
        }

        if (category.MenuItems.Any())
        {
            throw new InvalidOperationException("Cannot delete category because it has associated menu items.");
        }

        await _repository.DeleteAsync(id);
    }


    /// <summary>
    /// Retrieves a category by its name.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <returns>The view model containing category details.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no categories are found in the repository.</exception>
    /// <exception cref="ArgumentException">Thrown when the category with the specified name is not found.</exception>
    public async Task<CategoryViewModel> GetCategoryByNameAsync(string name)
    {
        IEnumerable<Category> allcategory;

        try
        {
            allcategory = await _repository.GetAllAsync();
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No categories found in the repository.");
        }

        if (!allcategory.Any())
        {
            throw new InvalidOperationException("No categories found in the repository.");
        }

        var category = allcategory.FirstOrDefault(c => c.Name == name);

        if (category == null)
        {
            throw new ArgumentException($"Category with name '{name}' not found.");
        }

        return new CategoryViewModel()
        {
            CategoryId = category.CategoryId,
            Name = name
        };
    }


}
