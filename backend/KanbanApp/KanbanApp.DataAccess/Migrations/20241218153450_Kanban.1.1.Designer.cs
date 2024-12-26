﻿// <auto-generated />
using System;
using KanbanApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KanbanApp.DataAccess.Migrations
{
    [DbContext(typeof(KanbanAppDbContext))]
    [Migration("20241218153450_Kanban.1.1")]
    partial class Kanban11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.BoardKanbanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.ColumnKanbanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.UserKanbanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SubtaskKanbanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtasks");
                });

            modelBuilder.Entity("TaskKanbanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssignedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ColumnId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssignedId");

                    b.HasIndex("ColumnId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.ColumnKanbanEntity", b =>
                {
                    b.HasOne("KanbanApp.DataAccess.Entites.BoardKanbanEntity", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("SubtaskKanbanEntity", b =>
                {
                    b.HasOne("TaskKanbanEntity", "Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskKanbanEntity", b =>
                {
                    b.HasOne("KanbanApp.DataAccess.Entites.UserKanbanEntity", "Assigned")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("KanbanApp.DataAccess.Entites.ColumnKanbanEntity", "Column")
                        .WithMany("Tasks")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assigned");

                    b.Navigation("Column");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.BoardKanbanEntity", b =>
                {
                    b.Navigation("Columns");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.ColumnKanbanEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("KanbanApp.DataAccess.Entites.UserKanbanEntity", b =>
                {
                    b.Navigation("AssignedTasks");
                });

            modelBuilder.Entity("TaskKanbanEntity", b =>
                {
                    b.Navigation("Subtasks");
                });
#pragma warning restore 612, 618
        }
    }
}
