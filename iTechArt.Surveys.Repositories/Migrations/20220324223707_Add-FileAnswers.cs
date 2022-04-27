using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Surveys.Repositories.Migrations
{
    public partial class AddFileAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileAnswerId",
                table: "QuestionAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAnswer_FileInfo_FileInfoId",
                        column: x => x.FileInfoId,
                        principalTable: "FileInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileAnswer_QuestionAnswer_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAnswer_FileInfoId",
                table: "FileAnswer",
                column: "FileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAnswer_QuestionAnswerId",
                table: "FileAnswer",
                column: "QuestionAnswerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAnswer");

            migrationBuilder.DropTable(
                name: "FileInfo");

            migrationBuilder.DropColumn(
                name: "FileAnswerId",
                table: "QuestionAnswer");
        }
    }
}
