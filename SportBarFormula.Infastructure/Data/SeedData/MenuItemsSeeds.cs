using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Infrastructure.Data.SeedData;

public static class MenuItemsSeeds
{
    public static IEnumerable<MenuItem> GenerateMenuItems()
    {
        return new List<MenuItem>()
        {
            new MenuItem()
            {
                MenuItemId = 1,
               Name = "Чориззо",
               Description = "Почувствайте аромата на прясно изпечен, хрупкав блат, намазан с богата доматена салса, овкусена с риган и босилек. Върху него се разстила нежен слой разтопена моцарела, която съчетава вкусовете на апетитните парчета салам чоризо. Нотките на червен сладък лук и тънко нарязани черни маслини допълват вкусовото удоволствие, докато свежите доматени резени и парченца люта чушка добавят свежест и пикантност." +
                "Прекрасно съчетание на вкусове, което ви кара да се върнете за още!",
               Price = 10.90M,
               Quantity = 480,
               CategoryId = 10,
               ImageURL = "assets/img/Menu/Pizza/Chorizzo.jpg",
               IsAvailable = true,
               Ingredients = "Доматена паста, Моцарела, Чоризо, Люта Чушка, Червен Лук, Черна Маслина, Пресен Домат ",
               PreparationTime = 5,
               IsDeleted = false
            },
        };
    }
}

