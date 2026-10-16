namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for <see cref="PrezzoServizio"/>.
/// </summary>
/// <remarks>
/// <para>Maps <see cref="PrezzoServizio"/> to the <c>PrezziServizi</c> table and uses <c>Id</c> as the primary key.</para>
/// <para>Configures the required relationship to <see cref="TipologiaServizio"/> through <c>TipologiaServizioId</c>
/// with <see cref="DeleteBehavior.Restrict"/> to prevent cascading deletes.</para>
/// <para>Configures the monetary fields <c>CostoFisso</c>, <c>CostoKm</c>, <c>SecondoTrasportato</c>, and
/// <c>FermoMacchina</c> as required with a default value of 0 and precision <c>18,2</c>; <c>Accompagnatore</c>
/// with precision <c>18,2</c>; and <c>ScontoSocio</c> with a default value of 0.</para>
/// </remarks>
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