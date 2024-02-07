using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP2part1.Models.EntityFramework;

public partial class Tp2part1Context : DbContext
{
    public Tp2part1Context()
    {
    }

    public Tp2part1Context(DbContextOptions<Tp2part1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Serie> Series { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432; Database=TP2Part1; uid=postgres; password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Serie>(entity =>
        {
            entity.HasKey(e => e.Serieid).HasName("serie_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
