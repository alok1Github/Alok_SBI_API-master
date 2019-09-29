﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectHandler.API.DatabaseLayer;

namespace ProjectHandler.API.Migrations
{
    [DbContext(typeof(TaskHandlerDbContext))]
    [Migration("20190919174811_sba")]
    partial class sba
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHandler.API.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Priority");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectHandler.API.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("End_Date");

                    b.Property<bool>("EndTask");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Task")
                        .HasMaxLength(100);

                    b.Property<int?>("ParentTaskId")
                        .HasColumnName("ParentId");

                    b.Property<int>("Priority");

                    b.Property<int>("ProjectId")
                        .HasColumnName("ProjectId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("Start_Date");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("ProjectHandler.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ProjectId");

                    b.Property<int>("TaskId");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProjectHandler.API.Models.Project", b =>
                {
                    b.HasOne("ProjectHandler.API.Models.User")
                        .WithMany("Project")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ProjectHandler.API.Models.TaskItem", b =>
                {
                    b.HasOne("ProjectHandler.API.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectHandler.API.Models.User")
                        .WithMany("Task")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
