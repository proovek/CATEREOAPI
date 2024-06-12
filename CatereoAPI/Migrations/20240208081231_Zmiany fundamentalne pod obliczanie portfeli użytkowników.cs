using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianyfundamentalnepodobliczanieportfeliużytkowników : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Credits",
                table: "UserData",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Credits",
                table: "AspNetUsers",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credits",
                table: "UserData");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "AspNetUsers");
        }
    }
}
