﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ovning14SkalProj.Migrations
{
    /// <inheritdoc />
    public partial class Model2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GymClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GymClasses");
        }
    }
}