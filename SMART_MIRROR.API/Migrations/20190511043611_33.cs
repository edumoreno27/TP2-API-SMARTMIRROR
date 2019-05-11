using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class _33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StartNews",
                table: "BooleanTables",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "NewsInformationAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tittle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsInformationAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsInformationAction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsInformationNoUserAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tittle = table.Column<string>(nullable: true),
                    MirrorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsInformationNoUserAction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsInformationAction_UserId",
                table: "NewsInformationAction",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsInformationAction");

            migrationBuilder.DropTable(
                name: "NewsInformationNoUserAction");

            migrationBuilder.DropColumn(
                name: "StartNews",
                table: "BooleanTables");
        }
    }
}
