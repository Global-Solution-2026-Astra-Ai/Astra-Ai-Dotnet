using Microsoft.EntityFrameworkCore;
using AstraAiDotnet.Models;
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
                    .HasColumnName("id_cliente")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razao_social")
                    .HasMaxLength(100);

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasMaxLength(14);

                entity.Property(e => e.DemandaContratadaGwh)
                    .HasColumnName("demanda_contratada_gwh")
                    .HasPrecision(6, 2);

                entity.Property(e => e.StatusCadastro)
                    .IsRequired()
                    .HasColumnName("status_cadastro")
                    .HasMaxLength(20);

            });

            modelBuilder.Entity<Leilao>(entity =>
            {
                entity.ToTable("AST_LEILAO_BIDDING");

                entity.HasKey(e => e.IdLeilao)
                    .HasName("AST_LEILAO_BIDDING_PK");

                entity.Property(e => e.IdLeilao)
                    .HasColumnName("id_leilao")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdSatelite)
                    .HasColumnName("id_satelite");

                entity.Property(e => e.IdRcdennaOrigem)
                    .HasColumnName("id_rcdenna_origem");

                entity.Property(e => e.DataHoraInicio)
                    .HasColumnName("data_hora_inicio")
                    .HasColumnType("TIMESTAMP");

                entity.Property(e => e.DataHoraFim)
                    .HasColumnName("data_hora_fim")
                    .HasColumnType("TIMESTAMP");

                entity.Property(e => e.GwhDisponivel)
                    .HasColumnName("gwh_disponivel")
                    .HasPrecision(10, 2);

                entity.Property(e => e.PrecoMinPorGwh)
                    .HasColumnName("preco_min_por_gwh")
                    .HasPrecision(10, 2);

                entity.Property(e => e.StatusLeilao)
                    .HasColumnName("status_leilao")
                    .HasMaxLength(20);

            });

            modelBuilder.Entity<LogTransacao>(entity =>
            {
                entity.ToTable("AST_LOG_TRANSACAO");

                entity.HasKey(e => e.IdTransacao)
                    .HasName("AST_LOG_TRANSACAO_PK");

                entity.Property(e => e.IdTransacao)
                    .HasColumnName("id_transacao")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdLeilao)
                    .HasColumnName("id_leilao");

                entity.Property(e => e.IdClienteVencedor)
                    .HasColumnName("id_cliente_vencedor");

                entity.Property(e => e.ValorArrematado)
                    .HasColumnName("valor_arrematado")
                    .HasPrecision(10, 2);

                entity.Property(e => e.TaxaAstra)
                    .HasColumnName("taxa_oneracao_astra")
                    .HasPrecision(10, 2);

                entity.Property(e => e.DataFaturamento)
                    .HasColumnName("data_faturamento")
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
