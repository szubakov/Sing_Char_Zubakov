﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sing_Char_Zubakov.Data;

#nullable disable

namespace Sing_Char_Zubakov.Migrations
{
    [DbContext(typeof(ChatingContext))]
    [Migration("20231129171342_addus")]
    partial class addus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.23");

            modelBuilder.Entity("Sing_Char_Zubakov.Data.Mess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Mess");
                });

            modelBuilder.Entity("Sing_Char_Zubakov.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sing_Char_Zubakov.Data.Mess", b =>
                {
                    b.HasOne("Sing_Char_Zubakov.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
