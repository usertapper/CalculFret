using System;
using APIfret.Controllers;
using APIfret.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Iles> Iles { get; set; }
    public DbSet<Tarifsrevatuas> Tarifsrevatuas { get; set; }
    public DbSet<TarifFrets> TarifFrets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarifsrevatuas>()
            .HasNoKey();
        modelBuilder.Entity<TarifFrets>()
            .HasNoKey();
    }
}



