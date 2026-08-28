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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoGeek>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);
        }

    }






}

