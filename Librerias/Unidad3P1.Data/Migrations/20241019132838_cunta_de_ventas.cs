using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class cunta_de_ventas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoCompra_Producto_ProductoId",
                table: "CarritoCompra");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "CarritoCompra",
                newName: "ProductoEntityProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoCompra_ProductoId",
                table: "CarritoCompra",
                newName: "IX_CarritoCompra_ProductoEntityProductoId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "265e39fd-0ed7-4434-9e01-f669da0925de", "265e39fd-0ed7-4434-9e01-f669da0925de", "Ventas", "Ventas" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1cf5be7-1298-4c1e-8499-7ee8f61959ae", "AQAAAAIAAYagAAAAEGJ6iCCyOGACOr8MpIf6svry5bO4mq03IFeYfxba3rlFQXu9FvO0Njcle0JOywyQuA==", "23035c48-8b7c-4df2-a675-7712e5d69d39" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoCompra_Producto_ProductoEntityProductoId",
                table: "CarritoCompra",
                column: "ProductoEntityProductoId",
                principalTable: "Producto",
                principalColumn: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoCompra_Producto_ProductoEntityProductoId",
                table: "CarritoCompra");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "265e39fd-0ed7-4434-9e01-f669da0925de");

            migrationBuilder.RenameColumn(
                name: "ProductoEntityProductoId",
                table: "CarritoCompra",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoCompra_ProductoEntityProductoId",
                table: "CarritoCompra",
                newName: "IX_CarritoCompra_ProductoId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4490afde-5587-46ba-b2d4-5672c9bc22b3", "AQAAAAIAAYagAAAAEMwNVeZHKWsCswDLasaXpBFpAp8FAto9g4I4CHEqipGUR/ApCjNy2NwC7IPvsa3JMw==", "68cbb7b7-64d0-4da1-98ac-8858c3ab71a9" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoCompra_Producto_ProductoId",
                table: "CarritoCompra",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "ProductoId");
        }
    }
}
