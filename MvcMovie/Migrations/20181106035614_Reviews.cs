using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class Reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Movie",
                newName: "Reviewer");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Movie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "Reviewer",
                table: "Movie",
                newName: "Review");
        }
    }
}
