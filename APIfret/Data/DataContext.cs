using System;
using APIfret.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIfret.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Iles> Iles { get; set; }
}