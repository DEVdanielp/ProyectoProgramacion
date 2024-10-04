﻿// <auto-generated />
using System;
using Hospital.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hospital.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hospital.Web.Data.Entities.Appoiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time");

                    b.Property<int?>("UserDoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("UserPatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserDoctorId");

                    b.HasIndex("UserPatientId");

                    b.ToTable("Appoiments");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppoimentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NamePatient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppoimentId");

                    b.ToTable("MedicalHistory");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppoimentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("IdAppoiment")
                        .HasColumnType("int");

                    b.Property<int>("IdMedication")
                        .HasColumnType("int");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppoimentId");

                    b.HasIndex("MedicationId");

                    b.ToTable("MedicalOrders");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalSpe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("UserDoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserDoctorId");

                    b.ToTable("MedicalSpe");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Medication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommercialName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Laboratory")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("ScientificName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Permissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.RolesPermission", b =>
                {
                    b.Property<int?>("PermisosId")
                        .HasColumnType("int");

                    b.Property<int?>("rolId")
                        .HasColumnType("int");

                    b.HasKey("PermisosId", "rolId");

                    b.HasIndex("rolId");

                    b.ToTable("RolesPermisos");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppoimentId")
                        .HasColumnType("int");

                    b.Property<string>("StatusAppoiment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppoimentId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Birth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Appoiment", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.User", "UserDoctor")
                        .WithMany("AppoimentDoctor")
                        .HasForeignKey("UserDoctorId");

                    b.HasOne("Hospital.Web.Data.Entities.User", "UserPatient")
                        .WithMany("AppoimentPatient")
                        .HasForeignKey("UserPatientId");

                    b.Navigation("UserDoctor");

                    b.Navigation("UserPatient");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalHistory", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Appoiment", "Appoiments")
                        .WithMany()
                        .HasForeignKey("AppoimentId");

                    b.Navigation("Appoiments");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalOrder", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Appoiment", "Appoiment")
                        .WithMany()
                        .HasForeignKey("AppoimentId");

                    b.HasOne("Hospital.Web.Data.Entities.Medication", "Medication")
                        .WithMany()
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appoiment");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.MedicalSpe", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.User", "UserDoctor")
                        .WithMany()
                        .HasForeignKey("UserDoctorId");

                    b.Navigation("UserDoctor");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.RolesPermission", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Permissions", "Permisos")
                        .WithMany("RolPermisos")
                        .HasForeignKey("PermisosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Web.Data.Entities.Rol", "rol")
                        .WithMany("RolPermisos")
                        .HasForeignKey("rolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permisos");

                    b.Navigation("rol");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Status", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Appoiment", "Appoiment")
                        .WithMany()
                        .HasForeignKey("AppoimentId");

                    b.Navigation("Appoiment");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.User", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Permissions", b =>
                {
                    b.Navigation("RolPermisos");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.Rol", b =>
                {
                    b.Navigation("RolPermisos");
                });

            modelBuilder.Entity("Hospital.Web.Data.Entities.User", b =>
                {
                    b.Navigation("AppoimentDoctor");

                    b.Navigation("AppoimentPatient");
                });
#pragma warning restore 612, 618
        }
    }
}
