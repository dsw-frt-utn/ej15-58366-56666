using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data;

public class Dsw2026Ej15DbContext: DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public Dsw2026Ej15DbContext(DbContextOptions<Dsw2026Ej15DbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(e =>
        {
            e.ToTable("Doctors");
            e.Property(d => d.Name).HasMaxLength(100).IsRequired();
            e.Property(d => d.LicenseNumber).HasMaxLength(50).IsRequired();
            e.HasIndex(d => d.LicenseNumber).IsUnique();
        });

        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
        var json = File.ReadAllText(jsonPath);
        var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];

        modelBuilder.Entity<Speciality>(e =>
        {
            e.ToTable("Specialities");
            e.Property(s => s.Name).HasMaxLength(100).IsRequired();
            e.Property(s => s.Description).HasMaxLength(300).IsRequired();
            e.HasData(specialities.Select(s => new Speciality(s.Name, s.Description, s.Id)).ToList());
        });
    }
}