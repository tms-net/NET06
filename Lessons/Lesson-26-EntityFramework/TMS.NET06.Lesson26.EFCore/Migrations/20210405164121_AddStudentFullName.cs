using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Lesson26.EFCore.Migrations
{
    public partial class AddStudentFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(
                @"
                    UPDATE Students
                    SET FullName = Name + ' ' + [last_name];
                ");

            migrationBuilder.Sql(
            @"
                EXEC ('CREATE PROCEDURE getFullName
                    @LastName nvarchar(50),
                    @FirstName nvarchar(50)
                AS
                    RETURN @LastName + @FirstName;')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Students");
        }
    }
}
