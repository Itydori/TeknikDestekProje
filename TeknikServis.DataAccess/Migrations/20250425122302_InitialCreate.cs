using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslimler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "Teslimat yapılmadı");

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Sipariş verilmedi");

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslimler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "Nakit");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "KapatmaSaati",
                table: "IsEmriTeslimler",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true,
                oldDefaultValueSql: "CAST(GETDATE() AS time)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "KapatmaGunu",
                table: "IsEmriTeslimler",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "AlinanOdeme",
                table: "IsEmriTeslimler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslimler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "Teslimat yapılmadı",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Sipariş verilmedi",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslimler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Nakit",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "KapatmaSaati",
                table: "IsEmriTeslimler",
                type: "time",
                nullable: true,
                defaultValueSql: "CAST(GETDATE() AS time)",
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "KapatmaGunu",
                table: "IsEmriTeslimler",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AlinanOdeme",
                table: "IsEmriTeslimler",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
