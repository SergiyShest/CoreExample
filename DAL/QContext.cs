using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Core;
using NLog.Extensions.Logging;

namespace DAL;

public partial class QContext : DbContext
{
    protected IConfiguration Configuration;

    public QContext()
    {
        var config = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json")
     .Build();
        Configuration = config;

    }

    public QContext(DbContextOptions<QContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }



    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //  => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UserPortal;Username=postgres;Password=Qazwsx123");


        var loggerFactory = LoggerFactory.Create(builder =>
        {
            // builder.AddFilter("Microsoft", LogLevel.Warning)
            // AddFilter("System", LogLevel.Warning)
            builder.AddFilter("", LogLevel.Debug);
        }

        );
        loggerFactory.AddNLog().ConfigureNLog("NLog.config"); ;

        options.UseLoggerFactory(loggerFactory) //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging();

        var connect = Configuration.GetConnectionString("WebApiDatabase");
        options.UseNpgsql(connect);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pgcrypto");


        modelBuilder.Entity<User>(entity =>
      {
          entity.ToTable("Users");
          entity.HasIndex(e => e.Email, "IX_user_email");
          entity.Property(e => e.Id).HasColumnName("id");

      });

        modelBuilder.Entity<Vjsf>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Name, "IX_Vjsf_Name").IsUnique();
            entity.Property(e => e.Options).HasColumnType("jsonb");
            entity.Property(e => e.Schema).HasColumnType("jsonb");
        });
        modelBuilder.Entity<Questionnaire>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Name, "IX_Questionnaire_Name").IsUnique();

        });
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
