using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianywmenuitem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "allergeninformation",
                table: "MenuItems",
                newName: "ALLERGENINFORMATION");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "MenuItems",
                newName: "CATEGORY");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CATEGORY",
                table: "MenuItems",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "ALLERGENINFORMATION",
                table: "MenuItems",
                newName: "allergeninformation");
        }
    }
}
