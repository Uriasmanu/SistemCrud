using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemCrud.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTimeTracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Collaborators_CollaboratorId",
                table: "TimeTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Users_UserId",
                table: "TimeTrackers");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "TimeTrackers",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTrackers_CollaboratorId",
                table: "TimeTrackers",
                newName: "IX_TimeTrackers_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Collaborators_UserId",
                table: "TimeTrackers",
                column: "UserId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Users_UserId1",
                table: "TimeTrackers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Collaborators_UserId",
                table: "TimeTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Users_UserId1",
                table: "TimeTrackers");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "TimeTrackers",
                newName: "CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTrackers_UserId1",
                table: "TimeTrackers",
                newName: "IX_TimeTrackers_CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Collaborators_CollaboratorId",
                table: "TimeTrackers",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Users_UserId",
                table: "TimeTrackers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
