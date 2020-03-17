﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant_menu.Context;

namespace RestaurantMenu.DAL.Migrations
{
    [DbContext(typeof(DishesContext))]
    partial class DishesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Restaurant_menu.Models.Dish", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Calorific")
                        .HasColumnType("float");

                    b.Property<string>("Consist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CookTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Gram")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Price")
                        .HasColumnType("smallmoney");

                    b.HasKey("Id");

                    b.ToTable("Dish");
                });
#pragma warning restore 612, 618
        }
    }
}
