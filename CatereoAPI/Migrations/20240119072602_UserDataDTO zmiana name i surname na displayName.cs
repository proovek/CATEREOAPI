using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class UserDataDTOzmiananameisurnamenadisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserDataDTO");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "UserDataDTO",
                newName: "displayName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "displayName",
                table: "UserDataDTO",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserDataDTO",
                type: "text",
                nullable: true);
        }
    }
}
