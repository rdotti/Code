using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Core.Entities;

namespace TechChallenge.Infraestructure.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connString;

        public ApplicationDbContext()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            _connString = configuration.GetConnectionString("TechChallenge") ?? throw new Exception("String de conexão não informada");
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
