using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PontoNet.Domain.Entities;
using PontoNet.Infra.Context.Maps;

namespace PontoNet.Infra.Context
{
    public class PontoContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<FechamentoMes> FechamentosMes => Set<FechamentoMes>();
        public DbSet<Registro> Registros => Set<Registro>();

        public PontoContext(DbContextOptions<PontoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PontoDb"));
            //optionsBuilder.UseSqlite(_configuration.GetConnectionString("SqliteConnection"), b => b.MigrationsAssembly("Formula1.Infra"));
            optionsBuilder.UseInMemoryDatabase("PontoNet");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FechamentoMesMap());
            modelBuilder.ApplyConfiguration(new RegistroMap());
        }
    }
}