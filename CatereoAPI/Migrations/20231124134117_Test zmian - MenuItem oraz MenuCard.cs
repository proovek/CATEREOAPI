using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class TestzmianMenuItemorazMenuCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "MenuItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AlterColumn<int>(
                name: "MenuCardId",
                table: "MenuItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
