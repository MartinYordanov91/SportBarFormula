using SportBarFormula.Core.ViewModels.Category;

namespace SportBarFormula.Core.Services.Contracts;

public interface ICategoryService
{
    public Task<ICollection<CategoryViewModel>> GetAllCategoriesAsync();

    public Task AddCategoryAsync(CategoryViewModel model);

    public Task<CategoryViewModel?> GetCategoryByIdAsync(int id);

    public Task<CategoryViewModel?> GetCategoryByNameAsync(string name);

    public Task UpdateCategoryAsync(CategoryViewModel model);

    public Task<bool> DeleteCategoryAsync(int id);
}
