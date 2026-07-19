using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymRoute.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Users",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_Phone",
                table: "Users",
                newName: "IX_Users_Email_phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Users",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_phone",
                table: "Users",
                newName: "IX_Users_Email_Phone");
        }
    }
}
