using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astra_Ai_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class FixLeilaoMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ID_TRANSACAO",
                table: "AST_LOG_TRANSACAO",
                type: "NUMBER(19)",
                nullable: false,
                defaultValueSql: "SEQ_AST_TRANSACAO.NEXTVAL",
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "ID_CLIENTE",
                table: "AST_CLIENTE_PREMIUM",
                type: "NUMBER(19)",
                nullable: false,
                defaultValueSql: "SEQ_AST_CLIENTE.NEXTVAL",
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ID_TRANSACAO",
                table: "AST_LOG_TRANSACAO",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)",
                oldDefaultValueSql: "SEQ_AST_TRANSACAO.NEXTVAL")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "ID_CLIENTE",
                table: "AST_CLIENTE_PREMIUM",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)",
                oldDefaultValueSql: "SEQ_AST_CLIENTE.NEXTVAL")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");
        }
    }
}
