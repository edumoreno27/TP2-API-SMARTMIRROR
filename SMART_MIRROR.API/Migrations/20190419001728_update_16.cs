using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMART_MIRROR.API.Migrations
{
    public partial class update_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiaryInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateTime = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryInformations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sender = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    SenderAt = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailInformations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryInformations_UserId",
                table: "DiaryInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailInformations_UserId",
                table: "EmailInformations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryInformations");

            migrationBuilder.DropTable(
                name: "EmailInformations");
        }
    }
}
