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
            entity.HasOne(d => d.City).WithMany(p => p.Attractions).HasForeignKey(d => d.CityId);

            entity.HasOne(d => d.Country).WithMany(p => p.Attractions).HasForeignKey(d => d.CountryId);

            entity.HasOne(d => d.User).WithMany(p => p.Attractions).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Cities_Name").IsUnique();

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).HasForeignKey(d => d.CountryId);

            entity.HasOne(d => d.User).WithMany(p => p.Cities).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Attraction).WithMany(p => p.Comments).HasForeignKey(d => d.AttractionId);

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Countries_Name").IsUnique();

            entity.HasOne(d => d.User).WithMany(p => p.Countries).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.Property(e => e.Like1).HasColumnName("Like");

            entity.HasOne(d => d.Attraction).WithMany(p => p.Likes).HasForeignKey(d => d.AttractionId);

            entity.HasOne(d => d.User).WithMany(p => p.Likes).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SubComment>(entity =>
        {
            entity.HasOne(d => d.Comment).WithMany(p => p.SubComments).HasForeignKey(d => d.CommentId);

            entity.HasOne(d => d.User).WithMany(p => p.SubComments).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
