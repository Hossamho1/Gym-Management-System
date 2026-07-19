using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymRoute.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTheRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_phone",
                table: "Users",
                newName: "IX_Users_Email_Phone");

            migrationBuilder.AlterColumn<string>(
                name: "Street ",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City ",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Building number ",
                table: "Users",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MemberId1",
                table: "Bookings",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_MemberId1",
                table: "Bookings",
                column: "MemberId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_MemberId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_MemberId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email_Phone",
                table: "Users",
                newName: "IX_Users_Email_phone");

            migrationBuilder.AlterColumn<string>(
                name: "Street ",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City ",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Building number ",
                table: "Users",
                type: "int",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);
        }
    }
}
