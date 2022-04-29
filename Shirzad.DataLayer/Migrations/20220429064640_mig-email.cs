using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shirzad.DataLayer.Migrations
{
    public partial class migemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailPassword",
                table: "AspNetUsers");
        }
    }
}
