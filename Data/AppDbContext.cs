    using ApiSistemaGeek.Model;
    using Microsoft.EntityFrameworkCore;

   namespace ApiSistemaGeek.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<ProdutoGeek> ProdutoGeek { get; set; }
        
    }
    }

