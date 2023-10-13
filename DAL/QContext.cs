using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Core;
using NLog.Extensions.Logging;
using Npgsql;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace DAL;

public partial class QContext : DbContext
{
    protected IConfiguration Configuration;

	static QContext()
	{
		AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            NpgsqlConnection.GlobalTypeMapper.UseNodaTime();

	}


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
		// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		//  AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
		NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
		var dataSourceBuilder = new NpgsqlDataSourceBuilder();
		dataSourceBuilder.UseNodaTime();
		var dataSource = dataSourceBuilder.Build();

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

		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connect);
		dataSourceBuilder
			.UseLoggerFactory(loggerFactory) // Configure logging
//			.UsePeriodicPasswordProvider() // Automatically rotate the password periodically
			.UseNodaTime(); // Use NodaTime for date/time types
		await using var dataSource = dataSourceBuilder.Build();


	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

	


		modelBuilder
            .HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<AvaiableUser>(entity =>
        {
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<QuestionImage>(entity =>
        {
            entity.ToTable("QuestionImage");
            entity.HasIndex(e => e.Name, "ix_question_image").IsUnique();
            entity.Property(e => e.Id).HasColumnName("id");

        });


        modelBuilder.Entity<User>(entity =>
      {
          entity.ToTable("Users");
          entity.HasIndex(e => e.Email, "IX_user_email").IsUnique();
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

		//modelBuilder.ApplyUtcDateTimeConverter();

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

public static class UtcDateAnnotation
{
	private const string IsUtcAnnotation = "IsUtc";
	private static readonly ValueConverter<DateTime, DateTime> UtcConverter = new ValueConverter<DateTime, DateTime>(convertTo => DateTime.SpecifyKind(convertTo, DateTimeKind.Utc), convertFrom => convertFrom);

	public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, bool isUtc = true) => builder.HasAnnotation(IsUtcAnnotation, isUtc);

	public static bool IsUtc(this IMutableProperty property)
	{
		if (property != null && property.PropertyInfo != null)
		{
			var attribute = property.PropertyInfo.GetCustomAttribute<IsUtcAttribute>();
			if (attribute is not null && attribute.IsUtc)
			{
				return true;
			}

			return ((bool?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;
		}
		return true;
	}

	/// <summary>
	/// Make sure this is called after configuring all your entities.
	/// </summary>
	public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
	{
		foreach (var entityType in builder.Model.GetEntityTypes())
		{
			foreach (var property in entityType.GetProperties())
			{
				if (!property.IsUtc())
				{
					continue;
				}

				if (property.ClrType == typeof(DateTime) ||
					property.ClrType == typeof(DateTime?))
				{
					property.SetValueConverter(UtcConverter);
				}
			}
		}
	}
}
public class IsUtcAttribute : Attribute
{
	public IsUtcAttribute(bool isUtc = true) => this.IsUtc = isUtc;
	public bool IsUtc { get; }
}