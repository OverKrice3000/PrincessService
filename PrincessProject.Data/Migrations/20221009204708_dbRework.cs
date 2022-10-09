using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrincessProject.Data.Migrations
{
    public partial class dbRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptData_Attempts_AttemptId",
                table: "AttemptData");

            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.DropIndex(
                name: "IX_AttemptData_AttemptId",
                table: "AttemptData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChosenValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttemptData_AttemptId",
                table: "AttemptData",
                column: "AttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptData_Attempts_AttemptId",
                table: "AttemptData",
                column: "AttemptId",
                principalTable: "Attempts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
