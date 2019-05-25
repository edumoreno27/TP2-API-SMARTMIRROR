using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class _35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "NewsInformationNoUserAction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "NewsInformationAction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "EmailInformations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "NewsInformationNoUserAction");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "NewsInformationAction");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "EmailInformations");
        }
    }
}
