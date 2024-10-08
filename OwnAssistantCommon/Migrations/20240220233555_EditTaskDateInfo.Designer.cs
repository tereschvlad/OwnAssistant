﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OwnAssistantCommon.Models;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240220233555_EditTaskDateInfo")]
    partial class EditTaskDateInfo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskCheckpointInfoDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerTaskMainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Long")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTaskMainId");

                    b.ToTable("CheckpointInfoTasks", (string)null);
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskDateInfoDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerTaskMainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TaskDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTaskMainId");

                    b.ToTable("DateInfoTasks", (string)null);
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskMainInfoDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CrtDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("PerformerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PerformerId");

                    b.ToTable("MainInfoTasks", (string)null);
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.RoleDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.UserDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CrtDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskCheckpointInfoDbModel", b =>
                {
                    b.HasOne("OwnAssistantCommon.Models.CustomerTaskMainInfoDbModel", "CustomerTaskMainInfo")
                        .WithMany("CustomerTaskCheckpointInfos")
                        .HasForeignKey("CustomerTaskMainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerTaskMainInfo");
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskDateInfoDbModel", b =>
                {
                    b.HasOne("OwnAssistantCommon.Models.CustomerTaskMainInfoDbModel", "CustomerTaskMainInfo")
                        .WithMany("CustomerTaskDateInfos")
                        .HasForeignKey("CustomerTaskMainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerTaskMainInfo");
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskMainInfoDbModel", b =>
                {
                    b.HasOne("OwnAssistantCommon.Models.UserDbModel", "CreatorUser")
                        .WithMany("CreatedTasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OwnAssistantCommon.Models.UserDbModel", "PerformingUser")
                        .WithMany("PerformingTasks")
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("PerformingUser");
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.UserDbModel", b =>
                {
                    b.HasOne("OwnAssistantCommon.Models.RoleDbModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.CustomerTaskMainInfoDbModel", b =>
                {
                    b.Navigation("CustomerTaskCheckpointInfos");

                    b.Navigation("CustomerTaskDateInfos");
                });

            modelBuilder.Entity("OwnAssistantCommon.Models.UserDbModel", b =>
                {
                    b.Navigation("CreatedTasks");

                    b.Navigation("PerformingTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
