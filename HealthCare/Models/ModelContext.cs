using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HealthCare.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Creditcard> Creditcards { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Website> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH10_USER113;PASSWORD=aboodabood1;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH10_USER113")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.AboutusId)
                    .HasName("SYS_C00198099");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.AboutusId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUS_ID");

                entity.Property(e => e.BackgroundImage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("BACKGROUND_IMAGE");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Pargraph1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARGRAPH1");

                entity.Property(e => e.Pargraph2)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARGRAPH2");

                entity.Property(e => e.Pargraph3)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARGRAPH3");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Aboutus)
                    .HasForeignKey(d => d.WebId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WEB1");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENT");

                entity.Property(e => e.AppointmentId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("APPOINTMENT_ID");

                entity.Property(e => e.AcceptOrReject)
                    .HasPrecision(1)
                    .HasColumnName("ACCEPT_OR_REJECT");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Data)
                    .HasColumnType("DATE")
                    .HasColumnName("DATA");

                entity.Property(e => e.DoctorId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Massage)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("MASSAGE");

                entity.Property(e => e.Phonenumber)
                    .HasPrecision(10)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TIME");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CARD1");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DOCTOR1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER1");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("ATTENDANCE");

                entity.Property(e => e.AttendanceId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ATTENDANCE_ID");

                entity.Property(e => e.Day)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DAY");

                entity.Property(e => e.DoctorId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("DOCTOR_ID");

                entity.Property(e => e.DoctorName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("DOCTOR_NAME");

                entity.Property(e => e.Time)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIME");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DOCTOR2");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("SYS_C00198102");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.ContactId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACT_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phonenummber)
                    .HasPrecision(10)
                    .HasColumnName("PHONENUMMBER");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Contactus)
                    .HasForeignKey(d => d.WebId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WEB2");
            });

            modelBuilder.Entity<Creditcard>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("SYS_C00198083");

                entity.ToTable("CREDITCARD");

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER(20)")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Ccv)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CCV");

                entity.Property(e => e.ExpireData)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRE_DATA");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Creditcards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("DOCTOR");

                entity.Property(e => e.DoctorId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DOCTOR_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasPrecision(10)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.SpecializationId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("SPECIALIZATION_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROLE2");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SPECIALIZATION");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEWS");

                entity.Property(e => e.ReviewId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVIEW_ID");

                entity.Property(e => e.Rate)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RATE");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.WebId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WEB4");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("SPECIALIZATION");

                entity.Property(e => e.SpecializationId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SPECIALIZATION_ID");

                entity.Property(e => e.Specialization1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SPECIALIZATION");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("SYS_C00198105");

                entity.ToTable("TESTIMONIALS");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TEST_ID");

                entity.Property(e => e.Feedback)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FEEDBACK");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WEB_ID");

                entity.HasOne(d => d.Web)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.WebId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WEB3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasPrecision(10)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROLE1");
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasKey(e => e.WebId)
                    .HasName("SYS_C00198096");

                entity.ToTable("WEBSITE");

                entity.Property(e => e.WebId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("WEB_ID");

                entity.Property(e => e.BackgroundImage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("BACKGROUND_IMAGE");

                entity.Property(e => e.LogoPic)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("LOGO_PIC");

                entity.Property(e => e.Pargraph)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARGRAPH");

                entity.Property(e => e.SlidrPic)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("SLIDR_PIC");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Websites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER2");
            });

            modelBuilder.HasSequence("DEPT2_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
