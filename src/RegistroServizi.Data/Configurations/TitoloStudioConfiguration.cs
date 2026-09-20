namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for the TitoloStudio entity.
/// </summary>
/// <remarks>Maps the entity to the 'TitoliStudio' table, sets the primary key to Id, enforces a unique index on
/// Descrizione, and configures Descrizione as required with a maximum length of 200 characters.</remarks>
internal class TitoloStudioConfiguration : IEntityTypeConfiguration<TitoloStudio>
{
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
