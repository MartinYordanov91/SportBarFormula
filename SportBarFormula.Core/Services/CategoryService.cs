using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

public class CategoryService(IRepository<Category> repository) : ICategoryService
{
    private readonly IRepository<Category> _repository = repository;
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

    public async Task AddCategoryAsync(CategoryViewModel model)
    {
        var category = new Category()
        {
            Name = model.Name
        };

        await _repository.AddAsync(category);
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return new CategoryViewModel()
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };
    }

    public async Task UpdateCategoryAsync(CategoryViewModel model)
    {
        var category = await _repository.GetByIdAsync(model.CategoryId);

        if (category == null)
        {
            throw new Exception("Category not found");
        }

        category.Name = model.Name;
        await _repository.UpdateAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            throw new Exception("Category not found");
        }

        if (category.MenuItems.Any())
        {
           return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }
}
