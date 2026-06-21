using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_EmployeeId",
                table: "Photo");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_EmployeeId",
                table: "Photo",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_EmployeeId",
                table: "Photo");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_EmployeeId",
                table: "Photo",
                column: "EmployeeId");
        }
    }
}
