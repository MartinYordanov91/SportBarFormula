using System.ComponentModel.DataAnnotations;
using static SportBarFormula.Infrastructure.Constants.DataConstants.CategoryConstants;

namespace SportBarFormula.Core.ViewModels.Category;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
    public required string Name { get; set; }
}
