using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "UserGadgets");

            migrationBuilder.DropColumn(
                name: "MirrorCode",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MirrorId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MirrorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "UserGadgets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MirrorCode",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
