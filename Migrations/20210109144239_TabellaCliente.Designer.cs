﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroServizi.Models.Services.Infrastructure;

namespace RegistroServizi.Migrations
{
    [DbContext(typeof(RegistroServiziDbContext))]
    [Migration("20210109144239_TabellaCliente")]
    partial class TabellaCliente
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
#pragma warning restore 612, 618
        }
    }
}
