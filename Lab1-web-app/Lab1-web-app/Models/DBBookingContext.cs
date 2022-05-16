using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab1_web_app
{
    public partial class DBBookingContext : DbContext
    {
        public DBBookingContext()
        {
        }

        public DBBookingContext(DbContextOptions<DBBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accomodation> Accomodations { get; set; } = null!;
        public virtual DbSet<AccomodationStatus> AccomodationStatuses { get; set; } = null!;
        public virtual DbSet<AccomodationType> AccomodationTypes { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingStatus> BookingStatuses { get; set; } = null!;
        public virtual DbSet<Cancelation> Cancelations { get; set; } = null!;
        public virtual DbSet<CancelationStatus> CancelationStatuses { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<MealService> MealServices { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= DROBINDDA; Database=DBBooking; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accomodation>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Accomodations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accomodations_Cities");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accomodations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accomodations_AccomodationStatuses");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Accomodations)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accomodations_AccomodationTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accomodations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accomodations_Users");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Bookings_Rooms");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_BookingStatuses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_Users");
            });

            modelBuilder.Entity<Cancelation>(entity =>
            {
                entity.Property(e => e.CancelationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Cancelations)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cancelations_Bookings");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Cancelations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cancelations_CancelationStatuses");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.PhoneCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Bookings");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_PaymentMethods");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_PaymentStatuses");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentMethods)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentMethods_Users");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(d => d.Accomodation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.AccomodationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Accomodations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Users");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.HasTv).HasColumnName("HasTV");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Size).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Accomodation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.AccomodationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Rooms_Accomodations");

                entity.HasOne(d => d.MealService)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.MealServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_MealServices");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_RoomStatuses");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Countries");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserStatuses");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
