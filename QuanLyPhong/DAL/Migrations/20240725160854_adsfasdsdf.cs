using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class adsfasdsdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "KindOfRoom",
                newName: "PricePerDay");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceByHour",
                table: "KindOfRoom",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceByHour",
                table: "KindOfRoom");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "KindOfRoom",
                newName: "Price");
        }
    }
}
