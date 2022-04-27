using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shirzad.DataLayer.Migrations
{
    public partial class migbestsaller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BestSaller",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSaller",
                table: "Products");
        }
    }
}
