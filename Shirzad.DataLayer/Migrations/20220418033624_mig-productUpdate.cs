using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shirzad.DataLayer.Migrations
{
    public partial class migproductUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductGalleries");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductGalleries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
