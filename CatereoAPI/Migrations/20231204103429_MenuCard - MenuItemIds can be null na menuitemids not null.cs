using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class MenuCardMenuItemIdscanbenullnamenuitemidsnotnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuItemIds",
                table: "MenuCards",
                newName: "menuitemids");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "menuitemids",
                table: "MenuCards",
                newName: "MenuItemIds");
        }
    }
}
