using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editUnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Unit",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentId",
                table: "Unit",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit",
                column: "ParentId",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ParentId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Unit");
        }
    }
}
