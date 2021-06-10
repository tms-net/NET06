using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Parfume.Manager.MVC.Migrations
{
    public partial class GhangeImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
