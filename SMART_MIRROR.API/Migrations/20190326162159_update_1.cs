using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gadgets_AspNetUsers_UserId",
                table: "Gadgets");

            migrationBuilder.DropIndex(
                name: "IX_Gadgets_UserId",
                table: "Gadgets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Gadgets");

            migrationBuilder.CreateTable(
                name: "UserGadgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GadgetId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGadgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGadgets_Gadgets_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGadgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGadgets_GadgetId",
                table: "UserGadgets",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGadgets_UserId",
                table: "UserGadgets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGadgets");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Gadgets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_UserId",
                table: "Gadgets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gadgets_AspNetUsers_UserId",
                table: "Gadgets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
