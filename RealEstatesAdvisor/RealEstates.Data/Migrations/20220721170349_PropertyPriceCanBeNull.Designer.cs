﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstates.Data;

namespace RealEstates.Data.Migrations
{
    [DbContext(typeof(RealEstatesDbContext))]
    [Migration("20220721170349_PropertyPriceCanBeNull")]
    partial class PropertyPriceCanBeNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RealEstates.Models.BuildingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BuildingTypes");
                });

            modelBuilder.Entity("RealEstates.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("RealEstates.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingTypeId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int?>("TotalFloors")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int?>("YardSize")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingTypeId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("TypeId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealEstates.Models.PropertyTag", b =>
                {
                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PropertyId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PropertiesTags");
                });

            modelBuilder.Entity("RealEstates.Models.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("RealEstates.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Importance")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RealEstates.Models.Property", b =>
                {
                    b.HasOne("RealEstates.Models.BuildingType", "BuildingType")
                        .WithMany("Properties")
                        .HasForeignKey("BuildingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstates.Models.District", "District")
                        .WithMany("Properties")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstates.Models.PropertyType", "Type")
                        .WithMany("Properties")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingType");

                    b.Navigation("District");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("RealEstates.Models.PropertyTag", b =>
                {
                    b.HasOne("RealEstates.Models.Property", "Property")
                        .WithMany("PropertiesTags")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstates.Models.Tag", "Tag")
                        .WithMany("PropertiesTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("RealEstates.Models.BuildingType", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstates.Models.District", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstates.Models.Property", b =>
                {
                    b.Navigation("PropertiesTags");
                });

            modelBuilder.Entity("RealEstates.Models.PropertyType", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealEstates.Models.Tag", b =>
                {
                    b.Navigation("PropertiesTags");
                });
#pragma warning restore 612, 618
        }
    }
}
