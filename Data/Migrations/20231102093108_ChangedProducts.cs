using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioWiseV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emmission",
                table: "Product");

            migrationBuilder.AddColumn<float>(
                name: "CO2Emmision",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EcologicalFootprint",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WaterFootprint",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CO2Emmision",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "EcologicalFootprint",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WaterFootprint",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "Emmission",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
