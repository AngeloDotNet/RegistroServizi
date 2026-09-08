namespace RegistroServizi.Data.Configurations;

public class PrezzoServizioConfiguration : IEntityTypeConfiguration<PrezzoServizio>
{
    public void Configure(EntityTypeBuilder<PrezzoServizio> builder)
    {
        builder.ToTable("PrezziServizi");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.TipologiaServizio);

        builder.Property(p => p.TipologiaServizio)
            .HasConversion<string>()
            .HasMaxLength(200);

        builder.Property(p => p.CostoFisso)
            .IsRequired()
            .HasDefaultValue(0)
            .HasPrecision(18, 2);

        builder.Property(p => p.CostoKm)
            .IsRequired()
            .HasDefaultValue(0)
            .HasPrecision(18, 2);

        builder.Property(p => p.SecondoTrasportato)
            .IsRequired()
            .HasDefaultValue(0)
            .HasPrecision(18, 2);

        builder.Property(p => p.FermoMacchina)
            .IsRequired()
            .HasDefaultValue(0)
            .HasPrecision(18, 2);

        builder.Property(p => p.Accompagnatore)
            .HasPrecision(18, 2);

        builder.Property(p => p.ScontoSocio)
            .HasDefaultValue(0);
    }
}