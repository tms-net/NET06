using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Lesson26.EFCore.Migrations
{
    public partial class AddHomeworkReviewer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewerStudentId",
                table: "homeworks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_homeworks_ReviewerStudentId",
                table: "homeworks",
                column: "ReviewerStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_homeworks_Students_ReviewerStudentId",
                table: "homeworks",
                column: "ReviewerStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_homeworks_Students_ReviewerStudentId",
                table: "homeworks");

            migrationBuilder.DropIndex(
                name: "IX_homeworks_ReviewerStudentId",
                table: "homeworks");

            migrationBuilder.DropColumn(
                name: "ReviewerStudentId",
                table: "homeworks");
        }
    }
}
