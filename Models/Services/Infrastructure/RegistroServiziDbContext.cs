using Microsoft.EntityFrameworkCore;
using RegistroServizi.Models.Entities;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public partial class RegistroServiziDbContext : DbContext
    {
        public RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Associazione> Associazioni { get; set; }
        public virtual DbSet<Costo> Costi { get; set; }
        public virtual DbSet<Cliente> Clienti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Associazione>(entity =>
            {
                entity.ToTable("Associazione");
                entity.HasKey(associazione => associazione.Id);
            });

            modelBuilder.Entity<Costo>(entity =>
            {
                entity.ToTable("Costo");
                entity.HasKey(costo => costo.Id);

                entity.OwnsOne(costo => costo.CostoFisso, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("CostoFisso_Currency");
                    builder.Property(money => money.Amount)
                           .HasColumnName("CostoFisso_Amount")
                           .HasConversion<float>();
                });

                entity.OwnsOne(costo => costo.CostoKm, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("CostoKm_Currency");
                    builder.Property(money => money.Amount)
                           .HasColumnName("CostoKm_Amount")
                           .HasConversion<float>();
                });

                entity.OwnsOne(costo => costo.SecondoTrasportato, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("SecondoTrasportato_Currency");
                    builder.Property(money => money.Amount)
                           .HasColumnName("SecondoTrasportato_Amount")
                           .HasConversion<float>();
                });

                entity.OwnsOne(costo => costo.FermoMacchina, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("FermoMacchina_Currency");
                    builder.Property(money => money.Amount)
                           .HasColumnName("FermoMacchina_Amount")
                           .HasConversion<float>();
                });

                entity.OwnsOne(costo => costo.Accompagnatore, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("Accompagnatore_Currency");
                    builder.Property(money => money.Amount)
                           .HasColumnName("Accompagnatore_Amount")
                           .HasConversion<float>();
                });
            });
        
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(cliente => cliente.Id);
            });
        }
    }
}