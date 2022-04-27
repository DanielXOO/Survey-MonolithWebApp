using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Surveys.Repositories.Migrations
{
    public partial class AddScaleAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ScaleAnswer",
                table: "QuestionAnswer",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScaleAnswer",
                table: "QuestionAnswer");
        }
    }
}
