﻿using Hospital.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appoiment>()
                .HasOne(x => x.UserPatient)
                .WithMany(x => x.AppoimentPatient)
                .HasForeignKey(x => x.UserPatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appoiment>()
                .HasOne(x => x.UserDoctor)
                .WithMany(x => x.AppoimentDoctor)
                .HasForeignKey(x => x.UserDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RolesPermission>().
                HasKey(x => new { x.PermisosId, x.rolId });

        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appoiment> Appoiments { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<MedicalSpe> MedicalSpe{ get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicalOrder> MedicalOrders { get; set; }
       
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<RolesPermission> RolesPermisos { get; set; }

    }
}
