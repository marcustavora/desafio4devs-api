using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio4Devs.Infra.Data.EntityConfigurations.Config
{
    public class ClienteConfig : EntityConfiguration<Cliente>, IEntityTypeConfiguration<Cliente>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            DefaultConfigs(builder, tableName: "Cliente");

            builder.Property(x => x.Nome).HasMaxLength(256).IsRequired();
            builder.Property(x => x.NomeContato).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Cnpj).HasMaxLength(14);
            builder.Property(x => x.Ativo);

            builder.HasMany(x => x.ClienteAvaliacao).WithOne(x => x.Cliente).HasForeignKey("ClienteId");
        }
    }
}