using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ovning14SkalProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    GymClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.GymClassId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGymClass",
                columns: table => new
                {
                    GymClassId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserGymClassId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGymClass", x => new { x.ApplicationUserId, x.GymClassId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGymClass_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUserGymClass_Course_GymClassId",
                        column: x => x.GymClassId,
                        principalTable: "Course",
                        principalColumn: "GymClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGymClassApplicationUserGymClass",
                columns: table => new
                {
                    AttendedClassesApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    AttendedClassesGymClassId = table.Column<int>(type: "int", nullable: false),
                    AttendingMembersApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    AttendingMembersGymClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGymClassApplicationUserGymClass", x => new { x.AttendedClassesApplicationUserId, x.AttendedClassesGymClassId, x.AttendingMembersApplicationUserId, x.AttendingMembersGymClassId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGymClassApplicationUserGymClass_ApplicationUserGymClass_AttendedClassesApplicationUserId_AttendedClassesGymCl~",
                        columns: x => new { x.AttendedClassesApplicationUserId, x.AttendedClassesGymClassId },
                        principalTable: "ApplicationUserGymClass",
                        principalColumns: new[] { "ApplicationUserId", "GymClassId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGymClassApplicationUserGymClass_ApplicationUserGymClass_AttendingMembersApplicationUserId_AttendingMembersGym~",
                        columns: x => new { x.AttendingMembersApplicationUserId, x.AttendingMembersGymClassId },
                        principalTable: "ApplicationUserGymClass",
                        principalColumns: new[] { "ApplicationUserId", "GymClassId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGymClass_ApplicationUserId1",
                table: "ApplicationUserGymClass",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGymClass_GymClassId",
                table: "ApplicationUserGymClass",
                column: "GymClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGymClassApplicationUserGymClass_AttendingMembersApplicationUserId_AttendingMembersGymClassId",
                table: "ApplicationUserGymClassApplicationUserGymClass",
                columns: new[] { "AttendingMembersApplicationUserId", "AttendingMembersGymClassId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGymClassApplicationUserGymClass");

            migrationBuilder.DropTable(
                name: "ApplicationUserGymClass");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");
        }
    }
}
