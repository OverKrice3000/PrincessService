using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrincessProject.Data.Migrations
{
    public partial class CandidateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateOrder",
                table: "AttemptData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateOrder",
                table: "AttemptData");
        }
    }
}
