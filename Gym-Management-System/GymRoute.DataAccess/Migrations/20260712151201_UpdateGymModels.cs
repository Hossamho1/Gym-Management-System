using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymRoute.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGymModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Phone",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Users",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "phone");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_phone",
                table: "Users",
                columns: new[] { "Email", "phone" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users",
                sql: "LEN(phone) = 11 AND (phone LIKE '010%' OR phone LIKE '011%' OR phone LIKE '012%' OR phone LIKE '015%')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_phone",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Users",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Phone",
                table: "Users",
                columns: new[] { "Email", "Phone" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users",
                sql: "LEN(Phone) = 11 AND (Phone LIKE '010%' OR Phone LIKE '011%' OR Phone LIKE '012%' OR Phone LIKE '015%')");
        }
    }
}