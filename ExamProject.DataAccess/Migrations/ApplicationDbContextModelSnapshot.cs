﻿// <auto-generated />
using System;
using ExamProject.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExamProject.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExamProject.DataAccess.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("ExamProject.DataAccess.ExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Answer")
                        .HasColumnType("int");

                    b.Property<int?>("ExamId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("QnAsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("QnAsId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ExamProject.DataAccess.QnAs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Answer")
                        .HasColumnType("int");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("Option1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option3")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option4")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("QnAs");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CvFileName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PictureFileName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ExamProject.DataAccess.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Exam", b =>
                {
                    b.HasOne("ExamProject.DataAccess.Group", "Group")
                        .WithMany("Exams")
                        .HasForeignKey("GroupId")
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ExamProject.DataAccess.ExamResult", b =>
                {
                    b.HasOne("ExamProject.DataAccess.Exam", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamProject.DataAccess.QnAs", "QnAs")
                        .WithMany("ExamResults")
                        .HasForeignKey("QnAsId")
                        .IsRequired();

                    b.HasOne("ExamProject.DataAccess.Student", "Student")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentId")
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("QnAs");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Group", b =>
                {
                    b.HasOne("ExamProject.DataAccess.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExamProject.DataAccess.QnAs", b =>
                {
                    b.HasOne("ExamProject.DataAccess.Exam", "Exam")
                        .WithMany("QnAs")
                        .HasForeignKey("ExamId")
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Student", b =>
                {
                    b.HasOne("ExamProject.DataAccess.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Exam", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("QnAs");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Group", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("ExamProject.DataAccess.QnAs", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("ExamProject.DataAccess.Student", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("ExamProject.DataAccess.User", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
