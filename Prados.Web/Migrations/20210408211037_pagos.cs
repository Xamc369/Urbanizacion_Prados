using Microsoft.EntityFrameworkCore.Migrations;

namespace Prados.Web.Migrations
{
    public partial class pagos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Borr_Id",
                table: "Pagostbls",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pagostbls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Borr_Id",
                table: "PagosDeletetbls",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PagosDeletetbls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borr_Id",
                table: "Pagostbls");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pagostbls");

            migrationBuilder.DropColumn(
                name: "Borr_Id",
                table: "PagosDeletetbls");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PagosDeletetbls");
        }
    }
}
