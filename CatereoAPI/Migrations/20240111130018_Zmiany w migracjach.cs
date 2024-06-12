using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Zmianywmigracjach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCategory_CATEGORY_CategoryId",
                table: "MenuItemCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCategory_MenuItems_MenuItemId1",
                table: "MenuItemCategory");

            migrationBuilder.DropTable(
                name: "MenuItemIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemCategory",
                table: "MenuItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemCategory_CategoryId",
                table: "MenuItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemCategory_MenuItemId1",
                table: "MenuItemCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CATEGORY",
                table: "CATEGORY");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "MenuItemId1",
                table: "MenuItemCategory");

            migrationBuilder.RenameTable(
                name: "CATEGORY",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "MenuItemCategory",
                newName: "CATEGORYID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemCategory",
                table: "MenuItemCategory",
                columns: new[] { "CATEGORYID", "MenuItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateTable(
                name: "MenuItemIngredients",
                columns: table => new
                {
                    IngredientsIngredientId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemIngredients", x => new { x.IngredientsIngredientId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_MenuItemIngredients_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemIngredients_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCategory_MenuItemId",
                table: "MenuItemCategory",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredients_MenuItemId",
                table: "MenuItemIngredients",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCategory_Category_CATEGORYID",
                table: "MenuItemCategory",
                column: "CATEGORYID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemCategory_Category_CATEGORYID",
                table: "MenuItemCategory");

            migrationBuilder.DropTable(
                name: "MenuItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemCategory",
                table: "MenuItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemCategory_MenuItemId",
                table: "MenuItemCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "CATEGORY");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "CATEGORYID",
                table: "MenuItemCategory",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CATEGORY",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId1",
                table: "MenuItemCategory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemCategory",
                table: "MenuItemCategory",
                columns: new[] { "MenuItemId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CATEGORY",
                table: "CATEGORY",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");

            migrationBuilder.CreateTable(
                name: "MenuItemIngredient",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId1 = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_MenuItemIngredient_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemIngredient_MenuItems_MenuItemId1",
                        column: x => x.MenuItemId1,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCategory_CategoryId",
                table: "MenuItemCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemCategory_MenuItemId1",
                table: "MenuItemCategory",
                column: "MenuItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredient_IngredientId",
                table: "MenuItemIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredient_MenuItemId1",
                table: "MenuItemIngredient",
                column: "MenuItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCategory_CATEGORY_CategoryId",
                table: "MenuItemCategory",
                column: "CategoryId",
                principalTable: "CATEGORY",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemCategory_MenuItems_MenuItemId1",
                table: "MenuItemCategory",
                column: "MenuItemId1",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
