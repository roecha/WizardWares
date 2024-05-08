﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WizardWares.DataAccess.Data;

#nullable disable

namespace WizardWares.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            Name = "Potions"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Spell Books"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceMoney")
                        .HasColumnType("float");

                    b.Property<string>("PriceObject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 health.",
                            ImageUrl = "",
                            Name = "inferior health potion",
                            PriceMoney = 15.0,
                            PriceObject = "A piece of hidden lore"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 stamina.",
                            ImageUrl = "",
                            Name = "inferior stamina potion",
                            PriceMoney = 15.0,
                            PriceObject = "A piece of hidden lore"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Drink this potion to recover 50 mana.",
                            ImageUrl = "",
                            Name = "inferior mana potion",
                            PriceMoney = 15.0,
                            PriceObject = "A piece of hidden lore"
                        });
                });

            modelBuilder.Entity("WizardWares.Models.Product", b =>
                {
                    b.HasOne("WizardWares.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
