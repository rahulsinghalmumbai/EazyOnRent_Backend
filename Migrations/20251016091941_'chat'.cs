using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EazyOnRent.Migrations
{
    /// <inheritdoc />
    public partial class chat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ReadAt",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChatMessages");
        }
    }
}
