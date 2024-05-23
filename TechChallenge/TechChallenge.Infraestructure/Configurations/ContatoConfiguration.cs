using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechChallenge.Core.Entities;

namespace TechChallenge.Infraestructure.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.Property(p => p.DataCriacao).HasColumnType("DateTime").HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.Nome).HasMaxLength(150);
            builder.Property(p => p.Telefone).HasMaxLength(11);
            builder.Property(p => p.EMail).HasMaxLength(250);
        }
    }
}
