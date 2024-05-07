using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class changedDefaultValue1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultValue",
                table: "RefCurrency",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultValue",
                table: "RefCurrency",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true,
                oldDefaultValue: 1m);
        }
    }
}
