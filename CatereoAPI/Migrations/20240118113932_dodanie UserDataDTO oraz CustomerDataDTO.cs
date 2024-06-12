using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class dodanieUserDataDTOorazCustomerDataDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "CustomerCompanyDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompanyDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDataDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    MarginalRate = table.Column<double>(type: "double precision", nullable: true),
                    OrderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDataDTO_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerCompanyDTOUserDataDTO",
                columns: table => new
                {
                    CustomerCompanyDTOId = table.Column<int>(type: "integer", nullable: false),
                    UserDataDTOId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompanyDTOUserDataDTO", x => new { x.CustomerCompanyDTOId, x.UserDataDTOId });
                    table.ForeignKey(
                        name: "FK_CustomerCompanyDTOUserDataDTO_CustomerCompanyDTO_CustomerCo~",
                        column: x => x.CustomerCompanyDTOId,
                        principalTable: "CustomerCompanyDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCompanyDTOUserDataDTO_UserDataDTO_UserDataDTOId",
                        column: x => x.UserDataDTOId,
                        principalTable: "UserDataDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCompanyDTOUserDataDTO_UserDataDTOId",
                table: "CustomerCompanyDTOUserDataDTO",
                column: "UserDataDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDataDTO_OrderId",
                table: "UserDataDTO",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCompanyDTOUserDataDTO");

            migrationBuilder.DropTable(
                name: "CustomerCompanyDTO");

            migrationBuilder.DropTable(
                name: "UserDataDTO");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
