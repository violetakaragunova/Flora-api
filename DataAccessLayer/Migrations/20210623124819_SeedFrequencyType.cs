using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class SeedFrequencyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FrequencyType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Daily" });

            migrationBuilder.InsertData(
                table: "FrequencyType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Weekly" });

            migrationBuilder.InsertData(
                table: "FrequencyType",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Monthly" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FrequencyType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FrequencyType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FrequencyType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
