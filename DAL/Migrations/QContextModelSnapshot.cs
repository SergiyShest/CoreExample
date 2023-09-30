﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(QContext))]
    partial class QContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pgcrypto");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Questionnaire", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("CssStyle")
                        .HasColumnType("text");

                    b.Property<bool?>("Main")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Questionnaire_Name")
                        .IsUnique();

                    b.ToTable("Questionnaire");
                });

            modelBuilder.Entity("Core.Vjsf", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CssStyle")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NextButtonText")
                        .HasColumnType("text");

                    b.Property<string>("Options")
                        .HasColumnType("jsonb");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("PrevButtonText")
                        .HasColumnType("text");

                    b.Property<int?>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.Property<string>("Schema")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Vjsf_Name")
                        .IsUnique();

                    b.ToTable("Vjsf");
                });

            modelBuilder.Entity("DAL.Answer", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("Cdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int?>("QuestionnarieId")
                        .HasColumnType("integer");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("DAL.AvaiableUser", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("CDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CUser")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LUser")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AvaiableUser");
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("CDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CUser")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LUser")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_user_email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
