using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatereoAPI.Migrations
{
    public partial class Czasypracypracownikówobliczenia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeesWorkingTime",
                columns: table => new
                {
                    userName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    HourlyRate = table.Column<double>(type: "double precision", nullable: false),
                    Hours = table.Column<double>(type: "double precision", nullable: false),
                    Days = table.Column<double>(type: "double precision", nullable: false),
                    MonthlySalary = table.Column<double>(type: "double precision", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    isNotOlderThan26 = table.Column<bool>(type: "boolean", nullable: false),
                    PensionContribution = table.Column<double>(type: "double precision", nullable: false),
                    DisabilityContribution = table.Column<double>(type: "double precision", nullable: false),
                    AccidentContribution = table.Column<double>(type: "double precision", nullable: false),
                    LaborFoundContribution = table.Column<double>(type: "double precision", nullable: false),
                    HealthCareContribution = table.Column<double>(type: "double precision", nullable: false),
                    VacationDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesWorkingTime", x => x.userName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesWorkingTime");
        }
    }
}
