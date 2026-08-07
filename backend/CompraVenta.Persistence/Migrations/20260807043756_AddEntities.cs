using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompraVenta.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompraCab",
                columns: table => new
                {
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCab", x => x.Id_CompraCab);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoCab",
                columns: table => new
                {
                    Id_MovimientoCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    Id_DocumentoOrigen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoCab", x => x.Id_MovimientoCab);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_producto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroLote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fec_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_producto);
                });

            migrationBuilder.CreateTable(
                name: "Ventacab",
                columns: table => new
                {
                    Id_VentaCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventacab", x => x.Id_VentaCab);
                });

            migrationBuilder.CreateTable(
                name: "CompraDet",
                columns: table => new
                {
                    Id_CompraDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDet", x => x.Id_CompraDet);
                    table.ForeignKey(
                        name: "FK_CompraDet_CompraCab_Id_CompraCab",
                        column: x => x.Id_CompraCab,
                        principalTable: "CompraCab",
                        principalColumn: "Id_CompraCab",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraDet_Productos_Id_producto",
                        column: x => x.Id_producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientodet",
                columns: table => new
                {
                    Id_MovimientoDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_movimientocab = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientodet", x => x.Id_MovimientoDet);
                    table.ForeignKey(
                        name: "FK_Movimientodet_MovimientoCab_Id_movimientocab",
                        column: x => x.Id_movimientocab,
                        principalTable: "MovimientoCab",
                        principalColumn: "Id_MovimientoCab",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientodet_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaDet",
                columns: table => new
                {
                    Id_VentaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_VentaCab = table.Column<int>(type: "int", nullable: false),
                    Id_producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDet", x => x.Id_VentaDet);
                    table.ForeignKey(
                        name: "FK_VentaDet_Productos_Id_producto",
                        column: x => x.Id_producto,
                        principalTable: "Productos",
                        principalColumn: "Id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaDet_Ventacab_Id_VentaCab",
                        column: x => x.Id_VentaCab,
                        principalTable: "Ventacab",
                        principalColumn: "Id_VentaCab",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraDet_Id_CompraCab",
                table: "CompraDet",
                column: "Id_CompraCab");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDet_Id_producto",
                table: "CompraDet",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientodet_Id_movimientocab",
                table: "Movimientodet",
                column: "Id_movimientocab");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientodet_Id_Producto",
                table: "Movimientodet",
                column: "Id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDet_Id_producto",
                table: "VentaDet",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDet_Id_VentaCab",
                table: "VentaDet",
                column: "Id_VentaCab");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDet");

            migrationBuilder.DropTable(
                name: "Movimientodet");

            migrationBuilder.DropTable(
                name: "VentaDet");

            migrationBuilder.DropTable(
                name: "CompraCab");

            migrationBuilder.DropTable(
                name: "MovimientoCab");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventacab");
        }
    }
}
