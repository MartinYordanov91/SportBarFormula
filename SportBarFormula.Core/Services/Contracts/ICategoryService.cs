using SportBarFormula.Core.ViewModels.Category;

namespace SportBarFormula.Core.Services.Contracts;

public interface ICategoryService
{
    public Task<ICollection<CategoryViewModel>> GetAllCategoyAsinc();
}
