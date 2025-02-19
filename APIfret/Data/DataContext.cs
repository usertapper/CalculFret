using System;
using APIfret.Controllers;
using APIfret.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Ile> Iles { get; set; }
    public DbSet<TarifsRevatua> Tarifsrevatuas { get; set; }
    public DbSet<TarifFret> TarifFrets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TarifsRevatua>()
            .HasNoKey();
        modelBuilder.Entity<TarifFret>()
            .HasNoKey();
    }
}



