﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WizardWares.DataAccess.Data;

#nullable disable

namespace WizardWares.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240520003636_addCategoryImgAndInStock")]
    partial class addCategoryImgAndInStock
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WizardWares.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            ImageUrl = "",
                            Name = "Potions"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            ImageUrl = "",
                            Name = "Spell Books"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            ImageUrl = "",
                            Name = "Wands"
                        });
                });

            modelBuilder.Entity("WizardWares.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStock")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("RarityId")
                        .HasColumnType("int");

                    b.Property<string>("TradeItem")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RarityId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 health.",
                            ImageUrl = "",
                            InStock = 1000,
                            Name = "inferior health potion",
                            Price = 15.0,
                            RarityId = 1,
                            TradeItem = "A piece of hidden lore"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 stamina.",
                            ImageUrl = "",
                            InStock = 1000,
                            Name = "inferior stamina potion",
                            Price = 15.0,
                            RarityId = 1,
                            TradeItem = "A piece of hidden lore"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 mana.",
                            ImageUrl = "",
                            InStock = 1000,
                            Name = "inferior mana potion",
                            Price = 15.0,
                            RarityId = 1,
                            TradeItem = "A piece of hidden lore"
                        });
                });

            modelBuilder.Entity("WizardWares.Models.Rarity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValueOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rarities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColorCode = "1111111",
                            Name = "Common",
                            ValueOrder = 1
                        });
                });

            modelBuilder.Entity("WizardWares.Models.Product", b =>
                {
                    b.HasOne("WizardWares.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WizardWares.Models.Rarity", "Rarity")
                        .WithMany()
                        .HasForeignKey("RarityId");

                    b.Navigation("Category");

                    b.Navigation("Rarity");
                });
#pragma warning restore 612, 618
        }
    }
}