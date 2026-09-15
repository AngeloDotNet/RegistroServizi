namespace RegistroServizi.Data.Configurations;

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