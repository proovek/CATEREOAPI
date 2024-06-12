using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class dodanieMenuItemDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuCardMenuItem");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "MenuCards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuCardMenuItemDTO",
                columns: table => new
                {
                    MenuCardId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCardMenuItemDTO", x => new { x.MenuCardId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItemDTO_MenuCards_MenuCardId",
                        column: x => x.MenuCardId,
                        principalTable: "MenuCards",
                        principalColumn: "MenuCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuCardMenuItemDTO_MenuItemDTO_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItemDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuCards_MenuItemId",
                table: "MenuCards",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCardMenuItemDTO_MenuItemId",
                table: "MenuCardMenuItemDTO",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCards_MenuItems_MenuItemId",
                table: "MenuCards",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCards_MenuItems_MenuItemId",
                table: "MenuCards");

            migrationBuilder.DropTable(
                name: "MenuCardMenuItemDTO");

            migrationBuilder.DropIndex(
                name: "IX_MenuCards_MenuItemId",
                table: "MenuCards");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "MenuCards");

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
    }
}
