using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AnimeVerse;

public partial class AnimeVerseContext : IdentityDbContext<AnimeVerseUser>
{
    public AnimeVerseContext()
    {
    }

    public AnimeVerseContext(DbContextOptions<AnimeVerseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<SeriesItem> Series { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString(name: "AnimeVerseContext"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Character");

            entity.Property(e => e.CharacterId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
        });

        modelBuilder.Entity<SeriesItem>(entity =>
        {
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasOne(d => d.SeriesItem).WithMany(p => p.Characters)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Characters_Series");
            });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
