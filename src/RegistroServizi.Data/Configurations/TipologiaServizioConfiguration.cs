namespace RegistroServizi.Data.Configurations;

public class TipologiaServizioConfiguration : IEntityTypeConfiguration<TipologiaServizio>
{
    public void Configure(EntityTypeBuilder<TipologiaServizio> builder)
    {
        builder.ToTable("TipologieServizi");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TipoServizio)
            .IsRequired()
            .HasMaxLength(30);
    }
}