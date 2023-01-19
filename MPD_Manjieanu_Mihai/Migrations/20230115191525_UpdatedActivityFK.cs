using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPDManjineanuMihai.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedActivityFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserID",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_UserID",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserID",
                table: "Activities",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserID",
                table: "Activities",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
