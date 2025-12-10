using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EazyOnRent.Migrations
{
    /// <inheritdoc />
    public partial class oneCustomerType_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "ViewerCategory",
                table: "dbVieweds");

            migrationBuilder.RenameColumn(
                name: "RenterId",
                table: "RenterItems",
                newName: "ListerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListerId",
                table: "RenterItems",
                newName: "RenterId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ItemImages",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewerCategory",
                table: "dbVieweds",
                type: "int",
                nullable: true);
        }
    }
}
