using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestCatProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "isItemOfTheWeek",
                table: "Producto");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Producto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4490afde-5587-46ba-b2d4-5672c9bc22b3", "AQAAAAIAAYagAAAAEMwNVeZHKWsCswDLasaXpBFpAp8FAto9g4I4CHEqipGUR/ApCjNy2NwC7IPvsa3JMw==", "68cbb7b7-64d0-4da1-98ac-8858c3ab71a9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b8befd4-76fd-4227-b888-ab8ce9cd9a03", "AQAAAAIAAYagAAAAEKgz2OR2xVe7MBvGb0SpTrO99DWPAF6Vlgqb9kyAfsUDkT5Vh2dNVwkhcraOYsR47Q==", "1d4f5513-5c93-404a-9b5f-43f890c80b40" });
        }
    }
}
