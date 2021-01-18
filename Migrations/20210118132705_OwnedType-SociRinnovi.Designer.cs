﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Migrations
{
    [DbContext(typeof(RegistroServiziDbContext))]
    [Migration("20210118132705_OwnedType-SociRinnovi")]
    partial class OwnedTypeSociRinnovi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("RegistroServizi.Models.Entities.Associazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cap")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comune")
                        .HasColumnType("TEXT");

                    b.Property<string>("Denominazione")
                        .HasColumnType("TEXT");

                    b.Property<string>("Indirizzo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sigla")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Associazione");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cap")
                        .HasColumnType("TEXT");

                    b.Property<string>("CodiceFiscale")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comune")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fax")
                        .HasColumnType("TEXT");

                    b.Property<string>("Indirizzo")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartitaIva")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("RagioneSociale")
                        .HasColumnType("TEXT");

                    b.Property<int>("Spese")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.Costo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScontoSoci")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoServizio")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Costo");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.Socio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cap")
                        .HasColumnType("TEXT");

                    b.Property<string>("CodiceFiscale")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comune")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascita")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataTesseramento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Indirizzo")
                        .HasColumnType("TEXT");

                    b.Property<string>("LuogoNascita")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nominativo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Professione")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tessera")
                        .HasColumnType("TEXT");

                    b.Property<string>("TrattamentoDati")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zona")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Socio");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.SocioFamiliare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Familiare")
                        .HasColumnType("TEXT");

                    b.Property<int>("SocioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SocioFamiliare");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.SocioRinnovo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Anno")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataRinnovo")
                        .HasColumnType("TEXT");

                    b.Property<int>("SocioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SocioRinnovo");
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.Costo", b =>
                {
                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "Accompagnatore", b1 =>
                        {
                            b1.Property<int>("CostoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("Accompagnatore_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("Accompagnatore_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("CostoId");

                            b1.ToTable("Costo");

                            b1.WithOwner()
                                .HasForeignKey("CostoId");
                        });

                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "CostoFisso", b1 =>
                        {
                            b1.Property<int>("CostoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("CostoFisso_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("CostoFisso_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("CostoId");

                            b1.ToTable("Costo");

                            b1.WithOwner()
                                .HasForeignKey("CostoId");
                        });

                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "CostoKm", b1 =>
                        {
                            b1.Property<int>("CostoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("CostoKm_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("CostoKm_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("CostoId");

                            b1.ToTable("Costo");

                            b1.WithOwner()
                                .HasForeignKey("CostoId");
                        });

                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "FermoMacchina", b1 =>
                        {
                            b1.Property<int>("CostoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("FermoMacchina_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("FermoMacchina_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("CostoId");

                            b1.ToTable("Costo");

                            b1.WithOwner()
                                .HasForeignKey("CostoId");
                        });

                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "SecondoTrasportato", b1 =>
                        {
                            b1.Property<int>("CostoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("SecondoTrasportato_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("SecondoTrasportato_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("CostoId");

                            b1.ToTable("Costo");

                            b1.WithOwner()
                                .HasForeignKey("CostoId");
                        });
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.SocioFamiliare", b =>
                {
                    b.HasOne("RegistroServizi.Models.Entities.Socio", "Socio")
                        .WithMany("SociFamiliari")
                        .HasForeignKey("SocioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegistroServizi.Models.Entities.SocioRinnovo", b =>
                {
                    b.HasOne("RegistroServizi.Models.Entities.Socio", "Socio")
                        .WithMany("SociRinnovi")
                        .HasForeignKey("SocioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("RegistroServizi.Models.ValueTypes.Money", "Quota", b1 =>
                        {
                            b1.Property<int>("SocioRinnovoId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("Amount")
                                .HasColumnName("Quota_Amount")
                                .HasColumnType("REAL");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("Quota_Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("SocioRinnovoId");

                            b1.ToTable("SocioRinnovo");

                            b1.WithOwner()
                                .HasForeignKey("SocioRinnovoId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
