﻿// <auto-generated />
using System;
using GlickoRank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GlickoRank.Migrations
{
    [DbContext(typeof(MvcGlickoRankContext))]
    partial class MvcGlickoRankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Mode")
                        .HasColumnType("int");

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
                            Name = "MajkPascal_1_2305843009296116294"
                        });
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivity", b =>
                {
                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("ActivityID")
                        .HasColumnType("int");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<int>("Standing")
                        .HasColumnType("int");

                    b.Property<float>("Team")
                        .HasColumnType("real");

                    b.Property<float>("TeamScore")
                        .HasColumnType("real");

                    b.HasKey("CharacterID", "ActivityID");

                    b.HasIndex("ActivityID");

                    b.ToTable("CharacterActivity");
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivityModeRank", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterActivityActivityID")
                        .HasColumnType("int");

                    b.Property<int>("CharacterActivityCharacterID")
                        .HasColumnType("int");

                    b.Property<int>("CharacterActivityID")
                        .HasColumnType("int");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<float>("RD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(350f);

                    b.Property<float>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(1500f);

                    b.Property<float>("Volatility")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0.06f);

                    b.HasKey("ID");

                    b.HasIndex("CharacterActivityCharacterID", "CharacterActivityActivityID");

                    b.ToTable("CharacterActivityModeRank");
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterModeRank", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<float>("RD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(350f);

                    b.Property<float>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(1500f);

                    b.Property<float>("Volatility")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0.06f);

                    b.HasKey("ID");

                    b.HasIndex("CharacterID");

                    b.ToTable("CharacterModeRank");
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivity", b =>
                {
                    b.HasOne("GlickoRank.Models.Activity", "Activity")
                        .WithMany("CharacterActivities")
                        .HasForeignKey("ActivityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlickoRank.Models.Character", "Character")
                        .WithMany("CharacterActivities")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterActivityModeRank", b =>
                {
                    b.HasOne("GlickoRank.Models.CharacterActivity", "CharacterActivity")
                        .WithMany()
                        .HasForeignKey("CharacterActivityCharacterID", "CharacterActivityActivityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GlickoRank.Models.CharacterModeRank", b =>
                {
                    b.HasOne("GlickoRank.Models.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
