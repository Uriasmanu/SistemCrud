using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemCrud.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Collaborators_UserId",
                table: "TimeTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Users_UserId1",
                table: "TimeTrackers");

            migrationBuilder.DropIndex(
                name: "IX_TimeTrackers_UserId",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "UserId",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Collaborators_CollaboratorId",
                table: "TimeTrackers");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "TimeTrackers",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTrackers_CollaboratorId",
                table: "TimeTrackers",
                newName: "IX_TimeTrackers_UserId1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeTrackers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeTrackers_UserId",
                table: "TimeTrackers",
                column: "UserId");

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
    }
}
