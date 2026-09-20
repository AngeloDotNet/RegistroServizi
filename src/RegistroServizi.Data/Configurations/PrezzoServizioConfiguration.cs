namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for the PrezzoServizio entity.
/// </summary>
/// <remarks>Maps the entity to the 'PrezziServizi' table and sets Id as the primary key. Defines a required
/// relationship to TipologiaServizio with foreign key TipologiaServizioId and DeleteBehavior.Restrict. Configures
/// numeric properties: CostoFisso, CostoKm, SecondoTrasportato, and FermoMacchina as required with default 0 and
/// precision 18,2; Accompagnatore with precision 18,2; and ScontoSocio with default 0.</remarks>
public class PrezzoServizioConfiguration : IEntityTypeConfiguration<PrezzoServizio>
{
    public void Configure(EntityTypeBuilder<PrezzoServizio> builder)
    {
        builder.ToTable("PrezziServizi");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.TipologiaServizio)
            .WithMany(p => p.PrezziServizi)
            .HasForeignKey(p => p.TipologiaServizioId)
            .OnDelete(DeleteBehavior.Restrict);

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