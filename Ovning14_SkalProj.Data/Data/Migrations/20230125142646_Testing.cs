using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ovning14SkalProj.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_GymClasses_GymClassId",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGymClass",
                table: "ApplicationUserGymClass");

            migrationBuilder.RenameTable(
                name: "ApplicationUserGymClass",
                newName: "AppUsersGymClasses");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserGymClass_GymClassId",
                table: "AppUsersGymClasses",
                newName: "IX_AppUsersGymClasses_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsersGymClasses",
                table: "AppUsersGymClasses",
                columns: new[] { "ApplicationUserId", "GymClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsersGymClasses_AspNetUsers_ApplicationUserId",
                table: "AppUsersGymClasses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsersGymClasses_GymClasses_GymClassId",
                table: "AppUsersGymClasses",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "GymClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsersGymClasses_AspNetUsers_ApplicationUserId",
                table: "AppUsersGymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsersGymClasses_GymClasses_GymClassId",
                table: "AppUsersGymClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsersGymClasses",
                table: "AppUsersGymClasses");

            migrationBuilder.RenameTable(
                name: "AppUsersGymClasses",
                newName: "ApplicationUserGymClass");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsersGymClasses_GymClassId",
                table: "ApplicationUserGymClass",
                newName: "IX_ApplicationUserGymClass_GymClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGymClass",
                table: "ApplicationUserGymClass",
                columns: new[] { "ApplicationUserId", "GymClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserGymClass",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_GymClasses_GymClassId",
                table: "ApplicationUserGymClass",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "GymClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
