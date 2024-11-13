﻿using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //ConfigureAppoiments(builder);
            ConfigureKeys(builder);
            ConfigureIndexes(builder);

            base.OnModelCreating(builder);
        }

        //private void ConfigureAppoiments(ModelBuilder builder)
        //{

        //    builder.Entity<Appoiment>().HasKey(rp => new { rp.Id });



        //    builder.Entity<Appoiment>().HasOne(x => x.UserPatient)
        //                               .WithMany(x => x.AppoimentPatient)
        //                               .HasForeignKey(x => x.UserPatientId)
        //                               .OnDelete(DeleteBehavior.ClientSetNull);

        //    builder.Entity<Appoiment>().HasOne(x => x.UserDoctor)
        //                               .WithMany(x => x.AppoimentDoctor)
        //                               .HasForeignKey(x => x.UserDoctorId)
        //                               .OnDelete(DeleteBehavior.ClientSetNull);

        //}

        private void ConfigureKeys(ModelBuilder builder)
        {
            //Haciendo la Relacion Muchos a Muchos
            // Role Permissions
            builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

           // builder.Entity<RolePermission>().HasOne(rp => rp.Role)
                                            //.WithMany(r => r.RolePermissions)
                                            //.HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>().HasOne(rp => rp.Permission)
                                            .WithMany(p => p.RolePermissions)
                                            .HasForeignKey(rp => rp.PermissionId);
        }

        private void ConfigureIndexes(ModelBuilder builder)
        {
            //Haciendo unicos, ciertos atributos de las Entidades
            // Roles
            builder.Entity<HospitalRole>().HasIndex(r => r.Name)
                                             .IsUnique();
            // Medical Specialitation
            builder.Entity<MedicalSpe>().HasIndex(m => m.Name)
                                             .IsUnique();
            // Users
            builder.Entity<User>().HasIndex(u => u.Document)
                                             .IsUnique();
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<HospitalRole> HospitalRoles { get; set; }
        public DbSet<Appoiment> Appoiments { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<MedicalSpe> MedicalSpe { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicalOrder> MedicalOrders { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


    }
}