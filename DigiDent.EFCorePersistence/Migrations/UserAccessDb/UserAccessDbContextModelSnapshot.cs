﻿// <auto-generated />
using System;
using DigiDent.EFCorePersistence.UserAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigiDent.EFCorePersistence.Migrations.UserAccessDb
{
    [DbContext(typeof(UserAccessDbContext))]
    partial class UserAccessDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("User_Access")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DigiDent.Application.UserAccess.Tokens.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Token");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens", "User_Access");
                });

            modelBuilder.Entity("DigiDent.Domain.UserAccessContext.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Full Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Users_Email")
                        .IsUnique();

                    b.ToTable("Users", "User_Access");

                    b.HasData(
                        new
                        {
                            Id = new Guid("575dadc1-9fbf-492d-8b31-cdd61d567537"),
                            Email = "temp@admin.tmp",
                            FullName = "Temporary Administrator",
                            Password = "CjEWdqOvMly9mNUYkGdjPunJ2yGRTwjMmp8tvA5vQkU=:By9pbtPc95ZkkDOAZ7obDmfOgAqlJXdqUTrPJQJv+Bc=",
                            PhoneNumber = "+380000000000",
                            Role = "Administrator"
                        });
                });

            modelBuilder.Entity("DigiDent.Application.UserAccess.Tokens.RefreshToken", b =>
                {
                    b.HasOne("DigiDent.Domain.UserAccessContext.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("DigiDent.Application.UserAccess.Tokens.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
