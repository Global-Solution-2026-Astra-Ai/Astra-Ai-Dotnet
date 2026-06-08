using Microsoft.EntityFrameworkCore;
using AstraAiDotnet.Leiloes.Models;
using AstraAiDotnet.LogTransacoes.Models;
using AstraAiDotnet.ClientesPremium.Models;

namespace AstraAiDotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClientePremium> ClientesPremium { get; set; }
        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<LogTransacao> LogTransacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientePremium>(entity =>
            {
                entity.ToTable("AST_CLIENTE_PREMIUM");

                entity.HasKey(e => e.IdCliente)
                    .HasName("AST_CLIENTE_PREMIUM_PK");

                entity.HasIndex(e => e.Cnpj)
                    .IsUnique()
                    .HasDatabaseName("UK_CLIENTE_CNPJ");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("ID_CLIENTE")
                    .HasDefaultValueSql("SEQ_AST_CLIENTE.NEXTVAL")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("RAZAO_SOCIAL")
                    .HasMaxLength(100);

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(14);

                entity.Property(e => e.DemandaContratadaGwh)
                    .HasColumnName("DEMANDA_CONTRATADA_GWH")
                    .HasPrecision(6, 2);

                entity.Property(e => e.StatusCadastro)
                    .IsRequired()
                    .HasColumnName("STATUS_CADASTRO")
                    .HasMaxLength(20);

            });

            modelBuilder.Entity<Leilao>(entity =>
            {
                entity.ToTable("AST_LEILAO_BIDDING");

                entity.HasKey(e => e.IdLeilao)
                    .HasName("AST_LEILAO_BIDDING_PK");

                entity.Property(e => e.IdLeilao)
                    .HasColumnName("ID_LEILAO")
                    .HasDefaultValueSql("SEQ_AST_LEILAO.NEXTVAL")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdSatelite)
                    .HasColumnName("ID_SATELITE");

                entity.Property(e => e.IdRectennaOrigem)
                    .HasColumnName("ID_RECTENNA_ORIGEM");

                entity.Property(e => e.DataHoraInicio)
                    .HasColumnName("DATA_HORA_INICIO")
                    .HasColumnType("TIMESTAMP");

                entity.Property(e => e.DataHoraFim)
                    .HasColumnName("DATA_HORA_FIM")
                    .HasColumnType("TIMESTAMP");

                entity.Property(e => e.GwhDisponivel)
                    .HasColumnName("GWH_DISPONIVEL")
                    .HasPrecision(10, 2);

                entity.Property(e => e.PrecoMinPorGwh)
                    .HasColumnName("PRECO_MIN_POR_GWH")
                    .HasPrecision(10, 2);

                entity.Property(e => e.StatusLeilao)
                    .HasColumnName("STATUS_LEILAO")
                    .HasMaxLength(20);

            });

            modelBuilder.Entity<LogTransacao>(entity =>
            {
                entity.ToTable("AST_LOG_TRANSACAO");

                entity.HasKey(e => e.IdTransacao)
                    .HasName("AST_LOG_TRANSACAO_PK");

                entity.Property(e => e.IdTransacao)
                    .HasColumnName("ID_TRANSACAO")
                    .HasDefaultValueSql("SEQ_AST_TRANSACAO.NEXTVAL")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdLeilao)
                    .HasColumnName("ID_LEILAO");

                entity.Property(e => e.IdClienteVencedor)
                    .HasColumnName("ID_CLIENTE_VENCEDOR");

                entity.Property(e => e.ValorArrematado)
                    .HasColumnName("VALOR_ARREMATADO")
                    .HasPrecision(10, 2);

                entity.Property(e => e.TaxaAstra)
                    .HasColumnName("TAXA_ASTRA")
                    .HasPrecision(10, 2);

                entity.Property(e => e.DataFaturamento)
                    .HasColumnName("DATA_FATURAMENTO")
                    .HasColumnType("DATE");

                entity.HasOne(e => e.Leilao)
                    .WithMany(l => l.LogTransacoes)
                    .HasForeignKey(e => e.IdLeilao)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ClienteVencedor)
                    .WithMany(c => c.Transacoes)
                    .HasForeignKey(e => e.IdClienteVencedor)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
