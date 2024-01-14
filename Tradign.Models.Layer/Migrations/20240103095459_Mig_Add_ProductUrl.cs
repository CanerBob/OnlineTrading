using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Models.Layer.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Add_ProductUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Products");
        }
    }
}
