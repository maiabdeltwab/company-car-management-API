using Microsoft.EntityFrameworkCore.Migrations;

namespace CarManagementApi.Migrations
{
    public partial class edit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCards_Cars_CarId",
                table: "AccessCards");

            migrationBuilder.DropIndex(
                name: "IX_AccessCards_CarId",
                table: "AccessCards");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "AccessCards");

            migrationBuilder.AddColumn<int>(
                name: "AccessCardId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AccessCardId",
                table: "Cars",
                column: "AccessCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AccessCards_AccessCardId",
                table: "Cars",
                column: "AccessCardId",
                principalTable: "AccessCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AccessCards_AccessCardId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AccessCardId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AccessCardId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "AccessCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessCards_CarId",
                table: "AccessCards",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCards_Cars_CarId",
                table: "AccessCards",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
