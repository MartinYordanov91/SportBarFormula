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
}
