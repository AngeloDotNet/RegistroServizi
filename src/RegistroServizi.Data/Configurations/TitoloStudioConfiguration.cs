namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for the <see cref="TitoloStudio"/> entity.
/// </summary>
/// <remarks>
/// This configuration maps <see cref="TitoloStudio"/> to the <c>TitoliStudio</c> table,
/// sets <see cref="TitoloStudio.Id"/> as the primary key, enforces uniqueness on
/// <see cref="TitoloStudio.Descrizione"/>, and requires <see cref="TitoloStudio.Descrizione"/>
/// to have a maximum length of 200 characters.
/// </remarks>
internal class TitoloStudioConfiguration : IEntityTypeConfiguration<TitoloStudio>
{
    /// <summary>
    /// Applies the entity mapping rules for <see cref="TitoloStudio"/>.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the model.</param>
    public void Configure(EntityTypeBuilder<TitoloStudio> builder)
    {
        builder.ToTable("TitoliStudio");

        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.Descrizione)
            .IsUnique();

        builder.Property(a => a.Descrizione)
            .IsRequired()
            .HasMaxLength(200);
    }
}
