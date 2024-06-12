using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class MenuCardMenuItemIdscanbenull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "MenuItemIds",
                table: "MenuCards",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "MenuItemIds",
                table: "MenuCards",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0],
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true);
        }
    }
}
