using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EazyOnRent.Migrations
{
    /// <inheritdoc />
    public partial class AvailabilityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailabilityType",
                table: "ListerItems",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityType",
                table: "ListerItems");
        }
    }
}
