    using ApiSistemaGeek.Model;
    using Microsoft.EntityFrameworkCore;
    using ApiSistemaGeek.Data;


namespace ApiSistemaGeek.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<ProdutoGeek> ProdutoGeek { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }

            public DbSet<Pedido> Pedidos { get; set; }
            public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoGeek>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PedidoItem>()
                .HasOne<Pedido>()
                .WithMany()
                .HasForeignKey(p => p.PedidoId);

            modelBuilder.Entity<PedidoItem>()
                .HasOne<ProdutoGeek>()
                .WithMany()
                .HasForeignKey(p => p.ProdutoGeekId);
        }

    }






}

