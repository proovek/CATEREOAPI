using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class WielkiezmianywdziałaniuMenuCardzMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuCardMenuItemCards");

            migrationBuilder.DropTable(
                name: "MenuItemCards");

            migrationBuilder.CreateTable(
                name: "MenuCardMenuItem",
                columns: table => new
                {
                    MenuCardId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCardMenuItem", x => new { x.MenuCardId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItem_MenuCards_MenuCardId",
                        column: x => x.MenuCardId,
                        principalTable: "MenuCards",
                        principalColumn: "MenuCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItem_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuCardMenuItem_MenuItemId",
                table: "MenuCardMenuItem",
                column: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuCardMenuItem");

            migrationBuilder.CreateTable(
                name: "MenuItemCards",
                columns: table => new
                {
                    MenuItemCardId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AllergenInformation = table.Column<string>(type: "text", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    IsVegan = table.Column<bool>(type: "boolean", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MenuCardId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PopularityRating = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemCards", x => x.MenuItemCardId);
                });

            migrationBuilder.CreateTable(
                name: "MenuCardMenuItemCards",
                columns: table => new
                {
                    MenuCardId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemCardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCardMenuItemCards", x => new { x.MenuCardId, x.MenuItemCardId });
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItemCards_MenuCards_MenuCardId",
                        column: x => x.MenuCardId,
                        principalTable: "MenuCards",
                        principalColumn: "MenuCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItemCards_MenuItemCards_MenuItemCardId",
                        column: x => x.MenuItemCardId,
                        principalTable: "MenuItemCards",
                        principalColumn: "MenuItemCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuCardMenuItemCards_MenuItemCardId",
                table: "MenuCardMenuItemCards",
                column: "MenuItemCardId");
        }
    }
}
