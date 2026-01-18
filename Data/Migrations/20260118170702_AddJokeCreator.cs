using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddJokeCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Joke",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Joke_CreatorId",
                table: "Joke",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke");

            migrationBuilder.DropIndex(
                name: "IX_Joke_CreatorId",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Joke");
        }
    }
}
