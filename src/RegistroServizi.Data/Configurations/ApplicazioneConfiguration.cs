namespace RegistroServizi.Data.Configurations;

/// <summary>
/// Configures the Entity Framework Core model for the Applicazione entity.
/// </summary>
/// <remarks>Maps the entity to the "Applicazioni" table; sets the primary key to Id; creates an index on
/// NomeApplicazione; configures NomeApplicazione as required with a maximum length of 200, Versione as required with a
/// maximum length of 15, and TimeZone as required with a maximum length of 50.</remarks>
public class ApplicazioneConfiguration : IEntityTypeConfiguration<Applicazione>
{
    public void Configure(EntityTypeBuilder<Applicazione> builder)
    {
        builder.ToTable("Applicazioni");

        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.NomeApplicazione);

        builder.Property(a => a.NomeApplicazione)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Versione)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(a => a.TimeZone)
            .IsRequired()
            .HasMaxLength(50);
    }
}