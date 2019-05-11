using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "News",
                table: "NewsInformationNoUserAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StartNews",
                table: "NewsInformationNoUserAction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "News",
                table: "BooleanTables",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "News",
                table: "NewsInformationNoUserAction");

            migrationBuilder.DropColumn(
                name: "StartNews",
                table: "NewsInformationNoUserAction");

            migrationBuilder.DropColumn(
                name: "News",
                table: "BooleanTables");
        }
    }
}
