using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class AddingFrequencyTypeDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantNeeds_FrequencyType_FrequencyTypeId",
                table: "PlantNeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FrequencyType",
                table: "FrequencyType");

            migrationBuilder.RenameTable(
                name: "FrequencyType",
                newName: "FrequencyTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FrequencyTypes",
                table: "FrequencyTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantNeeds_FrequencyTypes_FrequencyTypeId",
                table: "PlantNeeds",
                column: "FrequencyTypeId",
                principalTable: "FrequencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantNeeds_FrequencyTypes_FrequencyTypeId",
                table: "PlantNeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FrequencyTypes",
                table: "FrequencyTypes");

            migrationBuilder.RenameTable(
                name: "FrequencyTypes",
                newName: "FrequencyType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FrequencyType",
                table: "FrequencyType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantNeeds_FrequencyType_FrequencyTypeId",
                table: "PlantNeeds",
                column: "FrequencyTypeId",
                principalTable: "FrequencyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
