using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Gadgets");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Gadgets");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "UserGadgets",
                newName: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserGadgets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserGadgets");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "UserGadgets",
                newName: "Position");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Gadgets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Gadgets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
