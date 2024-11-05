using SportBarFormula.Infastructure.Data.Models;

namespace SportBarFormula.Infastructure.Data.SeedData;

public static class MenuItemsSeeds
{
    public static IEnumerable<MenuItem> GenerateMenuItems()
    {
        return new List<MenuItem>()
        {
            new MenuItem()
            {
                Name = "Чориззо",
                Description = "тестова рецепта ",
                Price = 10.99M,
                Category = new Category { Name = "Пици" },
                ImageURL = "https://photos.fife.usercontent.google.com/pw/AP1GczPjbg3nRj01AJP_kW1FMV_yeooafZOz8X0vJuFx1tGPOq8nHbshZFuE=w1200-h800-s-no-gm?authuser=0",
                IsAvailable = true,
                Ingredients = "Доматена паста, Моцарела, Чоризо, Люта Чушка, Червен Лук, Черна Маслина, Пресен Домат ",
                PreparationTime = 5,
                IsDeleted = false
            },
        };
    }
}

