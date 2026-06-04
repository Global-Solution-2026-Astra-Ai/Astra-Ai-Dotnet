using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astra_Ai_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AST_CLIENTE_PREMIUM",
                columns: table => new
                {
                    id_cliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    razao_social = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    cnpj = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    demanda_contratada_gwh = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    status_cadastro = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_CLIENTE_PREMIUM_PK", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "AST_LEILAO_BIDDING",
                columns: table => new
                {
                    id_leilao = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_satelite = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_rcdenna_origem = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    data_hora_inicio = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    data_hora_fim = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    gwh_disponivel = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    preco_min_por_gwh = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    status_leilao = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_LEILAO_BIDDING_PK", x => x.id_leilao);
                });

            migrationBuilder.CreateTable(
                name: "AST_LOG_TRANSACAO",
                columns: table => new
                {
                    id_transacao = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_leilao = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_cliente_vencedor = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    valor_arrematado = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    taxa_oneracao_astra = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    data_faturamento = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_LOG_TRANSACAO_PK", x => x.id_transacao);
                    table.ForeignKey(
                        name: "FK_AST_LOG_TRANSACAO_AST_CLIENTE_PREMIUM_id_cliente_vencedor",
                        column: x => x.id_cliente_vencedor,
                        principalTable: "AST_CLIENTE_PREMIUM",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AST_LOG_TRANSACAO_AST_LEILAO_BIDDING_id_leilao",
                        column: x => x.id_leilao,
                        principalTable: "AST_LEILAO_BIDDING",
                        principalColumn: "id_leilao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UK_CLIENTE_CNPJ",
                table: "AST_CLIENTE_PREMIUM",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AST_LOG_TRANSACAO_id_cliente_vencedor",
                table: "AST_LOG_TRANSACAO",
                column: "id_cliente_vencedor");

            migrationBuilder.CreateIndex(
                name: "IX_AST_LOG_TRANSACAO_id_leilao",
                table: "AST_LOG_TRANSACAO",
                column: "id_leilao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AST_LOG_TRANSACAO");

            migrationBuilder.DropTable(
                name: "AST_CLIENTE_PREMIUM");

            migrationBuilder.DropTable(
                name: "AST_LEILAO_BIDDING");
        }
    }
}
