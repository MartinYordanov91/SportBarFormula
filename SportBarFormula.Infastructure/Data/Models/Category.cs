using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SportBarFormula.Infrastructure.Constants.DataConstants.CategoryConstants;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("Group menu items by categories such as drinks, main courses, desserts.")]
public class Category
{
    public Category()
    {
        this.MenuItems = new HashSet<MenuItem>();
    }

    [Key]
    [Comment("Unique identifier of the Category")]
    public int CategoryId { get; set; }

    [MaxLength(CategoryNameMaxLength)]
    [Comment("Category name (drinks, pizzas)")]
    public required string Name { get; set; }


    [Comment("Collection of menu items associated with this category")]
    public virtual ICollection<MenuItem> MenuItems { get; set; }
}