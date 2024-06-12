using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class MenuCardMenuItemIdscanbenullandListmenuitem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
