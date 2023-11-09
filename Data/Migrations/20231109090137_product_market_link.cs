using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioWiseV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class product_market_link : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Market_link",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Market_link",
                table: "Product");
        }
    }
}
