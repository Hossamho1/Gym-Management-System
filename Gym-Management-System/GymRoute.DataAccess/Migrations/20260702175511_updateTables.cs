using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymRoute.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_phone",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_phone",
                table: "Users",
                newName: "IX_Users_Email_Phone");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Phone",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GymUser_Phone",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_Phone",
                table: "Users",
                newName: "IX_Users_Email_phone");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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
    }
}
