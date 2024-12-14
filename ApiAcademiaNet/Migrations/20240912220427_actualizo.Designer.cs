﻿// <auto-generated />
using System;
using ApiAcademiaNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiAcademiaNet.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240912220427_actualizo")]
    partial class actualizo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiAcademiaNet.Models.AlumnoInscripcion", b =>
                {
                    b.Property<int>("IdInscripcion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdInscripcion"));

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<string>("condicion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("nota")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdInscripcion");

                    b.HasIndex("IdCurso");

                    b.HasIndex("IdPersona");

                    b.ToTable("AlumnoInscripcion");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Comision", b =>
                {
                    b.Property<int>("IdComision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdComision"));

                    b.Property<string>("DescComision")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdPlan")
                        .HasColumnType("int");

                    b.HasKey("IdComision");

                    b.HasIndex("IdPlan");

                    b.ToTable("Comision");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Curso", b =>
                {
                    b.Property<int>("IdCurso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCurso"));

                    b.Property<int>("AnioCalendario")
                        .HasColumnType("int");

                    b.Property<int>("IdComision")
                        .HasColumnType("int");

                    b.Property<int>("IdMateria")
                        .HasColumnType("int");

                    b.Property<int>("cupo")
                        .HasColumnType("int");

                    b.HasKey("IdCurso");

                    b.HasIndex("IdComision");

                    b.HasIndex("IdMateria");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.DocenteCurso", b =>
                {
                    b.Property<int>("IdDictado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDictado"));

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.HasKey("IdDictado");

                    b.HasIndex("IdCurso");

                    b.HasIndex("IdPersona");

                    b.ToTable("DocenteCurso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Especialidad", b =>
                {
                    b.Property<int>("IdEspecialidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEspecialidad"));

                    b.Property<string>("DescEspecialidad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdEspecialidad");

                    b.ToTable("Especialidad");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Materia", b =>
                {
                    b.Property<int>("IdMateria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMateria"));

                    b.Property<string>("DescMateria")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("HsSemanales")
                        .HasColumnType("int");

                    b.Property<int>("HsTotales")
                        .HasColumnType("int");

                    b.Property<int>("IdPlan")
                        .HasColumnType("int");

                    b.HasKey("IdMateria");

                    b.HasIndex("IdPlan");

                    b.ToTable("Materia");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdPlan")
                        .HasColumnType("int");

                    b.Property<string>("Legajo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdPersona");

                    b.HasIndex("IdPlan");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Plan", b =>
                {
                    b.Property<int>("IdPlan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlan"));

                    b.Property<string>("DescPlan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdEspecialidad")
                        .HasColumnType("int");

                    b.HasKey("IdPlan");

                    b.HasIndex("IdEspecialidad");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.AlumnoInscripcion", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Curso", "Curso")
                        .WithMany("AlumnoInscripcion")
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ApiAcademiaNet.Models.Persona", "Persona")
                        .WithMany("AlumnoInscripcion")
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Comision", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Plan", "Plan")
                        .WithMany("Comision")
                        .HasForeignKey("IdPlan")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Curso", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Comision", "Comision")
                        .WithMany("Curso")
                        .HasForeignKey("IdComision")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ApiAcademiaNet.Models.Materia", "Materia")
                        .WithMany("Curso")
                        .HasForeignKey("IdMateria")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Comision");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.DocenteCurso", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Curso", "Curso")
                        .WithMany("DocenteCurso")
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ApiAcademiaNet.Models.Persona", "Persona")
                        .WithMany("DocenteCurso")
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Materia", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Plan", "Plan")
                        .WithMany("Materia")
                        .HasForeignKey("IdPlan")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Persona", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Plan", "Plan")
                        .WithMany("Persona")
                        .HasForeignKey("IdPlan")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Plan", b =>
                {
                    b.HasOne("ApiAcademiaNet.Models.Especialidad", "Especialidad")
                        .WithMany("Plan")
                        .HasForeignKey("IdEspecialidad")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Especialidad");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Comision", b =>
                {
                    b.Navigation("Curso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Curso", b =>
                {
                    b.Navigation("AlumnoInscripcion");

                    b.Navigation("DocenteCurso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Especialidad", b =>
                {
                    b.Navigation("Plan");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Materia", b =>
                {
                    b.Navigation("Curso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Persona", b =>
                {
                    b.Navigation("AlumnoInscripcion");

                    b.Navigation("DocenteCurso");
                });

            modelBuilder.Entity("ApiAcademiaNet.Models.Plan", b =>
                {
                    b.Navigation("Comision");

                    b.Navigation("Materia");

                    b.Navigation("Persona");
                });
#pragma warning restore 612, 618
        }
    }
}