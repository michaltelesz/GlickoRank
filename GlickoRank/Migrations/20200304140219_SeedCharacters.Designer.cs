﻿// <auto-generated />
using System;
using GlickoRank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GlickoRank.Migrations
{
    [DbContext(typeof(MvcGlickoRankContext))]
    [Migration("20200304140219_SeedCharacters")]
    partial class SeedCharacters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GlickoRank.Models.Activity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Period")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("GlickoRank.Models.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CharacterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembershipType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CharacterId = "2305843009296116294",
                            MembershipId = "4611686018470345232",
                            MembershipType = 1,
                            Name = "MajkPascal_2305843009296116294_1"
                        });
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivity", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("CharacterActivity");
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivity", b =>
                {
                    b.HasOne("GlickoRank.Models.Activity", "Activity")
                        .WithMany("CharacterActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlickoRank.Models.Character", "Character")
                        .WithMany("CharacterActivities")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
