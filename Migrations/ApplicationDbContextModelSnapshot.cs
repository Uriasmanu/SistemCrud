﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemCrud.Data;

#nullable disable

namespace SistemCrud.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemCrud.Models.Collaborator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("SistemCrud.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SistemCrud.Models.ProjectCollaborator", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "CollaboratorId");

                    b.HasIndex("CollaboratorId");

                    b.ToTable("ProjectCollaborator");
                });

            modelBuilder.Entity("SistemCrud.Models.Tarefas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descritiva")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("SistemCrud.Models.TimeTracker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TarefasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.HasIndex("TarefasId");

                    b.ToTable("TimeTrackers");
                });

            modelBuilder.Entity("SistemCrud.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UUIDUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UUIDUserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SistemCrud.Models.Collaborator", b =>
                {
                    b.HasOne("SistemCrud.Models.User", "User")
                        .WithOne("Collaborator")
                        .HasForeignKey("SistemCrud.Models.Collaborator", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SistemCrud.Models.ProjectCollaborator", b =>
                {
                    b.HasOne("SistemCrud.Models.Collaborator", "Collaborator")
                        .WithMany("ProjectCollaborators")
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemCrud.Models.Project", "Project")
                        .WithMany("ProjectCollaborators")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collaborator");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("SistemCrud.Models.Tarefas", b =>
                {
                    b.HasOne("SistemCrud.Models.Collaborator", "Collaborator")
                        .WithMany("Tarefas")
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SistemCrud.Models.Project", "Project")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Collaborator");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("SistemCrud.Models.TimeTracker", b =>
                {
                    b.HasOne("SistemCrud.Models.Collaborator", "Collaborator")
                        .WithMany("TimeTrackers")
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SistemCrud.Models.Tarefas", "Tarefas")
                        .WithMany("TimeTrackers")
                        .HasForeignKey("TarefasId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Collaborator");

                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("SistemCrud.Models.Collaborator", b =>
                {
                    b.Navigation("ProjectCollaborators");

                    b.Navigation("Tarefas");

                    b.Navigation("TimeTrackers");
                });

            modelBuilder.Entity("SistemCrud.Models.Project", b =>
                {
                    b.Navigation("ProjectCollaborators");

                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("SistemCrud.Models.Tarefas", b =>
                {
                    b.Navigation("TimeTrackers");
                });

            modelBuilder.Entity("SistemCrud.Models.User", b =>
                {
                    b.Navigation("Collaborator")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
