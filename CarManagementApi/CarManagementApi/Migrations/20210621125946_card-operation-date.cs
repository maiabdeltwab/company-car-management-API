using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarManagementApi.Migrations
{
    public partial class cardoperationdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastOperation",
                table: "AccessCards",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastOperation",
                table: "AccessCards");
        }
    }
}
