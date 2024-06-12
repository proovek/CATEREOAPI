using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Trochezmian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCard_MenuCards_MenuCardId",
                table: "MenuItemCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemCard",
                table: "MenuItemCard");

            migrationBuilder.RenameTable(
                name: "MenuItemCard",
                newName: "MenuItemCards");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemCard_MenuCardId",
                table: "MenuItemCards",
                newName: "IX_MenuItemCards_MenuCardId");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "MenuItemCards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemCards",
                table: "MenuItemCards",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCards_MenuCards_MenuCardId",
                table: "MenuItemCards",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCards_MenuCards_MenuCardId",
                table: "MenuItemCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemCards",
                table: "MenuItemCards");

            migrationBuilder.RenameTable(
                name: "MenuItemCards",
                newName: "MenuItemCard");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemCards_MenuCardId",
                table: "MenuItemCard",
                newName: "IX_MenuItemCard_MenuCardId");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "MenuItemCard",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemCard",
                table: "MenuItemCard",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCard_MenuCards_MenuCardId",
                table: "MenuItemCard",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId");
        }
    }
}
