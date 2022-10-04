using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrincessProject.Data.Migrations
{
    public partial class Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Attempts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "happiness",
                table: "Attempts",
                newName: "ChosenValue");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AttemptData",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "candidate_value",
                table: "AttemptData",
                newName: "CandidateValue");

            migrationBuilder.RenameColumn(
                name: "candidate_surname",
                table: "AttemptData",
                newName: "CandidateSurname");

            migrationBuilder.RenameColumn(
                name: "candidate_name",
                table: "AttemptData",
                newName: "CandidateName");

            migrationBuilder.RenameColumn(
                name: "attempt_id",
                table: "AttemptData",
                newName: "AttemptId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptData_Attempts_AttemptId",
                table: "AttemptData");

            migrationBuilder.DropIndex(
                name: "IX_AttemptData_AttemptId",
                table: "AttemptData");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attempts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ChosenValue",
                table: "Attempts",
                newName: "happiness");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AttemptData",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CandidateValue",
                table: "AttemptData",
                newName: "candidate_value");

            migrationBuilder.RenameColumn(
                name: "CandidateSurname",
                table: "AttemptData",
                newName: "candidate_surname");

            migrationBuilder.RenameColumn(
                name: "CandidateName",
                table: "AttemptData",
                newName: "candidate_name");

            migrationBuilder.RenameColumn(
                name: "AttemptId",
                table: "AttemptData",
                newName: "attempt_id");
        }
    }
}
