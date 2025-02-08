﻿// <auto-generated />
using APIfret.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIfret.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("APIfret.Entities.Iles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("codezonetarifaire")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Iles");
                });

            modelBuilder.Entity("APIfret.Entities.Tarifsrevatuas", b =>
                {
                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Refrigere")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TauxPriseEnCharge")
                        .HasColumnType("INTEGER");

                    b.ToTable("Tarifsrevatuas");
                });
#pragma warning restore 612, 618
        }
    }
}
