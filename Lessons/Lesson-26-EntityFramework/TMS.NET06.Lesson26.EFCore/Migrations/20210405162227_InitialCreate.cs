using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Lesson26.EFCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "homeworks",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NewStudentId = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homeworks", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_homeworks_Students_NewStudentId",
                        column: x => x.NewStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAvatar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAvatar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAvatar_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "int", nullable: false),
                    SolutionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_Diplomas_homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "homeworks",
                        principalColumn: "HomeworkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_homeworks_Name",
                table: "homeworks",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_homeworks_NewStudentId",
                table: "homeworks",
                column: "NewStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAvatar_StudentId",
                table: "StudentAvatar",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Name_last_name",
                table: "Students",
                columns: new[] { "Name", "last_name" },
                unique: true,
                filter: "[last_name] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "BirthDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "StudentAvatar");

            migrationBuilder.DropTable(
                name: "homeworks");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
