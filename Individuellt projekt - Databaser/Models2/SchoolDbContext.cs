using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Individuellt_projekt___Databaser.Models2;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__FDF479865EDCD7F2");

            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("class_name");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Classes__course___403A8C7D");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__Classes__mentor___3F466844");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__8F1EF7AE409BFB20");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course_name");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C22324220788535E");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__3A8F732C265B3875");

            entity.Property(e => e.GradeId)
                .ValueGeneratedNever()
                .HasColumnName("grade_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.DateAssigned).HasColumnName("date_assigned");
            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("grade");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Grades__course_i__45F365D3");

            entity.HasOne(d => d.Staff).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Grades__staff_id__47DBAE45");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grades__student___46E78A0C");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__99A0E7A44131686E");

            entity.HasIndex(e => e.PositionName, "UQ__Position__0030EAD725B1E125").IsUnique();

            entity.Property(e => e.PositionId)
                .ValueGeneratedNever()
                .HasColumnName("position_id");
            entity.Property(e => e.MonthlySalary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monthly_salary");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9C2E2063B0");

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasColumnName("staff_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("surname");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Staff__departmen__4AB81AF0");

            entity.HasOne(d => d.Position).WithMany(p => p.Staff)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Staff__position___3A81B327");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__2A33069A963B7474");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("surname");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__class___4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
