using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Trochezmian2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCards_MenuCards_MenuCardId",
                table: "MenuItemCards");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemCards_MenuCardId",
                table: "MenuItemCards");

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
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuCardMenuItemCards_MenuItemCardId",
                table: "MenuCardMenuItemCards",
                column: "MenuItemCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuCardMenuItemCards");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCards_MenuCardId",
                table: "MenuItemCards",
                column: "MenuCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCards_MenuCards_MenuCardId",
                table: "MenuItemCards",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
