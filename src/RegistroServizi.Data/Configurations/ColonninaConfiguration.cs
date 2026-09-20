namespace RegistroServizi.Data.Configurations;

public class ColonninaConfiguration : IEntityTypeConfiguration<Colonnina>
{
    public void Configure(EntityTypeBuilder<Colonnina> builder)
    {
        builder.ToTable("Colonnine");

        builder.HasKey(c => c.Id);

        builder.HasIndex(a => a.NomeColonnina);

        builder.Property(c => c.NomeColonnina)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Comune)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Provincia)
            .IsRequired()
            .HasMaxLength(100);

        builder.ComplexProperty(o => o.Coordinate, coordinate =>
        {
            coordinate.Property(c => c.Latitudine)
                .HasDefaultValue(0.0);

            coordinate.Property(c => c.Longitudine)
                .HasDefaultValue(0.0);
        });
    }
}