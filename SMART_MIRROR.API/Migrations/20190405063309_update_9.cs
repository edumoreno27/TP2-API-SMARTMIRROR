using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaticNumber",
                table: "UserGadgets");

            migrationBuilder.DropColumn(
                name: "UserActive",
                table: "UserGadgets");

            migrationBuilder.AddColumn<int>(
                name: "StaticNumber",
                table: "Gadgets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaticNumber",
                table: "Gadgets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "StaticNumber",
                table: "UserGadgets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UserActive",
                table: "UserGadgets",
                nullable: false,
                defaultValue: false);
        }
    }
}
