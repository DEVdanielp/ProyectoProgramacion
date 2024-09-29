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

            modelBuilder.Entity("Hospital.Web.Data.Entities.User", b =>
                {
                    b.HasOne("Hospital.Web.Data.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
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
