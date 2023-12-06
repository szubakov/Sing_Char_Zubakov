using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sing_Char_Zubakov.Migrations
{
    public partial class addus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mess_UserId",
                table: "Mess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mess_Users_UserId",
                table: "Mess",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mess_Users_UserId",
                table: "Mess");

            migrationBuilder.DropIndex(
                name: "IX_Mess_UserId",
                table: "Mess");
        }
    }
}
