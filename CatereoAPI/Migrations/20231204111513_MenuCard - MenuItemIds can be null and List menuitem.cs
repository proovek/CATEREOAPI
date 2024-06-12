using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class MenuCardMenuItemIdscanbenullandListmenuitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "menuitemids",
                table: "MenuCards");

            migrationBuilder.AddColumn<int>(
                name: "MenuCardId",
                table: "MenuItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuCardId",
                table: "MenuItems",
                column: "MenuCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuCardId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuCardId",
                table: "MenuItems");

            migrationBuilder.AddColumn<int[]>(
                name: "menuitemids",
                table: "MenuCards",
                type: "integer[]",
                nullable: true);
        }
    }
}
