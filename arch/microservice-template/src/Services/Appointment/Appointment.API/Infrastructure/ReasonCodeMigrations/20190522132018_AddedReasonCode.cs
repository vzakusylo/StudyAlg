using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointment.API.Infrastructure.ReasonCodeMigrations
{
    public partial class AddedReasonCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "reason_code_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "ReasonCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonCode", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReasonCode");

            migrationBuilder.DropSequence(
                name: "reason_code_hilo");
        }
    }
}
