using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantTrackerAPI.DataAccessLayer.Migrations
{
    public partial class RelationBetweenPlantNeedAndMonthAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthFrom",
                table: "PlantNeeds");

            migrationBuilder.DropColumn(
                name: "MonthTo",
                table: "PlantNeeds");

            migrationBuilder.AddColumn<int>(
                name: "MonthFromId",
                table: "PlantNeeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthToId",
                table: "PlantNeeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlantNeeds_MonthFromId",
                table: "PlantNeeds",
                column: "MonthFromId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantNeeds_MonthToId",
                table: "PlantNeeds",
                column: "MonthToId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantNeeds_Months_MonthFromId",
                table: "PlantNeeds",
                column: "MonthFromId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantNeeds_Months_MonthToId",
                table: "PlantNeeds",
                column: "MonthToId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantNeeds_Months_MonthFromId",
                table: "PlantNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantNeeds_Months_MonthToId",
                table: "PlantNeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantNeeds_MonthFromId",
                table: "PlantNeeds");

            migrationBuilder.DropIndex(
                name: "IX_PlantNeeds_MonthToId",
                table: "PlantNeeds");

            migrationBuilder.DropColumn(
                name: "MonthFromId",
                table: "PlantNeeds");

            migrationBuilder.DropColumn(
                name: "MonthToId",
                table: "PlantNeeds");

            migrationBuilder.AddColumn<int>(
                name: "MonthFrom",
                table: "PlantNeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthTo",
                table: "PlantNeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
