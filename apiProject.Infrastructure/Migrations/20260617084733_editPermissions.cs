using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Permissions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Permissions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Permissions");
        }
    }
}
