using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class changedDefaultValue2 : Migration
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
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true,
                oldDefaultValue: 1m);
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
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldDefaultValue: 1m);
        }
    }
}
