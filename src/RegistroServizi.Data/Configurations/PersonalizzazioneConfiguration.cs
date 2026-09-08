namespace RegistroServizi.Data.Configurations;

public class PersonalizzazioneConfiguration : IEntityTypeConfiguration<Personalizzazione>
{
    public void Configure(EntityTypeBuilder<Personalizzazione> builder)
    {
        builder.ToTable("Personalizzazioni");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.NomeAssociazione);

        builder.Property(p => p.NomeAssociazione)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.SiglaAssociazione)
            .IsRequired()
            .HasMaxLength(50);
    }
}