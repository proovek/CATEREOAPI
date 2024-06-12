using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class ZmianywstrukturachdanychUżytkownika2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApllicationUserCustomerCompany");

            migrationBuilder.CreateTable(
                name: "ApllicationUserCustomerCompanyDTO",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    CustomerCompanyDTOId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApllicationUserCustomerCompanyDTO", x => new { x.ApplicationUserId, x.CustomerCompanyDTOId });
                    table.ForeignKey(
                        name: "FK_ApllicationUserCustomerCompanyDTO_AspNetUsers_ApplicationUs~",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApllicationUserCustomerCompanyDTO_CustomerCompanyDTO_Custom~",
                        column: x => x.CustomerCompanyDTOId,
                        principalTable: "CustomerCompanyDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApllicationUserCustomerCompanyDTO_CustomerCompanyDTOId",
                table: "ApllicationUserCustomerCompanyDTO",
                column: "CustomerCompanyDTOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApllicationUserCustomerCompanyDTO");

            migrationBuilder.CreateTable(
                name: "ApllicationUserCustomerCompany",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    CustomerCompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApllicationUserCustomerCompany", x => new { x.ApplicationUserId, x.CustomerCompanyId });
                    table.ForeignKey(
                        name: "FK_ApllicationUserCustomerCompany_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApllicationUserCustomerCompany_CustomerCompany_CustomerComp~",
                        column: x => x.CustomerCompanyId,
                        principalTable: "CustomerCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApllicationUserCustomerCompany_CustomerCompanyId",
                table: "ApllicationUserCustomerCompany",
                column: "CustomerCompanyId");
        }
    }
}
