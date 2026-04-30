using AlocacaoDeVeiculos.Models;
using Microsoft.EntityFrameworkCore;


namespace AlocacaoDeVeiculos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //
        }

        public DbSet<LocacaoModel> Alocacao { get; set; }
        public DbSet<CategoriaModel> Categoria { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<VeiculoModel> Veiculo { get; set; }
    }
}
