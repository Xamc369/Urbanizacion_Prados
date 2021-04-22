using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prados.Web.Migrations
{
    public partial class tipogasto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiposGId",
                table: "Egresostbls",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposGastotbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tip_Descripcion = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposGastotbl", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Egresostbls_TiposGId",
                table: "Egresostbls",
                column: "TiposGId");

            migrationBuilder.AddForeignKey(
                name: "FK_Egresostbls_TiposGastotbl_TiposGId",
                table: "Egresostbls",
                column: "TiposGId",
                principalTable: "TiposGastotbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egresostbls_TiposGastotbl_TiposGId",
                table: "Egresostbls");

            migrationBuilder.DropTable(
                name: "TiposGastotbl");

            migrationBuilder.DropIndex(
                name: "IX_Egresostbls_TiposGId",
                table: "Egresostbls");

            migrationBuilder.DropColumn(
                name: "TiposGId",
                table: "Egresostbls");
        }
    }
}
