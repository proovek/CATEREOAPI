using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianywstrukturachbazdanych : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarginalRate",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserDataDTO");

            migrationBuilder.DropColumn(
                name: "displayName",
                table: "UserDataDTO");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "UserData",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "UserData");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "MarginalRate",
                table: "UserDataDTO",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "UserDataDTO",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "UserDataDTO",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserDataDTO",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UserDataDTO",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserDataDTO",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "displayName",
                table: "UserDataDTO",
                type: "text",
                nullable: true);
        }
    }
}
