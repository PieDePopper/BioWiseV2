using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioWiseV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class transport_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transport_type",
                table: "TransportUsage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transport_type",
                table: "TransportUsage");
        }
    }
}
