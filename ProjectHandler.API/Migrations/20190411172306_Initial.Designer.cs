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
    [Migration("20190411172306_initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHandler.API.DatabaseLayer.Entity.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("End_Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Task")
                        .HasMaxLength(100);

                    b.Property<int?>("ParentTaskId")
                        .HasColumnName("ParentId");

                    b.Property<int>("Priority");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("Start_Date");

                    b.HasKey("Id");

                    b.ToTable("Task");
                });
#pragma warning restore 612, 618
        }
    }
}
