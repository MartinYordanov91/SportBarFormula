﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportBarFormula.Infrastructure.Data;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    [DbContext(typeof(SportBarFormulaDbContext))]
    [Migration("20241111073234_addtestseed")]
    partial class addtestseed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the Category");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Category name (drinks, pizzas)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", t =>
                        {
                            t.HasComment("Group menu items by categories such as drinks, main courses, desserts.");
                        });

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Закуска"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Предястия"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Салати"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Сандвичи"
                        },
                        new
                        {
                            CategoryId = 5,
                            Name = "Бургери"
                        },
                        new
                        {
                            CategoryId = 6,
                            Name = "Мезета"
                        },
                        new
                        {
                            CategoryId = 7,
                            Name = "Паста/Ризото"
                        },
                        new
                        {
                            CategoryId = 8,
                            Name = "Сръбска скара"
                        },
                        new
                        {
                            CategoryId = 9,
                            Name = "Основни"
                        },
                        new
                        {
                            CategoryId = 10,
                            Name = "Пица"
                        },
                        new
                        {
                            CategoryId = 11,
                            Name = "Дресинг/Сосове"
                        },
                        new
                        {
                            CategoryId = 12,
                            Name = "Пърленка"
                        },
                        new
                        {
                            CategoryId = 13,
                            Name = "Десерти"
                        },
                        new
                        {
                            CategoryId = 14,
                            Name = "Топли Напитки"
                        },
                        new
                        {
                            CategoryId = 15,
                            Name = "Студени кафе напитки"
                        },
                        new
                        {
                            CategoryId = 16,
                            Name = "Безалкохолни напитки"
                        },
                        new
                        {
                            CategoryId = 17,
                            Name = "Фрешове"
                        },
                        new
                        {
                            CategoryId = 18,
                            Name = "Безалкохолни коктейли"
                        },
                        new
                        {
                            CategoryId = 19,
                            Name = "Алкохолни коктейли"
                        },
                        new
                        {
                            CategoryId = 20,
                            Name = "Бира"
                        },
                        new
                        {
                            CategoryId = 21,
                            Name = "Ядки"
                        },
                        new
                        {
                            CategoryId = 22,
                            Name = "Водка"
                        },
                        new
                        {
                            CategoryId = 23,
                            Name = "Джин"
                        },
                        new
                        {
                            CategoryId = 24,
                            Name = "Текила"
                        },
                        new
                        {
                            CategoryId = 25,
                            Name = "Шотове"
                        },
                        new
                        {
                            CategoryId = 26,
                            Name = "Уиски"
                        },
                        new
                        {
                            CategoryId = 27,
                            Name = "Ром"
                        },
                        new
                        {
                            CategoryId = 28,
                            Name = "Коняк"
                        },
                        new
                        {
                            CategoryId = 29,
                            Name = "Дижестиви"
                        },
                        new
                        {
                            CategoryId = 30,
                            Name = "Анасонови"
                        },
                        new
                        {
                            CategoryId = 31,
                            Name = "Ликьори/Аперитиви"
                        },
                        new
                        {
                            CategoryId = 32,
                            Name = "Ракии"
                        },
                        new
                        {
                            CategoryId = 33,
                            Name = "Вино"
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the feedback");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User's comment");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time when the feedback was created");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasComment("Rating given by the user (e.g., from 1 to 5)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Identifier of the user who provided the feedback");

                    b.HasKey("FeedbackId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks", t =>
                        {
                            t.HasComment("Stores customer feedback and ratings for service, menu, or events.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the item");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the Category");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Item description");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Item Image URL");

                    b.Property<string>("Ingredients")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("List of ingredients");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasComment("Item availability flag");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Soft delit flag");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Item name");

                    b.Property<int>("PreparationTime")
                        .HasColumnType("int")
                        .HasComment("Preparation time in minutes");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Item price");

                    b.HasKey("MenuItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("MenuItems", t =>
                        {
                            t.HasComment("Contains information about menu items - food, drinks and more.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the order");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasComment("Order date");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount of the order");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Identifier of the user who placed the order");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", t =>
                        {
                            t.HasComment("Contains information about orders placed by customers.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the OrderItem");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int")
                        .HasComment("Item ID (foreign key to MenuItems table)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasComment("Order ID (foreign key to Orders table)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Item price at time of order vs. quantity");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasComment("Item quantity");

                    b.HasKey("OrderItemId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", t =>
                        {
                            t.HasComment("This table is the link between Orders and MenuItems. Each line in it represents one item in the order");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the payment");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Amount of the payment");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasComment("Identifier of the order associated with the payment");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time of the payment");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Payment method (e.g., cash, card)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Payment status (e.g., successful, unsuccessful)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Identifier of the user who made the payment");

                    b.HasKey("PaymentId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments", t =>
                        {
                            t.HasComment("Tracks information about payments for orders.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the reservation");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("bit")
                        .HasComment("Indicates whether the reservation is canceled");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int")
                        .HasComment("Number of guests");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time of the reservation");

                    b.Property<int>("TableId")
                        .HasColumnType("int")
                        .HasComment("Identifier of the reserved table");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Identifier of the user who made the reservation");

                    b.HasKey("ReservationId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations", t =>
                        {
                            t.HasComment("Manages table reservations in the sports bar.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the shift");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShiftId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasComment("End time of the shift");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Role of the employee during the shift");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2")
                        .HasComment("Start time of the shift");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Identifier of the user assigned to the shift");

                    b.HasKey("ShiftId");

                    b.HasIndex("UserId");

                    b.ToTable("Shifts", t =>
                        {
                            t.HasComment("Manages employee shifts to organize working hours.");
                        });
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier of the table");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int")
                        .HasComment("Table capacity");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasComment("Table availability flag");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Table location (e.g., indoor, outdoor)");

                    b.Property<string>("TableNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Table number");

                    b.HasKey("TableId");

                    b.ToTable("Tables", t =>
                        {
                            t.HasComment("Contains information about tables in the restaurant, such as table number and capacity.");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Feedback", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.MenuItem", b =>
                {
                    b.HasOne("SportBarFormula.Infrastructure.Data.Models.Category", "Category")
                        .WithMany("MenuItems")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Order", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.OrderItem", b =>
                {
                    b.HasOne("SportBarFormula.Infrastructure.Data.Models.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SportBarFormula.Infrastructure.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Payment", b =>
                {
                    b.HasOne("SportBarFormula.Infrastructure.Data.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Reservation", b =>
                {
                    b.HasOne("SportBarFormula.Infrastructure.Data.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Shift", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Category", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("SportBarFormula.Infrastructure.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
