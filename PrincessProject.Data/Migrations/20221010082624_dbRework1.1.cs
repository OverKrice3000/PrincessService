using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrincessProject.Data.Migrations
{
    public partial class dbRework11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttemptData",
                table: "AttemptData");

            migrationBuilder.RenameTable(
                name: "AttemptData",
                newName: "Attempts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attempts",
                table: "Attempts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attempts",
                table: "Attempts");

            migrationBuilder.RenameTable(
                name: "Attempts",
                newName: "AttemptData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttemptData",
                table: "AttemptData",
                column: "Id");
        }
    }
}
