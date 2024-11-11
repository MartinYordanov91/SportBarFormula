using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Infrastructure.Data.SeedData;

public class CategorySeeds
{
    public static IEnumerable<Category> GenerateCategorys()
    {
        return new List<Category>()
        {
            new Category{
                CategoryId = 1,
                Name = "Закуска"
            },

            new Category{
                CategoryId = 2,
                Name = "Предястия"
            },

            new Category{
                CategoryId = 3,
                Name = "Салати"
            },

            new Category{
                CategoryId = 4,
                Name = "Сандвичи"
            },

            new Category{
                CategoryId = 5,
                Name = "Бургери"
            },

            new Category{
                CategoryId = 6,
                Name = "Мезета"
            },

            new Category{
                CategoryId = 7,
                Name = "Паста/Ризото"
            },

            new Category{
                CategoryId = 8,
                Name = "Сръбска скара"
            },

            new Category{
                CategoryId = 9,
                Name = "Основни"
            },

            new Category{
                CategoryId = 10,
                Name = "Пица"
            },

            new Category{
                CategoryId = 11,
                Name = "Дресинг/Сосове"
            },

            new Category{
                CategoryId = 12,
                Name = "Пърленка"
            },

            new Category{
                CategoryId = 13,
                Name = "Десерти"
            },

            new Category{
                CategoryId = 14,
                Name = "Топли Напитки"
            },

            new Category{
                CategoryId = 15,
                Name = "Студени кафе напитки"
            },

            new Category{
                CategoryId = 16,
                Name = "Безалкохолни напитки"
            },

            new Category{
                CategoryId = 17,
                Name = "Фрешове"
            },

            new Category{
                CategoryId = 18,
                Name = "Безалкохолни коктейли"
            },

            new Category{
                CategoryId = 19,
                Name = "Алкохолни коктейли"
            },

            new Category{
                CategoryId = 20,
                Name = "Бира"
            },

            new Category{
                CategoryId = 21,
                Name = "Ядки"
            },

            new Category{
                CategoryId = 22,
                Name = "Водка"
            },

            new Category{
                CategoryId = 23,
                Name = "Джин"
            },

            new Category{
                CategoryId = 24,
                Name = "Текила"
            },

            new Category{
                CategoryId = 25,
                Name = "Шотове"
            },

            new Category{
                CategoryId = 26,
                Name = "Уиски"
            },

            new Category{
                CategoryId = 27,
                Name = "Ром"
            },

            new Category{
                CategoryId = 28,
                Name = "Коняк"
            },

            new Category{
                CategoryId = 29,
                Name = "Дижестиви"
            },

            new Category{
                CategoryId = 30,
                Name = "Анасонови"
            },

            new Category{
                CategoryId = 31,
                Name = "Ликьори/Аперитиви"
            },

            new Category{
                CategoryId = 32,
                Name = "Ракии"
            },

            new Category{
                CategoryId = 33,
                Name = "Вино"
            },
        };
    }
}
