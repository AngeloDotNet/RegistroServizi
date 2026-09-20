namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the TipologiaServizio entity for Entity Framework Core: maps to the "TipologieServizi" table, sets Id as
/// the primary key, and configures TipoServizio as required with a maximum length of 30.
/// </summary>
/// <remarks>Applied in DbContext.OnModelCreating via
/// modelBuilder.ApplyConfiguration<TipologiaServizioConfiguration>().</remarks>
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