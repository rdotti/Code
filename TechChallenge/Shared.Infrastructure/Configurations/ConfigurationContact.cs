using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities;

namespace Shared.Infraestructure.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(p => p.DataCriacao).HasColumnType("DateTime").HasDefaultValueSql("GetDate()").ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.Nome).HasMaxLength(150);
            builder.Property(p => p.Telefone).HasMaxLength(11);
            builder.Property(p => p.EMail).HasMaxLength(250);
        }
    }
}
