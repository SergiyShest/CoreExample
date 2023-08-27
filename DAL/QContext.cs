using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sasha.Lims.WebUI.Areas.Questions;
using Core;

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
    }
    public virtual DbSet<Vjsf> Vjsf { get; set; }


   public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      //  => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UserPortal;Username=postgres;Password=Qazwsx123");


        var loggerFactory = LoggerFactory.Create(builder =>
        {
            // builder.AddFilter("Microsoft", LogLevel.Warning)
            // AddFilter("System", LogLevel.Warning)
            builder.AddFilter("", LogLevel.Debug);
        }

        );

        options.UseLoggerFactory(loggerFactory) //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging();

        var connect = Configuration.GetConnectionString("WebApiDatabase");
        options.UseNpgsql(connect);
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("taxonomylevel", new[] { "Kingdom", "Phylum", "Class", "Order", "Family", "Genus", "Species" })
            .HasPostgresExtension("pgcrypto");

       

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_user_email");
            entity.Property(e => e.Id).HasColumnName("id");

        });

        //modelBuilder.Entity<VWfResult>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("v_wf_result");

        //    entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
        //    entity.Property(e => e.JsonData).HasColumnType("json");
        //});

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

        //modelBuilder.Entity<VjsfForm>(entity =>
        //{
        //    entity.HasIndex(e => e.VariableName, "IX_Vjsf_Form_Variable_Name").IsUnique();

        //    entity.Property(e => e.Model).HasColumnType("jsonb");
        //    entity.Property(e => e.Options).HasColumnType("jsonb");
        //    entity.Property(e => e.RefTable).HasMaxLength(255);
        //    entity.Property(e => e.Schema).HasColumnType("jsonb");
        //    entity.Property(e => e.VariableName).HasMaxLength(255);
        //});

        //modelBuilder.Entity<VjsfFormData>(entity =>
        //{
        //    entity.HasIndex(e => e.VjsfFormId, "IX_VjsfFormDatas_VjsfFormId");

        //    entity.Property(e => e.Json).HasColumnType("jsonb");
        //    entity.Property(e => e.RefToTable).HasColumnType("jsonb");

        //    entity.HasOne(d => d.VjsfForm).WithMany(p => p.VjsfFormData).HasForeignKey(d => d.VjsfFormId);
        //});

        //modelBuilder.Entity<VueComponent>(entity =>
        //{
        //    entity.HasIndex(e => e.VariableName, "IX_Vue_Component_Variable_Name").IsUnique();

        //    entity.Property(e => e.Json).HasColumnType("jsonb");
        //    entity.Property(e => e.RefTable).HasMaxLength(255);
        //    entity.Property(e => e.VariableName).HasMaxLength(255);
        //});

        //modelBuilder.Entity<VueComponentData>(entity =>
        //{
        //    entity.HasIndex(e => e.VueComponentId, "IX_VueComponentDatas_VueComponentId");

        //    entity.Property(e => e.Json).HasColumnType("jsonb");
        //    entity.Property(e => e.RefToTable).HasColumnType("jsonb");

        //    entity.HasOne(d => d.VueComponent).WithMany(p => p.VueComponentData).HasForeignKey(d => d.VueComponentId);
        //});

        //modelBuilder.Entity<WfResult>(entity =>
        //{
        //    entity.ToTable("wf_result");

        //    entity.HasIndex(e => e.PatientId, "IX_wf_result_PatientId");

        //    entity.Property(e => e.Date).HasColumnType("timestamp without time zone");

        //    entity.HasOne(d => d.Patient).WithMany(p => p.WfResults)
        //        .HasForeignKey(d => d.PatientId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        //modelBuilder.Entity<WfResultPoint>(entity =>
        //{
        //    entity.ToTable("wf_result_point");

        //    entity.HasIndex(e => e.Point, "IX_wf_result_point_Point").HasMethod("gist");

        //    entity.HasIndex(e => e.WorkflowResultId, "IX_wf_result_point_WorkflowResultId");

        //    entity.Property(e => e.Point).HasDefaultValueSql("'(0,0)'::point");

        //    entity.HasOne(d => d.WorkflowResult).WithMany(p => p.WfResultPoints).HasForeignKey(d => d.WorkflowResultId);
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
