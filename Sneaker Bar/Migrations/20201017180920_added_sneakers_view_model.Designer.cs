﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sneaker_Bar.Models;

namespace Sneaker_Bar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201017180920_added_sneakers_view_model")]
    partial class added_sneakers_view_model
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Sneaker_Bar.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("text")
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("article");
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("articleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("text")
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .HasColumnType("text");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("articleId");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Purchase", b =>
                {
                    b.Property<int>("purchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("sneakersId")
                        .HasColumnType("integer");

                    b.Property<int>("userId")
                        .HasColumnType("integer");

                    b.HasKey("purchaseId");

                    b.HasIndex("sneakersId");

                    b.HasIndex("userId");

                    b.ToTable("purchase");
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        },
                        new
                        {
                            Id = 3,
                            Name = "manager"
                        });
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Sneakers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("sneakers");
                });

            modelBuilder.Entity("Sneaker_Bar.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail.ru",
                            Password = "admin",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Article", b =>
                {
                    b.HasOne("Sneaker_Bar.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Comment", b =>
                {
                    b.HasOne("Sneaker_Bar.Models.Article", "article")
                        .WithMany()
                        .HasForeignKey("articleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sneaker_Bar.Models.Purchase", b =>
                {
                    b.HasOne("Sneaker_Bar.Models.Sneakers", "sneakers")
                        .WithMany()
                        .HasForeignKey("sneakersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sneaker_Bar.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sneaker_Bar.Models.User", b =>
                {
                    b.HasOne("Sneaker_Bar.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}