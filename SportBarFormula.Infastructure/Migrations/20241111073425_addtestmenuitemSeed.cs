using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtestmenuitemSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemId", "CategoryId", "Description", "ImageURL", "Ingredients", "IsAvailable", "IsDeleted", "Name", "PreparationTime", "Price" },
                values: new object[] { 1, 10, "Почувствайте аромата на прясно изпечен, хрупкав блат, намазан с богата доматена салса, овкусена с риган и босилек. Върху него се разстила нежен слой разтопена моцарела, която съчетава вкусовете на апетитните парчета салам чоризо. Нотките на червен сладък лук и тънко нарязани черни маслини допълват вкусовото удоволствие, докато свежите доматени резени и парченца люта чушка добавят свежест и пикантност.Прекрасно съчетание на вкусове, което ви кара да се върнете за още!", "https://photos.fife.usercontent.google.com/pw/AP1GczPjbg3nRj01AJP_kW1FMV_yeooafZOz8X0vJuFx1tGPOq8nHbshZFuE=w1200-h800-s-no-gm?authuser=0", "Доматена паста, Моцарела, Чоризо, Люта Чушка, Червен Лук, Черна Маслина, Пресен Домат ", true, false, "Чориззо", 5, 10.90m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1);
        }
    }
}
