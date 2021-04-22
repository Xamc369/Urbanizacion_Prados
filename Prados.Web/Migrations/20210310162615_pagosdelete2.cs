using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prados.Web.Migrations
{
    public partial class pagosdelete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PagosDeletetblId",
                table: "EstadosFinancierostbls",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PagosDeletetbls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PAG_FECHAPAGADO = table.Column<DateTime>(nullable: false),
                    PAG_FECHACREACION = table.Column<DateTime>(nullable: false),
                    PAG_ESTADO = table.Column<string>(nullable: false),
                    PAG_PAGADO = table.Column<string>(nullable: false),
                    PropietarioId = table.Column<int>(nullable: true),
                    AnioId = table.Column<int>(nullable: true),
                    MesId = table.Column<int>(nullable: true),
                    ValId = table.Column<int>(nullable: true),
                    TiposId = table.Column<int>(nullable: true),
                    PuntodePagoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosDeletetbls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_Aniostbls_AnioId",
                        column: x => x.AnioId,
                        principalTable: "Aniostbls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_Mesestbls_MesId",
                        column: x => x.MesId,
                        principalTable: "Mesestbls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_Propietariostbls_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Propietariostbls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_PuntosPagotbls_PuntodePagoId",
                        column: x => x.PuntodePagoId,
                        principalTable: "PuntosPagotbls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_TiposPagotbl_TiposId",
                        column: x => x.TiposId,
                        principalTable: "TiposPagotbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosDeletetbls_Valorestbls_ValId",
                        column: x => x.ValId,
                        principalTable: "Valorestbls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadosFinancierostbls_PagosDeletetblId",
                table: "EstadosFinancierostbls",
                column: "PagosDeletetblId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_AnioId",
                table: "PagosDeletetbls",
                column: "AnioId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_MesId",
                table: "PagosDeletetbls",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_PropietarioId",
                table: "PagosDeletetbls",
                column: "PropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_PuntodePagoId",
                table: "PagosDeletetbls",
                column: "PuntodePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_TiposId",
                table: "PagosDeletetbls",
                column: "TiposId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosDeletetbls_ValId",
                table: "PagosDeletetbls",
                column: "ValId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosFinancierostbls_PagosDeletetbls_PagosDeletetblId",
                table: "EstadosFinancierostbls",
                column: "PagosDeletetblId",
                principalTable: "PagosDeletetbls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosFinancierostbls_PagosDeletetbls_PagosDeletetblId",
                table: "EstadosFinancierostbls");

            migrationBuilder.DropTable(
                name: "PagosDeletetbls");

            migrationBuilder.DropIndex(
                name: "IX_EstadosFinancierostbls_PagosDeletetblId",
                table: "EstadosFinancierostbls");

            migrationBuilder.DropColumn(
                name: "PagosDeletetblId",
                table: "EstadosFinancierostbls");
        }
    }
}
