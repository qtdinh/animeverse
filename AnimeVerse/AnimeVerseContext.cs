using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AnimeVerse;

public partial class AnimeVerseContext : DbContext
{
    public AnimeVerseContext()
    {
    }

    public AnimeVerseContext(DbContextOptions<AnimeVerseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Character");

            entity.Property(e => e.CharacterId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CharacterID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");

            entity.HasOne(d => d.CharacterNavigation).WithOne(p => p.Character)
                .HasForeignKey<Character>(d => d.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Character_Series");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
