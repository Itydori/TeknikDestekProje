using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeNullDegerDefaultAtama2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Teslimat yapılmadı",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Sipariş verilmedi",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Nakit",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "KapatmaSaati",
                table: "IsEmriTeslim",
                type: "time",
                nullable: true,
                defaultValue: new TimeSpan(610506704251),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "KapatmaGunu",
                table: "IsEmriTeslim",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 4, 8, 16, 57, 30, 670, DateTimeKind.Local).AddTicks(3734),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Kapali",
                table: "IsEmriTeslim",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GelisTarih",
                table: "IsEmriTeslim",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 8, 16, 57, 30, 670, DateTimeKind.Local).AddTicks(6341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AlinanOdeme",
                table: "IsEmriTeslim",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Teslimat yapılmadı");

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Sipariş verilmedi");

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Nakit");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "KapatmaSaati",
                table: "IsEmriTeslim",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true,
                oldDefaultValue: new TimeSpan(610506704251));

            migrationBuilder.AlterColumn<DateTime>(
                name: "KapatmaGunu",
                table: "IsEmriTeslim",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 4, 8, 16, 57, 30, 670, DateTimeKind.Local).AddTicks(3734));

            migrationBuilder.AlterColumn<bool>(
                name: "Kapali",
                table: "IsEmriTeslim",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "GelisTarih",
                table: "IsEmriTeslim",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 8, 16, 57, 30, 670, DateTimeKind.Local).AddTicks(6341));

            migrationBuilder.AlterColumn<int>(
                name: "AlinanOdeme",
                table: "IsEmriTeslim",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);
        }
    }
}
