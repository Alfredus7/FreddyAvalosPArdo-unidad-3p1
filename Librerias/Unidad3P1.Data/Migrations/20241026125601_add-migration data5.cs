using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationdata5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39901732-ce7d-49ef-9387-9a4d564be46c", "AQAAAAIAAYagAAAAEHCV4cyowlX4px2Fqrhdiped6jkZY8y7Ucc7QJCVDxSLYUrmRrlw1afDbt201cEaRQ==", "f25d0748-cc61-4dd7-9c64-54a8bbbbcd16" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4725d6-6dc4-4d3f-ab81-dda36159300e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "332bc0ba-141b-404d-9ef2-2272457b3143", "AQAAAAIAAYagAAAAEMvJ2pgyRlf0KS6VeBzMuGTlPfBwPMZ5pDShqrHnY89lrJ8mmIOg1F72UggvMtHj4w==", "c198f43d-a34c-4373-9932-a85050ff23b5" });
        }
    }
}
