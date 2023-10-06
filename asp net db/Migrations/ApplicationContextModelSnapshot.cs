﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using asp_net_db.Data;

#nullable disable

namespace asp_net_db.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("asp_net_db.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("asp_net_db.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InviteHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("StudentsIds")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("createdAt")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("asp_net_db.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("integer");

                    b.Property<long>("Deadline")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("createdAt")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("asp_net_db.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LessonType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("canRetry")
                        .HasColumnType("boolean");

                    b.Property<long>("createdAt")
                        .HasColumnType("bigint");

                    b.Property<long?>("endTime")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("minScore")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("asp_net_db.Models.SolvedHomework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CheckedComment")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<double>("ScoreOf5")
                        .HasColumnType("double precision");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<long>("createdAt")
                        .HasColumnType("bigint");

                    b.Property<bool>("isChecked")
                        .HasColumnType("boolean");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("SolvedHomeworks");
                });

            modelBuilder.Entity("asp_net_db.Models.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("asp_net_db.Models.Tracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LessonId")
                        .HasColumnType("integer");

                    b.Property<int>("RetryCount")
                        .HasColumnType("integer");

                    b.Property<int>("ScoreOf100")
                        .HasColumnType("integer");

                    b.Property<double>("ScoreOf5")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<long>("createdAt")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("asp_net_db.Models.Content", b =>
                {
                    b.HasOne("asp_net_db.Models.Course", null)
                        .WithMany("Contents")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("asp_net_db.Models.Homework", b =>
                {
                    b.HasOne("asp_net_db.Models.Course", null)
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("asp_net_db.Models.Lesson", b =>
                {
                    b.HasOne("asp_net_db.Models.Course", null)
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("asp_net_db.Models.Course", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("Homeworks");

                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}