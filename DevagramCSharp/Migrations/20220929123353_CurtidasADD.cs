using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevagramCSharp.Migrations
{
    public partial class CurtidasADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curtidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPublicacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curtidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curtidas_Publicacaos_IdPublicacao",
                        column: x => x.IdPublicacao,
                        principalTable: "Publicacaos",
                        principalColumn: "Id");

                    table.ForeignKey(
                        name: "FK_Curtidas_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                       
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdPublicacao",
                table: "Curtidas",
                column: "IdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdUsuario",
                table: "Curtidas",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curtidas");
        }
    }
}
