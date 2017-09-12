using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using projectPrint.Data;

namespace projectPrint.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170912014714_PaymentType_mig")]
    partial class PaymentType_mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("projectPrint.Models.ApplicationUser", b =>
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

            modelBuilder.Entity("projectPrint.Models.BillingAddress", b =>
                {
                    b.Property<int>("BillingAddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int?>("IsDefault");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<int>("Unit");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("BillingAddressID");

                    b.HasIndex("UserId");

                    b.ToTable("BillingAddress");
                });

            modelBuilder.Entity("projectPrint.Models.CompositeProduct", b =>
                {
                    b.Property<int>("CompositeProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("InkID");

                    b.Property<int?>("OrderID");

                    b.Property<int?>("ProductTypeID");

                    b.Property<int?>("ScreenID");

                    b.HasKey("CompositeProductID");

                    b.HasIndex("InkID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductTypeID");

                    b.HasIndex("ScreenID");

                    b.ToTable("CompositeProduct");
                });

            modelBuilder.Entity("projectPrint.Models.Ink", b =>
                {
                    b.Property<int>("InkID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("InkID");

                    b.ToTable("Ink");
                });

            modelBuilder.Entity("projectPrint.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BillingAddressID");

                    b.Property<int?>("IsFulfilled");

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

            modelBuilder.Entity("projectPrint.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("IsActive");

                    b.Property<int>("IsPrimary");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("PaymentTypeID");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("projectPrint.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseColor")
                        .IsRequired();

                    b.Property<double>("Cost");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ProductTypeID");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("projectPrint.Models.Screen", b =>
                {
                    b.Property<int>("ScreenID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ScreenID");

                    b.ToTable("Screen");
                });

            modelBuilder.Entity("projectPrint.Models.ShippingAddress", b =>
                {
                    b.Property<int>("ShippingAddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int?>("IsDefault");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<int>("Unit");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("ShippingAddressID");

                    b.HasIndex("UserId");

                    b.ToTable("ShippingAddress");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("projectPrint.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("projectPrint.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("projectPrint.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("projectPrint.Models.BillingAddress", b =>
                {
                    b.HasOne("projectPrint.Models.ApplicationUser", "User")
                        .WithMany("BillingAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("projectPrint.Models.CompositeProduct", b =>
                {
                    b.HasOne("projectPrint.Models.Ink", "Ink")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("InkID");

                    b.HasOne("projectPrint.Models.Order", "Order")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("OrderID");

                    b.HasOne("projectPrint.Models.ProductType", "ProductType")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("ProductTypeID");

                    b.HasOne("projectPrint.Models.Screen", "Screen")
                        .WithMany("CompositeProduct")
                        .HasForeignKey("ScreenID");
                });

            modelBuilder.Entity("projectPrint.Models.Order", b =>
                {
                    b.HasOne("projectPrint.Models.BillingAddress", "BillingAddress")
                        .WithMany("Order")
                        .HasForeignKey("BillingAddressID");

                    b.HasOne("projectPrint.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeID");

                    b.HasOne("projectPrint.Models.ShippingAddress", "ShippingAddress")
                        .WithMany("Order")
                        .HasForeignKey("ShippingAddressID");

                    b.HasOne("projectPrint.Models.ApplicationUser", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("projectPrint.Models.PaymentType", b =>
                {
                    b.HasOne("projectPrint.Models.ApplicationUser", "User")
                        .WithMany("PaymentType")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("projectPrint.Models.ShippingAddress", b =>
                {
                    b.HasOne("projectPrint.Models.ApplicationUser", "User")
                        .WithMany("ShippingAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
