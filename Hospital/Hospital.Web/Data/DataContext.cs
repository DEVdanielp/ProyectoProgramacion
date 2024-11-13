using Hospital.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Hospital.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureKeys(builder);
            ConfigureIndexes(builder);

            builder.Entity<Appoiment>().HasOne(a => a.UserPatient)
                                       .WithMany()
                                       .HasForeignKey(a => a.UserPatientId)
                                       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appoiment>().HasOne(a => a.UserDoctor) 
                                       .WithMany()
                                       .HasForeignKey(a => a.UserDoctorId) 
                                       .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }


        private void ConfigureKeys(ModelBuilder builder)
        {
            //Haciendo la Relacion Muchos a Muchos
            // Role Permissions
            builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<RolePermission>().HasOne(rp => rp.Role)
                                            .WithMany(r => r.RolePermissions)
                                            .HasForeignKey(rp => rp.RoleId);

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
            //builder.Entity<User>().HasIndex(u => u.Document)
            //                                 .IsUnique();
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