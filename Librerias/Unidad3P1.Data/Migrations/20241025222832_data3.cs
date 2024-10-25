using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class data3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CarritoCompra",
                columns: table => new
                {
                    CarritoCompraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoEntityProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompra", x => x.CarritoCompraId);
                    table.ForeignKey(
                        name: "FK_CarritoCompra_Producto_ProductoEntityProductoId",
                        column: x => x.ProductoEntityProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "CarritoCompraItem",
                columns: table => new
                {
                    CarritoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CarritoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompraItem", x => x.CarritoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarritoCompraItem_CarritoCompra_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "CarritoCompra",
                        principalColumn: "CarritoCompraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoCompraItem_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5d4725d6-6dc4-4d3f-ab81-dda36159300e", 0, "29b6fac0-f81f-45fa-9730-93bdf7fe35d7", "admin@email.com", true, false, null, null, "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEFIZWshfCT53l2tMX3PzgEMJIwYGqsWmkUhIsHzu063WcvHBrjFV0ddZPu8R5koqzA==", null, false, "cc1ac656-bd9e-49f4-9b0e-9dcdfce1e3ed", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "5d4725d6-6dc4-4d3f-ab81-dda36159300e" });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompra_ProductoEntityProductoId",
                table: "CarritoCompra",
                column: "ProductoEntityProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompraItem_CarritoId",
                table: "CarritoCompraItem",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompraItem_ProductoId",
                table: "CarritoCompraItem",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoCompraItem");

            migrationBuilder.DropTable(
                name: "CarritoCompra");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "5d4725d6-6dc4-4d3f-ab81-dda36159300e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "341743f0-asd2–42de-afbf-59kmkkmk72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isItemOfTheWeek",
                table: "Producto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
