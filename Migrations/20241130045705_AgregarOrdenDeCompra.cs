using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaComprasMVC.Migrations
{
    public partial class AgregarOrdenDeCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenesDeCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaOrden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    UnidadDeMedidaId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CostoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesDeCompra_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesDeCompra_UnidadesDeMedida_UnidadDeMedidaId",
                        column: x => x.UnidadDeMedidaId,
                        principalTable: "UnidadesDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCompra_ArticuloId",
                table: "OrdenesDeCompra",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCompra_UnidadDeMedidaId",
                table: "OrdenesDeCompra",
                column: "UnidadDeMedidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenesDeCompra");
        }
    }
}
