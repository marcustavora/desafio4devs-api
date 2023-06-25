using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio4Devs.Infra.Data.EntityConfigurations.Config
{
    public class AvaliacaoConfig : EntityConfiguration<Avaliacao>, IEntityTypeConfiguration<Avaliacao>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            DefaultConfigs(builder, tableName: "Avaliacao");

            builder.Property(x => x.MesAnoReferencia).HasMaxLength(6).IsRequired();

            builder.HasMany(x => x.ClienteAvaliacao).WithOne(x => x.Avaliacao).HasForeignKey("AvaliacaoId");
        }
    }
}