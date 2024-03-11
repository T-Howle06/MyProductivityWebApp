#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyProductivityWebApp.Data.Data
{
    public partial class MyProductivityWebAppContext : DbContext
    {
        public MyProductivityWebAppContext()
        {
        }

        public MyProductivityWebAppContext(DbContextOptions<MyProductivityWebAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>(entity =>
            {
                entity.ToTable("WeatherForecast");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Summary).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}