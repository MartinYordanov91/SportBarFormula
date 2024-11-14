using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services
{
    public class CategoryService(IRepository<Category> repository) : ICategoryService
    {
        private readonly IRepository<Category> _repository = repository;
        public async Task<ICollection<CategoryViewModel>> GetAllCategoyAsinc()
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
    }
}
