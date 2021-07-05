using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class EntityFrequencyTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "FrequencyTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FrequencyTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Days",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FrequencyTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Days",
                value: 7);

            migrationBuilder.UpdateData(
                table: "FrequencyTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Days",
                value: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "FrequencyTypes");
        }
    }
}
