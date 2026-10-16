namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for <see cref="Ospedale"/>.
/// </summary>
/// <remarks>
/// Maps the entity to the <c>Ospedali</c> table, uses the inherited <c>Id</c> property as the primary key,
/// enforces a unique index on <see cref="Ospedale.NomeOspedale"/>, and configures the complex properties
/// <see cref="Ospedale.Ubicazione"/> and <see cref="Ospedale.Coordinate"/> with required fields, maximum lengths,
/// and default values.
/// </remarks>
public class OspedaleConfiguration : IEntityTypeConfiguration<Ospedale>
{
    /// <summary>
    /// Applies the database schema configuration for <see cref="Ospedale"/>.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the model.</param>
    public void Configure(EntityTypeBuilder<Ospedale> builder)
    {
        builder.ToTable("Ospedali");

        builder.HasKey(o => o.Id);

        builder.HasIndex(o => o.NomeOspedale)
            .IsUnique();

        builder.Property(o => o.NomeOspedale)
            .IsRequired()
            .HasMaxLength(200);

        builder.ComplexProperty(o => o.Ubicazione, ubicazione =>
        {
            ubicazione.Property(i => i.Indirizzo)
                .IsRequired()
                .HasMaxLength(200);

            ubicazione.Property(i => i.Comune)
                .IsRequired()
                .HasMaxLength(100);

            ubicazione.Property(i => i.Provincia)
                .IsRequired()
                .HasMaxLength(100);

            ubicazione.Property(i => i.Cap)
                .HasDefaultValue(0);
        });

        builder.ComplexProperty(o => o.Coordinate, coordinate =>
        {
            coordinate.Property(c => c.Latitudine)
                .HasDefaultValue(0.0);

            coordinate.Property(c => c.Longitudine)
                .HasDefaultValue(0.0);
        });
    }
}