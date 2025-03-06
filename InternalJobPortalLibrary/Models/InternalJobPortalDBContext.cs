using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternalJobPortalLibrary.Models;

public partial class InternalJobPortalDBContext : DbContext
{
    public InternalJobPortalDBContext()
    {

    }

    public InternalJobPortalDBContext(DbContextOptions<InternalJobPortalDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplyJob> ApplyJobs { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:sqlserverteam2.database.windows.net,1433;Initial Catalog=InternalJobPortalDB-Tream2;Persist Security Info=False;User ID=team2;Password=Yuv@r@j123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplyJob>(entity =>
        {
            entity.HasKey(e => new { e.PostID, e.EmployeeID }).HasName("PK__ApplyJob__ADBF64C7E9BE3FEF");

            entity.ToTable("ApplyJob");

            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AppliedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApplyJob__Employ__398D8EEE");

            entity.HasOne(d => d.Post).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.PostID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ApplyJob__PostID__38996AB5");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF15B6E0690");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EmailID)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.JobID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobID)
                .HasConstraintName("FK__Employee__JobID__267ABA7A");
        });

        modelBuilder.Entity<EmployeeSkill>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeID, e.SkillID }).HasName("PK__Employee__172A46EFD48A394B");

            entity.ToTable("EmployeeSkill");

            entity.Property(e => e.EmployeeID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSkills)
                .HasForeignKey(d => d.EmployeeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeS__Emplo__2F10007B");

            entity.HasOne(d => d.Skill).WithMany(p => p.EmployeeSkills)
                .HasForeignKey(d => d.SkillID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeS__Skill__300424B4");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobID).HasName("PK__Job__056690E229A5E2D9");

            entity.ToTable("Job");

            entity.Property(e => e.JobID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.JobDescription)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("money");
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.PostID).HasName("PK__JobPost__AA1260388AC04994");

            entity.ToTable("JobPost");

            entity.Property(e => e.DateOfPosting).HasColumnType("datetime");
            entity.Property(e => e.JobID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastDateToApply).HasColumnType("datetime");

            entity.HasOne(d => d.Job).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobID)
                .HasConstraintName("FK__JobPost__JobID__32E0915F");
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => new { e.JobID, e.SkillID }).HasName("PK__JobSkill__689C99FC28B0EB69");

            entity.ToTable("JobSkill");

            entity.Property(e => e.JobID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Job).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.JobID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobSkill__JobID__2B3F6F97");

            entity.HasOne(d => d.Skill).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.SkillID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobSkill__SkillI__2C3393D0");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillID).HasName("PK__Skill__DFA091E755A1FB11");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillID)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillLevel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SkillName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
