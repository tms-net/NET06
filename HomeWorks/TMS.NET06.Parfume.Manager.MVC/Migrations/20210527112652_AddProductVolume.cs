using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Parfume.Manager.MVC.Migrations
{
    public partial class AddProductVolume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "volume",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "volume",
                table: "Products");
        }
    }
}
