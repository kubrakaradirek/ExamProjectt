using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=MONSTER\\MSSQLSERVERR;database=ExamProjectDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
        

        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Group> Groups{ get; set; }
        public DbSet<QnAs> QnAs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)//Validations
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Contact).HasMaxLength(50);
                entity.Property(e => e.CvFileName).HasMaxLength(250);
                entity.Property(e => e.PictureFileName).HasMaxLength(250);
                entity.HasOne(d => d.Group).WithMany(p => p.Students).HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<QnAs>(entity =>
            {
                entity.Property(e => e.Question).IsRequired();
                entity.Property(e => e.Option1).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option2).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option3).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option4).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Answer).IsRequired();
                entity.HasOne(d => d.Exam).WithMany(p => p.QnAs).HasForeignKey(d => d.ExamId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(100);
                entity.HasOne(d => d.User).WithMany(p => p.Groups).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(250);
                entity.HasOne(d => d.Group).WithMany(p => p.Exams).HasForeignKey(d => d.GroupId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.HasOne(d => d.Exam).WithMany(p => p.ExamResults).HasForeignKey(d => d.ExamId);
                entity.HasOne(d => d.QnAs).WithMany(p => p.ExamResults).HasForeignKey(d => d.QnAsId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Student).WithMany(p => p.ExamResults).HasForeignKey(d => d.StudentId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            base.OnModelCreating(modelBuilder); 
        }
        
    }
}
