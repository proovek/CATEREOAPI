using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class TestzmianMenuItemorazMenuCard4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string[]>(
                name: "Ingredients",
                table: "MenuItems",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                table: "MenuItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]");
        }
    }
}
