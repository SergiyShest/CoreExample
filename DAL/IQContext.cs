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
using Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DAL;

public interface IQContext
{
    // Другие свойства и методы, если нужно
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();

    DatabaseFacade Database {  get; }

	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;


	DbSet<TEntity> Set< TEntity>()where TEntity : class;
}



public partial class QContext : DbContext,IQContext
{
	protected IConfiguration Configuration;

	static QContext()
	{
		AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


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

		var connectString = Configuration.GetConnectionString("WebApiDatabase");
		options.UseNpgsql(connectString);




	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{




		modelBuilder
			.HasPostgresExtension("pgcrypto");


		modelBuilder.Entity<HitCounter>(entity =>
		{
			entity.Property(e => e.Id).HasColumnName("Id");
			entity.Property(e => e.Cdate)
		   .HasColumnType("timestamp with time zone")
		   .HasColumnName("Cdate");
		});

		modelBuilder.Entity<Answer>(entity =>
		{
			entity.ToTable("Answer");
			entity.Property(e => e.DateTime)
			  .HasColumnType("timestamp with time zone")
			  .HasColumnName("dateTime");

		});

		modelBuilder.Entity<SimpleAnswer>(entity =>
		{
			entity.ToTable("SimpleAnsver");
			entity.Property(e => e.DateTime)
			  .HasColumnType("timestamp with time zone")
			  .HasColumnName("dateTime");

		});

		modelBuilder.Entity<vAnswer>(entity =>
		{
			entity.ToTable("v_answer");
		});

		modelBuilder.Entity<vAnswer2>(entity =>
		{
			entity.ToTable("v_answer2");
		});

		modelBuilder.Entity<AvaiableUser>(entity =>
		{
			entity.ToTable("AvaiableUser");


			entity.Property(e => e.Cdate).HasColumnName("CDate");
			entity.Property(e => e.Cuser).HasColumnName("CUser");
			entity.Property(e => e.Ldate).HasColumnName("LDate");
			entity.Property(e => e.Luser).HasColumnName("LUser");
		});

		modelBuilder.Entity<AnswerNote>(entity =>
		{

		});


		modelBuilder.Entity<QuestionImage>(entity =>
		{
			entity.ToTable("QuestionImage");

			entity.HasIndex(e => e.Name, "ix_question_image").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
		});

		modelBuilder.Entity<Questionnaire>(entity =>
		{
			entity.ToTable("Questionnaire");

			entity.HasIndex(e => e.Name, "IX_Questionnaire_Name").IsUnique();
		});

		modelBuilder.Entity<User>(entity =>
{
	entity.ToTable("Users");
	entity.HasIndex(e => e.Email, "IX_user_email").IsUnique();

	entity.Property(e => e.Id).HasColumnName("id");
	entity.Property(e => e.Cdate).HasColumnName("CDate");
	entity.Property(e => e.Cuser).HasColumnName("CUser");
	entity.Property(e => e.Ldate).HasColumnName("LDate");
	entity.Property(e => e.Luser).HasColumnName("LUser");
	entity.Property(e => e.PasswordHash).HasDefaultValueSql("''::text");
	entity.Property(e => e.Salt).HasDefaultValueSql("''::text");
});

		modelBuilder.Entity<Vjsf>(entity =>
		{
			entity.ToTable("Vjsf");

			entity.HasIndex(e => e.Name, "IX_Vjsf_Name");

			entity.Property(e => e.Options).HasColumnType("jsonb");
			entity.Property(e => e.Schema).HasColumnType("jsonb");
			entity.Property(e => e.ShowNexButton)
				.IsRequired()
				.HasDefaultValueSql("true");
			entity.Property(e => e.ShowPrevButton)
				.IsRequired()
				.HasDefaultValueSql("true");
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