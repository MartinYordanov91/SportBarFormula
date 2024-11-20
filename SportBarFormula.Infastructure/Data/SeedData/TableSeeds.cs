using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Infrastructure.Data.SeedData;

public static class TableSeeds
{
    public static IEnumerable<Table> GenerateTables()
    {
        return new List<Table>()
        {
            new Table()
            {
                TableId = 1,
                Capacity = 6,
                TableNumber = "100",
                Location ="indoor",
                IsAvailable = true,
            },

            new Table()
            {
                TableId = 2,
                Capacity = 6,
                TableNumber = "101",
                Location ="indoor",
                IsAvailable = true,
            },

            new Table()
            {
                TableId = 3,
                Capacity = 6,
                TableNumber = "102",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 4,
                Capacity = 6,
                TableNumber = "103",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 5,
                Capacity = 6,
                TableNumber = "104",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 6,
                Capacity = 6,
                TableNumber = "105",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 7,
                Capacity = 6,
                TableNumber = "106",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 8,
                Capacity = 4,
                TableNumber = "107",
                Location ="indoor",
                IsAvailable = true,
            },


            new Table()
            {
                TableId = 9,
                Capacity = 4,
                TableNumber = "108",
                Location ="indoor",
                IsAvailable = true,
            },
        };
    }
}
