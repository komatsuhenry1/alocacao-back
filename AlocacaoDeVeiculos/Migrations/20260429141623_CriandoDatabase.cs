using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlocacaoDeVeiculos.Migrations
{
    /// <inheritdoc />
    public partial class CriandoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAT_NOME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CAT_DESCRICAO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CAT_VALOR_DIARIA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CAT_ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIA", x => x.CAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CLIENTE",
                columns: table => new
                {
                    CLI_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLI_NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CLI_CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_SENHA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CLI_DATA_NASCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CLI_TELEFONE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_ENDERECO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENTE", x => x.CLI_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CARRO",
                columns: table => new
                {
                    CAR_PLACA = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CAR_MARCA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAR_MODELO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAR_ANO = table.Column<int>(type: "int", nullable: false),
                    CAR_COR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAT_ID = table.Column<int>(type: "int", nullable: false),
                    CAR_IMAGEM_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAR_DISPONIVEL = table.Column<bool>(type: "bit", nullable: false),
                    CAR_ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CARRO", x => x.CAR_PLACA);
                    table.ForeignKey(
                        name: "FK_TB_CARRO_TB_CATEGORIA_CAT_ID",
                        column: x => x.CAT_ID,
                        principalTable: "TB_CATEGORIA",
                        principalColumn: "CAT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_LOCACAO",
                columns: table => new
                {
                    LOC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLI_ID = table.Column<int>(type: "int", nullable: false),
                    CAR_PLACA = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LOC_DATA_RETIRADA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LOC_DATA_PREV_DEVOLUCAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LOC_VALOR_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LOC_STATUS = table.Column<int>(type: "int", nullable: false),
                    LOC_CRIADO_EM = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LOCACAO", x => x.LOC_ID);
                    table.ForeignKey(
                        name: "FK_TB_LOCACAO_TB_CARRO_CAR_PLACA",
                        column: x => x.CAR_PLACA,
                        principalTable: "TB_CARRO",
                        principalColumn: "CAR_PLACA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_LOCACAO_TB_CLIENTE_CLI_ID",
                        column: x => x.CLI_ID,
                        principalTable: "TB_CLIENTE",
                        principalColumn: "CLI_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CARRO_CAT_ID",
                table: "TB_CARRO",
                column: "CAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LOCACAO_CAR_PLACA",
                table: "TB_LOCACAO",
                column: "CAR_PLACA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LOCACAO_CLI_ID",
                table: "TB_LOCACAO",
                column: "CLI_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_LOCACAO");

            migrationBuilder.DropTable(
                name: "TB_CARRO");

            migrationBuilder.DropTable(
                name: "TB_CLIENTE");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIA");
        }
    }
}
