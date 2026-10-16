namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Provides the Entity Framework Core mapping for <see cref="TipologiaServizio"/>.
/// </summary>
/// <remarks>
/// This configuration maps the entity to the <c>TipologieServizi</c> table, defines <see cref="TipologiaServizio.Id"/>
/// as the primary key, and configures <see cref="TipologiaServizio.TipoServizio"/> as a required column with a
/// maximum length of 30 characters.
/// </remarks>
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