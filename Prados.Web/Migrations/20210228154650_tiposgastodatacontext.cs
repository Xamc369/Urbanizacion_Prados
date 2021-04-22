using Microsoft.EntityFrameworkCore.Migrations;

namespace Prados.Web.Migrations
{
    public partial class tiposgastodatacontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egresostbls_TiposGastotbl_TiposGId",
                table: "Egresostbls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposGastotbl",
                table: "TiposGastotbl");

            migrationBuilder.RenameTable(
                name: "TiposGastotbl",
                newName: "TiposGastotbls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposGastotbls",
                table: "TiposGastotbls",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TiposGastotbls_Tip_Descripcion",
                table: "TiposGastotbls",
                column: "Tip_Descripcion",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Egresostbls_TiposGastotbls_TiposGId",
                table: "Egresostbls",
                column: "TiposGId",
                principalTable: "TiposGastotbls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egresostbls_TiposGastotbls_TiposGId",
                table: "Egresostbls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposGastotbls",
                table: "TiposGastotbls");

            migrationBuilder.DropIndex(
                name: "IX_TiposGastotbls_Tip_Descripcion",
                table: "TiposGastotbls");

            migrationBuilder.RenameTable(
                name: "TiposGastotbls",
                newName: "TiposGastotbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposGastotbl",
                table: "TiposGastotbl",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Egresostbls_TiposGastotbl_TiposGId",
                table: "Egresostbls",
                column: "TiposGId",
                principalTable: "TiposGastotbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
