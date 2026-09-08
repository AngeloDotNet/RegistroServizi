namespace RegistroServizi.Data.Configurations;

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
    }
}
