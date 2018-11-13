using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class UpdatingGuestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Guests",
                newName: "AttendingId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_UserId",
                table: "Guests",
                newName: "IX_Guests_AttendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_AttendingId",
                table: "Guests",
                column: "AttendingId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_AttendingId",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "AttendingId",
                table: "Guests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_AttendingId",
                table: "Guests",
                newName: "IX_Guests_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_UserId",
                table: "Guests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
