using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Lesson26.EFCore.Migrations
{
    public partial class AddStudentShortBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortBio",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortBio",
                table: "Students");
        }
    }
}
