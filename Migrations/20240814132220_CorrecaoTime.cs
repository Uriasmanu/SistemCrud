using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemCrud.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "TimeTrackers");

            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "TimeTrackers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "TimeTrackers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "TimeTrackers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
