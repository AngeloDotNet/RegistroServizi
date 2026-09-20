namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for <see cref="StatoBolla"/>.
/// </summary>
/// <remarks>Maps the entity to the 'StatiBolla' table, sets Id as the primary key, requires Descrizione with a
/// maximum length of 200 and adds a unique index on Descrizione.</remarks>
public class StatoBollaConfiguration : IEntityTypeConfiguration<StatoBolla>
{
    public void Configure(EntityTypeBuilder<StatoBolla> builder)
    {
        builder.ToTable("StatiBolla");

        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.Descrizione)
            .IsUnique();

        builder.Property(a => a.Descrizione)
            .IsRequired()
            .HasMaxLength(200);
    }
}