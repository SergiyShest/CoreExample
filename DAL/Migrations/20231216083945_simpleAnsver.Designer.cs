﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(QContext))]
    [Migration("20231216083945_simpleAnsver")]
    partial class simpleAnsver
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pgcrypto");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entity.Answer", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateOnly?>("Cdate")
                        .HasColumnType("date");

                    b.Property<DateTimeOffset?>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dateTime");

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

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("Entity.AvaiableUser", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("Cdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CDate");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Cuser")
                        .HasColumnType("text")
                        .HasColumnName("CUser");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Ldate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LDate");

                    b.Property<string>("Luser")
                        .HasColumnType("text")
                        .HasColumnName("LUser");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AvaiableUser", (string)null);
                });

            modelBuilder.Entity("Entity.HitCounter", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BrowserVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Cdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Cdate");

                    b.Property<string>("Cookies")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FlashVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Os")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OsVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("QuestionnaireId")
                        .HasColumnType("integer");

                    b.Property<string>("ScreenSize")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HitCounter");
                });

            modelBuilder.Entity("Entity.QuestionImage", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("CssStyle")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "ix_question_image")
                        .IsUnique();

                    b.ToTable("QuestionImage", (string)null);
                });

            modelBuilder.Entity("Entity.Questionnaire", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("CssStyle")
                        .HasColumnType("text");

                    b.Property<bool?>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<bool?>("Main")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Questionnaire_Name")
                        .IsUnique();

                    b.ToTable("Questionnaire", (string)null);
                });

            modelBuilder.Entity("Entity.SimpleAnsver", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateOnly?>("Cdate")
                        .HasColumnType("date");

                    b.Property<DateTimeOffset?>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dateTime");

                    b.Property<int?>("QuestionnarieId")
                        .HasColumnType("integer");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<string>("UserPhone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SimpleAnsver", (string)null);
                });

            modelBuilder.Entity("Entity.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime?>("Cdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CDate");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Cuser")
                        .HasColumnType("text")
                        .HasColumnName("CUser");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Ldate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LDate");

                    b.Property<string>("Luser")
                        .HasColumnType("text")
                        .HasColumnName("LUser");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValueSql("''::text");

                    b.Property<string>("Salt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValueSql("''::text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_user_email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Entity.Vjsf", b =>
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

                    b.Property<string>("NextQuestionCondition")
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

                    b.Property<bool?>("ShowNexButton")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValueSql("true");

                    b.Property<bool?>("ShowPrevButton")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValueSql("true");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Vjsf_Name");

                    b.ToTable("Vjsf", (string)null);
                });

            modelBuilder.Entity("Entity.vAnswer", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Area")
                        .HasColumnType("text");

                    b.Property<string>("Browser")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("Cdate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Os")
                        .HasColumnType("text");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int?>("QuestionnarieId")
                        .HasColumnType("integer");

                    b.Property<string>("ScreenSize")
                        .HasColumnType("text");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("Time")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("v_answer", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
