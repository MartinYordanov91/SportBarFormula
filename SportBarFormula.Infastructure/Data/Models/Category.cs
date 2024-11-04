using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SportBarFormula.Infastructure.Constants.DataConstants.CategoryConstants;

namespace SportBarFormula.Infastructure.Data.Models;

public class Category
{
    [Key]
    [Comment("Unique identifier of the Category")]
    public int CategoryId { get; set; }

    [MaxLength(CategoryNameMaxLength)]
    [Comment("Category name (drinks, pizzas)")]
    public required string Name { get; set; }
}