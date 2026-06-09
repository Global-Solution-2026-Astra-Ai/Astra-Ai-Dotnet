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
            migrationBuilder.CreateSequence<int>(
                name: "SEQ_AST_CLIENTE");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_AST_LEILAO");

            migrationBuilder.CreateSequence<int>(
                name: "SEQ_AST_TRANSACAO");

            migrationBuilder.CreateTable(
                name: "AST_CLIENTE_PREMIUM",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_AST_CLIENTE.NEXTVAL"),
                    RAZAO_SOCIAL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    DEMANDA_CONTRATADA_GWH = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    STATUS_CADASTRO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_CLIENTE_PREMIUM_PK", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "AST_LEILAO_BIDDING",
                columns: table => new
                {
                    ID_LEILAO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_AST_LEILAO.NEXTVAL"),
                    ID_SATELITE = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_RECTENNA_ORIGEM = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DATA_HORA_INICIO = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DATA_HORA_FIM = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    GWH_DISPONIVEL = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    PRECO_MIN_POR_GWH = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    STATUS_LEILAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_LEILAO_BIDDING_PK", x => x.ID_LEILAO);
                });

            migrationBuilder.CreateTable(
                name: "AST_LOG_TRANSACAO",
                columns: table => new
                {
                    ID_TRANSACAO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SEQ_AST_TRANSACAO.NEXTVAL"),
                    ID_LEILAO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_CLIENTE_VENCEDOR = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    VALOR_ARREMATADO = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    TAXA_ASTRA = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: false),
                    DATA_FATURAMENTO = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AST_LOG_TRANSACAO_PK", x => x.ID_TRANSACAO);
                    table.ForeignKey(
                        name: "FK_AST_LOG_TRANSACAO_AST_CLIENTE_PREMIUM_ID_CLIENTE_VENCEDOR",
                        column: x => x.ID_CLIENTE_VENCEDOR,
                        principalTable: "AST_CLIENTE_PREMIUM",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AST_LOG_TRANSACAO_AST_LEILAO_BIDDING_ID_LEILAO",
                        column: x => x.ID_LEILAO,
                        principalTable: "AST_LEILAO_BIDDING",
                        principalColumn: "ID_LEILAO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UK_CLIENTE_CNPJ",
                table: "AST_CLIENTE_PREMIUM",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AST_LOG_TRANSACAO_ID_CLIENTE_VENCEDOR",
                table: "AST_LOG_TRANSACAO",
                column: "ID_CLIENTE_VENCEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_AST_LOG_TRANSACAO_ID_LEILAO",
                table: "AST_LOG_TRANSACAO",
                column: "ID_LEILAO");
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

            migrationBuilder.DropSequence(
                name: "SEQ_AST_CLIENTE");

            migrationBuilder.DropSequence(
                name: "SEQ_AST_LEILAO");

            migrationBuilder.DropSequence(
                name: "SEQ_AST_TRANSACAO");
        }
    }
}
