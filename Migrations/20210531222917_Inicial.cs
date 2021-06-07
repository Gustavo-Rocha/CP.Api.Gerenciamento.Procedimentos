using Microsoft.EntityFrameworkCore.Migrations;

namespace CP.Api.Gerenciamento.Procedimentos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Procedimentos",
                columns: table => new
                {
                    IDProcedimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProcedimento = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    valorProcedimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DuracaoProcedimento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.IDProcedimento);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Procedimentos");
        }
    }
}
