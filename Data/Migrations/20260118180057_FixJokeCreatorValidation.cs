using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixJokeCreatorValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Joke",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Joke",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_CreatorId",
                table: "Joke",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
