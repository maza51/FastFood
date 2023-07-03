using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Infrastructure.Migrations
{
    public partial class AddDateCreatedForProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 25, 15, 46, 13, 539, DateTimeKind.Local).AddTicks(9010));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");
        }
    }
}
