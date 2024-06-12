using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianywmenuitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllergenInformation",
                table: "MenuItems",
                newName: "allergeninformation");

            migrationBuilder.AddColumn<int>(
                name: "SKU",
                table: "MenuItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKU",
                table: "MenuItems");

            migrationBuilder.RenameColumn(
                name: "allergeninformation",
                table: "MenuItems",
                newName: "AllergenInformation");
        }
    }
}
