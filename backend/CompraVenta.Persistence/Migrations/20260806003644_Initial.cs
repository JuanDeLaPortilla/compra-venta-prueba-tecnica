using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompraVenta.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_usuario);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id_usuario", "Contrasena", "Nombre_usuario" },
                values: new object[] { 1, "42ffdc354f77785d52c8bea0fdddb1962f625183da9174c3757e412b06d2932f", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Nombre_usuario",
                table: "Usuarios",
                column: "Nombre_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
