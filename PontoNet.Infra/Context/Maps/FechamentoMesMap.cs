using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoNet.Domain.Entities;

namespace PontoNet.Infra.Context.Maps
{
    public class FechamentoMesMap: IEntityTypeConfiguration<FechamentoMes>
    {
        public void Configure(EntityTypeBuilder<FechamentoMes> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.HasIndex(i => i.Mes);
        }
    }
}