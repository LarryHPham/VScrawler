using Microsoft.EntityFrameworkCore.Migrations;

namespace disavow_1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Parsed",
                table: "Disavow",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parsed",
                table: "Disavow");
        }
    }
}
