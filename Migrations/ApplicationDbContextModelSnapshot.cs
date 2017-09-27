﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProjectPrintDos.Data;
using System;

namespace ProjectPrintDos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.BillingAddress", b =>
                {
                    b.Property<int>("BillingAddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<bool>("IsDefault");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<int?>("Unit");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("BillingAddressID");

                    b.HasIndex("UserId");

                    b.ToTable("BillingAddress");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.CompositeProduct", b =>
                {
                    b.Property<int>("CompositeProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("InkID");

                    b.Property<int?>("OrderID");

                    b.Property<double?>("Price");

                    b.Property<int?>("ProductTypeID");

                    b.Property<int?>("ScreenID");

                    b.HasKey("CompositeProductID");

                    b.HasIndex("InkID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductTypeID");

                    b.HasIndex("ScreenID");

                    b.ToTable("CompositeProduct");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.Ink", b =>
                {
                    b.Property<int>("InkID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("Img");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("InkID");

                    b.ToTable("Ink");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BillingAddressID");

                    b.Property<bool?>("IsFulfilled");

                    b.Property<int?>("PaymentTypeID");

                    b.Property<int?>("ShippingAddressID");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("OrderID");

                    b.HasIndex("BillingAddressID");

                    b.HasIndex("PaymentTypeID");

                    b.HasIndex("ShippingAddressID");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("PaymentTypeID");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseColor")
                        .IsRequired();

                    b.Property<double>("Cost");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Img");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.Screen", b =>
                {
                    b.Property<int>("ScreenID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("Img");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ScreenID");

                    b.ToTable("Screen");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.ShippingAddress", b =>
                {
                    b.Property<int>("ShippingAddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<bool>("IsDefault");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<int?>("Unit");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("ShippingAddressID");

                    b.HasIndex("UserId");

                    b.ToTable("ShippingAddress");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectPrintDos.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectPrintDos.Models.BillingAddress", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser", "User")
                        .WithMany("BillingAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectPrintDos.Models.CompositeProduct", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.Ink", "Ink")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("InkID");

                    b.HasOne("ProjectPrintDos.Models.Order", "Order")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("OrderID");

                    b.HasOne("ProjectPrintDos.Models.ProductType", "ProductType")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("ProductTypeID");

                    b.HasOne("ProjectPrintDos.Models.Screen", "Screen")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("ScreenID");
                });

            modelBuilder.Entity("ProjectPrintDos.Models.Order", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.BillingAddress", "BillingAddress")
                        .WithMany("Order")
                        .HasForeignKey("BillingAddressID");

                    b.HasOne("ProjectPrintDos.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeID");

                    b.HasOne("ProjectPrintDos.Models.ShippingAddress", "ShippingAddress")
                        .WithMany("Order")
                        .HasForeignKey("ShippingAddressID");

                    b.HasOne("ProjectPrintDos.Models.ApplicationUser", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectPrintDos.Models.PaymentType", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser", "User")
                        .WithMany("PaymentType")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectPrintDos.Models.ShippingAddress", b =>
                {
                    b.HasOne("ProjectPrintDos.Models.ApplicationUser", "User")
                        .WithMany("ShippingAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
