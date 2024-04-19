﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTM.DataAccess;

#nullable disable

namespace TTM.DataAccess.Migrations
{
    [DbContext(typeof(TTMContext))]
    [Migration("20240419151509_User_ImprovementsV1.2")]
    partial class User_ImprovementsV12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TTM.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Professional tasks, projects, deadlines, meetings, and collaborations. Keep track of job-related duties and goals effortlessly.",
                            Name = "Work"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Manage errands, appointments, hobbies, self-care, and chores efficiently. Organize daily routines and prioritize personal goals.",
                            Name = "Personal"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Stay on top of exercise, meals, medical appointments, therapy, and self-care. Achieve physical and mental well-being goals seamlessly.",
                            Name = "Health & Fitness"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Study for exams, complete assignments, attend classes, research topics, and pursue professional development. Support academic and career objectives effectively.",
                            Name = "Education"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Plan social activities, events, gatherings, and networking. Stay connected with friends and loved ones effortlessly.",
                            Name = "Social"
                        });
                });

            modelBuilder.Entity("TTM.Domain.Duty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<double?>("Hours")
                        .HasColumnType("float");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Duty", (string)null);
                });

            modelBuilder.Entity("TTM.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("TTM.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<short?>("Gender")
                        .HasColumnType("smallint");

                    b.Property<string>("LastName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8193)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasMaxLength(8193)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "johndoe@mail.com",
                            FirstName = "John",
                            Gender = (short)0,
                            LastName = "Doe",
                            Password = "12345",
                            Token = ""
                        },
                        new
                        {
                            Id = 2,
                            Email = "janedoe@mail.com",
                            FirstName = "Jane",
                            Gender = (short)2,
                            LastName = "Doe",
                            Password = "54321",
                            Token = ""
                        });
                });

            modelBuilder.Entity("TTM.Domain.Duty", b =>
                {
                    b.HasOne("TTM.Domain.Project", "Project")
                        .WithMany("Duties")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TTM.Domain.User", "User")
                        .WithMany("Duties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TTM.Domain.Project", b =>
                {
                    b.HasOne("TTM.Domain.Category", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TTM.Domain.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TTM.Domain.Category", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TTM.Domain.Project", b =>
                {
                    b.Navigation("Duties");
                });

            modelBuilder.Entity("TTM.Domain.User", b =>
                {
                    b.Navigation("Duties");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
