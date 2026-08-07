using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompraVenta.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataseedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id_producto", "NroLote", "Costo", "Nombre_producto", "Fec_registro", "PrecioVenta" },
                values: new object[,]
                {
                    { 1, "LOT-LEN-001", 1800.00m, "Laptop Lenovo IdeaPad 3", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2430.00m },
                    { 2, "LOT-LOG-001", 35.00m, "Mouse Logitech M185", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 47.25m },
                    { 3, "LOT-LOG-002", 45.00m, "Teclado Logitech K120", new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.75m },
                    { 4, "LOT-LG-001", 550.00m, "Monitor LG 24 pulgadas", new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 742.50m },
                    { 5, "LOT-SONY-001", 180.00m, "Audífonos Sony WH-CH520", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 243.00m },
                    { 6, "LOT-LOG-003", 280.00m, "Webcam Logitech C920", new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 378.00m },
                    { 7, "LOT-KNG-001", 250.00m, "Disco SSD Kingston 1TB", new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 337.50m },
                    { 8, "LOT-KNG-002", 160.00m, "Memoria RAM Kingston 16GB", new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 216.00m },
                    { 9, "LOT-EPS-001", 650.00m, "Impresora Epson EcoTank L3250", new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 877.50m },
                    { 10, "LOT-TPL-001", 120.00m, "Router TP-Link Archer C6", new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 162.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id_producto",
                keyValue: 10);
        }
    }
}
