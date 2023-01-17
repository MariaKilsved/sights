using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sqlite.Models;

namespace sqlite.Data;

public partial class SqliteContext : DbContext
{
    public SqliteContext(DbContextOptions<SqliteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attraction> Attractions { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<SubComment> SubComments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attraction>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.CityId).HasColumnType("FOREGIN KEY INT");
            entity.Property(e => e.Cordinates).HasColumnType("DECIMAL");
            entity.Property(e => e.CountryId).HasColumnType("FOREGIN KEY INT");
            entity.Property(e => e.Description).HasColumnType("VARCHAR(5000)");
            entity.Property(e => e.Picture).HasColumnType("IMAGE");
            entity.Property(e => e.Title).HasColumnType("VARCHAR(100)");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.CountryId).HasColumnType("FOREGIN KEY INT");
            entity.Property(e => e.Name).HasColumnType("VARCHAR(100)");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.AttractionId).HasColumnType("INT");
            entity.Property(e => e.Content).HasColumnType("VARCHAR(1000)");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.Name).HasColumnType("VARCHAR(20)");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.AttractionId).HasColumnType("FOREGIN KEY INT");
            entity.Property(e => e.Like1)
                .HasColumnType("BOOLEAN")
                .HasColumnName("Like");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<SubComment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.CommentId).HasColumnType("FOREGIN KEY INT");
            entity.Property(e => e.Content).HasColumnType("VARCHAR(1000)");
            entity.Property(e => e.UserId).HasColumnType("FOREGIN KEY INT");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnType("IDENTETY");
            entity.Property(e => e.Password).HasColumnType("NVARCHAR(32)");
            entity.Property(e => e.Username).HasColumnType("VARCHAR(20)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
