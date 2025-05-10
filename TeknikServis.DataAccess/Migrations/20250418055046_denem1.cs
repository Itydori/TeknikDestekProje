using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class denem1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_IsEmriTeslimler_IsEmriTeslimId",
                table: "Islemler");

            migrationBuilder.DropColumn(
                name: "IsEmriId",
                table: "Islemler");

            migrationBuilder.AlterColumn<int>(
                name: "IsEmriTeslimId",
                table: "Islemler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslimler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "Teslimat yapılmadı",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Teslimat yapılmadı");

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Sipariş verilmedi",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Sipariş verilmedi");

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslimler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Nakit",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Nakit");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "IsEmriTeslimler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Marka",
                table: "IsEmriTeslimler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FisNo",
                table: "IsEmriTeslimler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ArizaDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_IsEmriTeslimler_IsEmriTeslimId",
                table: "Islemler",
                column: "IsEmriTeslimId",
                principalTable: "IsEmriTeslimler",
                principalColumn: "IsEmriTeslimId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_IsEmriTeslimler_IsEmriTeslimId",
                table: "Islemler");

            migrationBuilder.AlterColumn<int>(
                name: "IsEmriTeslimId",
                table: "Islemler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IsEmriId",
                table: "Islemler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Teslimat yapılmadı",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "Teslimat yapılmadı");

            migrationBuilder.AlterColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Sipariş verilmedi",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Sipariş verilmedi");

            migrationBuilder.AlterColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Nakit",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "Nakit");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Marka",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FisNo",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ArizaDurumu",
                table: "IsEmriTeslimler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_IsEmriTeslimler_IsEmriTeslimId",
                table: "Islemler",
                column: "IsEmriTeslimId",
                principalTable: "IsEmriTeslimler",
                principalColumn: "IsEmriTeslimId");
        }
    }
}
