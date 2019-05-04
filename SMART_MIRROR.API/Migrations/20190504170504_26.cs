using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MirrorId",
                table: "MusicActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MirrorId",
                table: "MusicActions");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "AspNetUsers");
        }
    }
}
