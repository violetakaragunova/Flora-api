using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class AddFrequencyTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequencyType",
                table: "PlantNeeds");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyTypeId",
                table: "PlantNeeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FrequencyType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequencyType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantNeeds_FrequencyTypeId",
                table: "PlantNeeds",
                column: "FrequencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantNeeds_FrequencyType_FrequencyTypeId",
                table: "PlantNeeds",
                column: "FrequencyTypeId",
                principalTable: "FrequencyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantNeeds_FrequencyType_FrequencyTypeId",
                table: "PlantNeeds");

            migrationBuilder.DropTable(
                name: "FrequencyType");

            migrationBuilder.DropIndex(
                name: "IX_PlantNeeds_FrequencyTypeId",
                table: "PlantNeeds");

            migrationBuilder.DropColumn(
                name: "FrequencyTypeId",
                table: "PlantNeeds");

            migrationBuilder.AddColumn<string>(
                name: "FrequencyType",
                table: "PlantNeeds",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
