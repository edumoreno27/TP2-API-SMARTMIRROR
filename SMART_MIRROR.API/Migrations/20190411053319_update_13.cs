using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BooleanTables",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BooleanTables_UserId",
                table: "BooleanTables",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooleanTables_AspNetUsers_UserId",
                table: "BooleanTables",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooleanTables_AspNetUsers_UserId",
                table: "BooleanTables");

            migrationBuilder.DropIndex(
                name: "IX_BooleanTables_UserId",
                table: "BooleanTables");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BooleanTables");
        }
    }
}
