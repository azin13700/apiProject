using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "ParentUnitId",
                table: "Unit",
                newName: "IsActive");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "Unit",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "UpdatedAt",
                table: "Unit",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "Role",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Role",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "UpdatedAt",
                table: "Role",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Unit",
                newName: "ParentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
