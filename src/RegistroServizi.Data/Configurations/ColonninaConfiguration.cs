namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Provides the EF Core configuration for the <see cref="Colonnina"/> entity.
/// </summary>
/// <remarks>
/// This configuration:
/// <list type="bullet">
/// <item><description>Maps <see cref="Colonnina"/> to the <c>Colonnine</c> table.</description></item>
/// <item><description>Defines <see cref="Colonnina.Id"/> as the primary key.</description></item>
/// <item><description>Creates an index on <see cref="Colonnina.NomeColonnina"/>.</description></item>
/// <item><description>Requires <see cref="Colonnina.NomeColonnina"/>, <see cref="Colonnina.Comune"/>, and <see cref="Colonnina.Provincia"/>.</description></item>
/// <item><description>Limits the length of <see cref="Colonnina.NomeColonnina"/> to 200 characters and <see cref="Colonnina.Comune"/> and <see cref="Colonnina.Provincia"/> to 100 characters each.</description></item>
/// <item><description>Configures <see cref="Colonnina.Coordinate"/> as a complex property and assigns default values of <c>0.0</c> to <c>Latitudine</c> and <c>Longitudine</c>.</description></item>
/// </list>
/// </remarks>
public class ColonninaConfiguration : IEntityTypeConfiguration<Colonnina>
{
    /// <summary>
    /// Applies the EF Core mapping rules for the <see cref="Colonnina"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The entity type builder used to configure the table mapping, key, indexes,
    /// property constraints, and complex property mappings.
    /// </param>
    public void Configure(EntityTypeBuilder<Colonnina> builder)
    {
        builder.ToTable("Colonnine");

        builder.HasKey(c => c.Id);

        builder.HasIndex(a => a.NomeColonnina);

        builder.Property(c => c.NomeColonnina)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Comune)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Provincia)
            .IsRequired()
            .HasMaxLength(100);

        builder.ComplexProperty(o => o.Coordinate, coordinate =>
        {
            coordinate.Property(c => c.Latitudine)
                .HasDefaultValue(0.0);

            coordinate.Property(c => c.Longitudine)
                .HasDefaultValue(0.0);
        });
    }
}