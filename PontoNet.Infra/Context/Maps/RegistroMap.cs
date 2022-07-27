using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoNet.Domain.Entities;

namespace PontoNet.Infra.Context.Maps
{
    public class RegistroMap : IEntityTypeConfiguration<Registro>
    {
        public void Configure(EntityTypeBuilder<Registro> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.HasIndex(i => i.Data);

            builder.Property(c => c.HoraInicial)
                .HasConversion(new TimeSpanToTicksConverter());

            builder.Property(c => c.HoraFinal)
                .HasConversion(new TimeSpanToTicksConverter());
        }
    }
}