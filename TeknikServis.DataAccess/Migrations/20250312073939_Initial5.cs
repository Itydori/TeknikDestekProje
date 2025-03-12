using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kapali",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "TeslimId",
                table: "IsEmriTeslim");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Kapali",
                table: "IsEmriTeslim",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeslimId",
                table: "IsEmriTeslim",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
