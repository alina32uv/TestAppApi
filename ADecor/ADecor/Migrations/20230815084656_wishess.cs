using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADecor.Migrations
{
    /// <inheritdoc />
    public partial class wishess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "WishList",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "WishList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "WishList");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "WishList");
        }
    }
}
