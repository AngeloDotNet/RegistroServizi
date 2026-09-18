namespace RegistroServizi.Data.Configurations;

public class OspedaleConfiguration : IEntityTypeConfiguration<Ospedale>
{
    public void Configure(EntityTypeBuilder<Ospedale> builder)
    {
        builder.ToTable("Ospedali");

        builder.HasKey(o => o.Id);

        builder.HasIndex(o => o.NomeOspedale).IsUnique();

        builder.Property(o => o.NomeOspedale).IsRequired().HasMaxLength(200);

        builder.ComplexProperty(o => o.Indirizzo, indirizzo =>
        {
            indirizzo.Property(i => i.Strada).IsRequired().HasMaxLength(200);
            indirizzo.Property(i => i.Citta).IsRequired().HasMaxLength(100);
            indirizzo.Property(i => i.Provincia).IsRequired().HasMaxLength(100);
            //indirizzo.Property(i => i.Cap).IsRequired();
            indirizzo.Property(i => i.Cap).HasDefaultValue(0);
        });

        builder.ComplexProperty(o => o.Coordinate, coordinate =>
        {
            //coordinate.Property(c => c.Latitudine).IsRequired();
            //coordinate.Property(c => c.Longitudine).IsRequired();
            coordinate.Property(c => c.Latitudine).HasDefaultValue(0.0);
            coordinate.Property(c => c.Longitudine).HasDefaultValue(0.0);
        });

        //builder.OwnsOne(o => o.Indirizzo);
        //builder.OwnsOne(o => o.Coordinate);
    }
}