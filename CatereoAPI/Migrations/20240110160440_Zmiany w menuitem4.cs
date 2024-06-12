using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianywmenuitem4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ALLERGENINFORMATION",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "CATEGORY",
                table: "MenuItems",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "MenuItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PopularityRating",
                table: "MenuItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "MenuItems",
                newName: "CATEGORY");

            migrationBuilder.AlterColumn<int>(
                name: "SKU",
                table: "MenuItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "PopularityRating",
                table: "MenuItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ALLERGENINFORMATION",
                table: "MenuItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "MenuItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
