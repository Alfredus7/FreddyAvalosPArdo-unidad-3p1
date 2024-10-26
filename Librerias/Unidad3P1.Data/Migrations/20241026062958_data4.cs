using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class data4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "OrdenDetalle");

            migrationBuilder.DropColumn(
                name: "Direccion2",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "DireccionCorreo",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "OrdenTotal",
                table: "Orden");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Orden");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "OrdenDetalle",
                newName: "PrecioTotal");

            migrationBuilder.RenameColumn(
                name: "Direccion1",
                table: "Orden",
                newName: "Direccion");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Orden",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "332bc0ba-141b-404d-9ef2-2272457b3143", "AQAAAAIAAYagAAAAEMvJ2pgyRlf0KS6VeBzMuGTlPfBwPMZ5pDShqrHnY89lrJ8mmIOg1F72UggvMtHj4w==", "c198f43d-a34c-4373-9932-a85050ff23b5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Orden");

            migrationBuilder.RenameColumn(
                name: "PrecioTotal",
                table: "OrdenDetalle",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Orden",
                newName: "Direccion1");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "OrdenDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Direccion2",
                table: "Orden",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DireccionCorreo",
                table: "Orden",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OrdenTotal",
                table: "Orden",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Orden",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29b6fac0-f81f-45fa-9730-93bdf7fe35d7", "AQAAAAIAAYagAAAAEFIZWshfCT53l2tMX3PzgEMJIwYGqsWmkUhIsHzu063WcvHBrjFV0ddZPu8R5koqzA==", "cc1ac656-bd9e-49f4-9b0e-9dcdfce1e3ed" });
        }
    }
}
