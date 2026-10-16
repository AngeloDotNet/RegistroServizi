namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Provides the Entity Framework Core configuration for the <see cref="StatoBolla"/> entity.
/// </summary>
/// <remarks>
/// This configuration maps <see cref="StatoBolla"/> to the <c>StatiBolla</c> table, defines <see cref="StatoBolla.Id"/>
/// as the primary key, enforces a unique constraint on <see cref="StatoBolla.Descrizione"/>, and limits the
/// description length to 200 characters.
/// </remarks>
public class StatoBollaConfiguration : IEntityTypeConfiguration<StatoBolla>
{
    /// <summary>
    /// Configures the entity mapping for <see cref="StatoBolla"/>.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the model.</param>
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