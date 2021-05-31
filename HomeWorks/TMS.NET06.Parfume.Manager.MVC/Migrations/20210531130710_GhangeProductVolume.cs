using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.NET06.Parfume.Manager.MVC.Migrations
{
    public partial class GhangeProductVolume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "volume",
                table: "Products",
                newName: "Volume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "Products",
                newName: "volume");
        }
    }
}
