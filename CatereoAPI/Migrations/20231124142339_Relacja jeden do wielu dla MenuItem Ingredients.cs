using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class RelacjajedendowieludlaMenuItemIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "MenuItemIngredient");

            migrationBuilder.RenameColumn(
                name: "MenuCardId",
                table: "MenuItems",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuCardId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuId");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "Ingredient",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WHIngredientId",
                table: "Ingredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_MenuItemId",
                table: "Ingredient",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_MenuItems_MenuItemId",
                table: "Ingredient",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCards_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_MenuItems_MenuItemId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCards_MenuId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_MenuItemId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "WHIngredientId",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "MenuItems",
                newName: "MenuCardId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuCardId");

            migrationBuilder.CreateTable(
                name: "MenuItemIngredient",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemIngredient", x => new { x.MenuItemId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_MenuItemIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemIngredient_Ingredient_IngredientId1",
                        column: x => x.IngredientId1,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId");
                    table.ForeignKey(
                        name: "FK_MenuItemIngredient_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredient_IngredientId",
                table: "MenuItemIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredient_IngredientId1",
                table: "MenuItemIngredient",
                column: "IngredientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCards_MenuCardId",
                table: "MenuItems",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "MenuCardId");
        }
    }
}
