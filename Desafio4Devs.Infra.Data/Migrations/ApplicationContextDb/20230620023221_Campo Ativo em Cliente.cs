using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio4Devs.Infra.Data.Migrations.ApplicationContextDb
{
    /// <inheritdoc />
    public partial class CampoAtivoemCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Cliente",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Cliente");
        }
    }
}
