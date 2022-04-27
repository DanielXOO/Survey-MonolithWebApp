using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Surveys.Repositories.Migrations
{
    public partial class AddAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesRoleId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_UsersUserId",
                table: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "Survey",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "RoleUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "RolesRoleId",
                table: "RoleUser",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersUserId",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Question",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "SurveyAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswer_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyAnswer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_SurveyAnswer_SurveyAnswerId",
                        column: x => x.SurveyAnswerId,
                        principalTable: "SurveyAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerOption_QuestionAnswer_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_SurveyAnswerId",
                table: "QuestionAnswer",
                column: "SurveyAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerOption_QuestionAnswerId",
                table: "QuestionAnswerOption",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_SurveyId",
                table: "SurveyAnswer",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_UserId",
                table: "SurveyAnswer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_UsersId",
                table: "RoleUser");

            migrationBuilder.DropTable(
                name: "QuestionAnswerOption");

            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "SurveyAnswer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Survey",
                newName: "SurveyId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RoleUser",
                newName: "UsersUserId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RoleUser",
                newName: "RolesRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Question",
                newName: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesRoleId",
                table: "RoleUser",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_UsersUserId",
                table: "RoleUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
