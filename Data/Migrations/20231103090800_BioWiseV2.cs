using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioWiseV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class BioWiseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QualityMark_link",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualityMark_link",
                table: "Product");
        }
    }
}
