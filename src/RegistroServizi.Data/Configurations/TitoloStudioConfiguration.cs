namespace RegistroServizi.Data.Configurations;

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
