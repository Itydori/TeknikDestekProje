using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ekleme6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "KapatmaTarihi",
                table: "IsEmriTeslim",
                type: "datetime2",
                maxLength: 20,
                nullable: false,
                computedColumnSql: "CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComputedColumnSql: "CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KapatmaTarihi",
                table: "IsEmriTeslim",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                computedColumnSql: "CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
                stored: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 20,
                oldComputedColumnSql: "CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
                oldStored: true);
        }
    }
}
