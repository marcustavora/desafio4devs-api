using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio4Devs.Infra.Data.EntityConfigurations.Config
{
    public class ClienteAvaliacaoConfig : EntityConfiguration<ClienteAvaliacao>, IEntityTypeConfiguration<ClienteAvaliacao>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<ClienteAvaliacao> builder)
        {
            DefaultConfigs(builder, tableName: "ClienteAvaliacao");

            builder.Property(x => x.ClienteId).IsRequired();
            builder.Property(x => x.AvaliacaoId).IsRequired();
            builder.Property(x => x.Nota).IsRequired();
            builder.Property(x => x.MotivoNota).HasMaxLength(512).IsRequired();
        }
    }
}