﻿// <auto-generated />
using Introspekt.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Introspekt.DAL.Migrations
{
    [DbContext(typeof(CourseStoreContext))]
    partial class CourseStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Introspekt.DAL.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Photo");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Introspekt.DAL.Models.CourseOrder", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("OrderId");

                    b.HasKey("CourseId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("CourseOrders");
                });

            modelBuilder.Entity("Introspekt.DAL.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<long>("ChatId");

                    b.Property<string>("Date");

                    b.Property<string>("Email");

                    b.Property<string>("Status");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Introspekt.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Introspekt.DAL.Models.CourseOrder", b =>
                {
                    b.HasOne("Introspekt.DAL.Models.Course", "Course")
                        .WithMany("CourseOrders")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Introspekt.DAL.Models.Order", "Order")
                        .WithMany("CourseOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Introspekt.DAL.Models.User", b =>
                {
                    b.HasOne("Introspekt.DAL.Models.Order", "Order")
                        .WithOne("User")
                        .HasForeignKey("Introspekt.DAL.Models.User", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
