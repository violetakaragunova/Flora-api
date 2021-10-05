using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class AddingPlantFamilyNameAtributeToPlantEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlantFamilyName",
                table: "Plants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantFamilyName",
                table: "Plants");
        }
    }
}
