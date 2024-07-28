using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using TechChallenge.Core.Entities;

namespace TechChallenge.Infraestructure.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string? _connString;

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _connString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString 
                ?? throw new Exception("String de conexão não informada");
        }

        public ApplicationDbContext(string connString) { _connString = connString; }


        #region DbSet
        public DbSet<Contato> Contato { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
